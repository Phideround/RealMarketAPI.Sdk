using RealTimeMarketAPI.Sdk.Clients;

namespace RealTimeMarketAPI.Sdk
{
    /// <summary>
    /// Default implementation of <see cref="IRealMarketApiClient"/>.
    /// Register via <c>services.AddRealMarketApiClient("your-api-key")</c>.
    /// </summary>
    public sealed class RealMarketApiClient : IRealMarketApiClient
    {
        /// <inheritdoc/>
        public ITickerClient Ticker { get; }

        /// <inheritdoc/>
        public IIndicatorClient Indicators { get; }

        /// <inheritdoc/>
        public ISymbolClient Symbols { get; }

        /// <inheritdoc/>
        public IWebSocketClient WebSocket { get; }

        /// <inheritdoc/>
        public IMcpMarketClient Mcp { get; }

        public RealMarketApiClient(HttpClient httpClient, RealMarketApiOptions options)
        {
            Ticker = new TickerClient(httpClient, options.ApiKey);
            Indicators = new IndicatorClient(httpClient, options.ApiKey);
            Symbols = new SymbolClient(httpClient);
            WebSocket = new WebSocketClient(options);
            Mcp = new McpMarketClient(httpClient, options);
        }
    }
}
