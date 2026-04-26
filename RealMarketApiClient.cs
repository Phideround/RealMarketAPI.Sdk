using RealTimeMarketAPI.Sdk.Clients;

namespace RealTimeMarketAPI.Sdk
{
    /// <summary>
    /// Default implementation of <see cref="IRealMarketApiClient"/>.
    /// Register via <c>services.AddRealMarketApiClient("your-api-key")</c>.
    /// </summary>
    public sealed class RealMarketApiClient(HttpClient httpClient, RealMarketApiOptions options) : IRealMarketApiClient
    {
        /// <inheritdoc/>
        public ITickerClient Ticker { get; } = new TickerClient(httpClient, options.ApiKey);

        /// <inheritdoc/>
        public IIndicatorClient Indicators { get; } = new IndicatorClient(httpClient, options.ApiKey);

        /// <inheritdoc/>
        public ISymbolClient Symbols { get; } = new SymbolClient(httpClient);

        /// <inheritdoc/>
        public IVolatilityClient Volatility { get; } = new VolatilityClient(httpClient, options.ApiKey);

        /// <inheritdoc/>
        public IWebSocketClient WebSocket { get; } = new WebSocketClient(options);

        /// <inheritdoc/>
        public IMcpMarketClient Mcp { get; } = new McpMarketClient(httpClient, options);

        /// <inheritdoc/>
        public IInsightClient Insight { get; } = new InsightClient(httpClient, options.ApiKey);

        /// <inheritdoc/>
        public IMultiTimeframeClient MultiTimeframe { get; } = new MultiTimeframeClient(httpClient, options.ApiKey);

        /// <inheritdoc/>
        public ILiquidityClient Liquidity { get; } = new LiquidityClient(httpClient, options.ApiKey);

        /// <inheritdoc/>
        public IOrderFlowClient OrderFlow { get; } = new OrderFlowClient(httpClient, options.ApiKey);

        /// <inheritdoc/>
        public IStopHuntClient StopHunt { get; } = new StopHuntClient(httpClient, options.ApiKey);

        /// <inheritdoc/>
        public IAnomalyClient Anomaly { get; } = new AnomalyClient(httpClient, options.ApiKey);

        /// <inheritdoc/>
        public IManipulationClient Manipulation { get; } = new ManipulationClient(httpClient, options.ApiKey);
    }
}
