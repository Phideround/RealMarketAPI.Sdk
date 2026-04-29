using RealTimeMarketAPI.Sdk.Internal;
using RealTimeMarketAPI.Sdk.Models.MultiTimeframe;
using RealTimeMarketAPI.Sdk.Models.OrderFlow;
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

        public IAsyncEnumerable<PriceTickerResult> StreamPriceAsync(
            string symbolCode, string timeFrame, CancellationToken ct = default)
            => StreamAsync<PriceTickerResult>(BuildUri("price", symbolCode, timeFrame), ct);

        public IAsyncEnumerable<PriceCandleResult> StreamCandlesAsync(
            string symbolCode, string timeFrame, CancellationToken ct = default)
            => StreamAsync<PriceCandleResult>(BuildUri("candles", symbolCode, timeFrame), ct);

        public IAsyncEnumerable<OrderFlowImbalanceResult> StreamOrderFlowImbalanceAsync(
            string symbolCode, string timeFrame, CancellationToken ct = default)
            => StreamAsync<OrderFlowImbalanceResult>(BuildUri("orderflow/imbalance", symbolCode, timeFrame), ct);

        public IAsyncEnumerable<MultiTimeframeResult> StreamMultiTimeframeAsync(
            string symbolCode, CancellationToken ct = default)
            => StreamAsync<MultiTimeframeResult>(BuildUri("multi-timeframe", symbolCode, null), ct);

        public IAsyncEnumerable<List<PriceMarketResult>> StreamMarketAsync(
            CancellationToken ct = default)
            => StreamAsync<List<PriceMarketResult>>(BuildUri("market", null, null), ct);

        private async IAsyncEnumerable<T> StreamAsync<T>(
            Uri uri,
            [EnumeratorCancellation] CancellationToken ct)
        {
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
                        var item = await JsonSerializer.DeserializeAsync<T>(ms, JsonOptions, ct);

                        if (item is not null)
                            yield return item;
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

        private Uri BuildUri(string path, string? symbolCode, string? timeFrame)
        {
            var baseUri = new Uri(options.BaseUrl);
            var scheme = baseUri.Scheme == "https" ? "wss" : "ws";
            var host = baseUri.IsDefaultPort
                ? baseUri.Host
                : $"{baseUri.Host}:{baseUri.Port}";

            var parameters = new Dictionary<string, string> { ["apiKey"] = options.ApiKey };
            if (symbolCode is not null) parameters["symbolCode"] = symbolCode;
            if (timeFrame is not null) parameters["timeFrame"] = timeFrame;

            var query = QueryBuilder.Build(parameters);
            return new Uri($"{scheme}://{host}/{path}?{query}");
        }
    }
}
