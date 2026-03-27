using RealTimeMarketAPI.Sdk.Internal;
using RealTimeMarketAPI.Sdk.Models.Ticker;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace RealTimeMarketAPI.Sdk.Clients
{
    internal sealed class WebSocketClient(RealMarketApiOptions options) : IWebSocketClient
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async IAsyncEnumerable<PriceTickerResult> StreamPriceAsync(
            string symbolCode,
            string timeFrame,
            [EnumeratorCancellation] CancellationToken ct = default)
        {
            var uri = BuildUri(symbolCode, timeFrame);
            using var ws = new ClientWebSocket();

            await ws.ConnectAsync(uri, ct);

            var buffer = new byte[8192];

            try
            {
                while (ws.State == WebSocketState.Open && !ct.IsCancellationRequested)
                {
                    var ms = new MemoryStream();
                    WebSocketReceiveResult received;
                    bool serverClosed = false;

                    try
                    {
                        do
                        {
                            received = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), ct);

                            if (received.MessageType == WebSocketMessageType.Close)
                            {
                                serverClosed = true;
                                break;
                            }

                            ms.Write(buffer, 0, received.Count);
                        }
                        while (!received.EndOfMessage);

                        if (serverClosed) break;

                        ms.Position = 0;
                        var ticker = await JsonSerializer.DeserializeAsync<PriceTickerResult>(ms, JsonOptions, ct);

                        if (ticker is not null)
                            yield return ticker;
                    }
                    finally
                    {
                        await ms.DisposeAsync();
                    }
                }
            }
            finally
            {
                if (ws.State == WebSocketState.Open)
                {
                    await ws.CloseAsync(
                        WebSocketCloseStatus.NormalClosure,
                        "Stream ended",
                        CancellationToken.None);
                }
            }
        }

        private Uri BuildUri(string symbolCode, string timeFrame)
        {
            var baseUri = new Uri(options.BaseUrl);
            var scheme = baseUri.Scheme == "https" ? "wss" : "ws";
            var host = baseUri.IsDefaultPort
                ? baseUri.Host
                : $"{baseUri.Host}:{baseUri.Port}";

            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = options.ApiKey,
                ["symbolCode"] = symbolCode,
                ["timeFrame"] = timeFrame
            });

            return new Uri($"{scheme}://{host}/price?{query}");
        }
    }
}
