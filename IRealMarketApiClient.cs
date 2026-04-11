using RealTimeMarketAPI.Sdk.Clients;

namespace RealTimeMarketAPI.Sdk
{
    /// <summary>
    /// The main entry point for the RealMarket API SDK.
    /// </summary>
    public interface IRealMarketApiClient
    {
        /// <summary>Access real-time prices, candles, and historical data via REST.</summary>
        ITickerClient Ticker { get; }

        /// <summary>Access technical indicators via REST: SMA, EMA, RSI, MACD, Fibonacci, Support/Resistance.</summary>
        IIndicatorClient Indicators { get; }

        /// <summary>Access available trading symbols via REST.</summary>
        ISymbolClient Symbols { get; }

        /// <summary>Access volatility metrics, spike detection, and heatmap via REST.</summary>
        IVolatilityClient Volatility { get; }

        /// <summary>
        /// Stream real-time price ticks over a persistent WebSocket connection.
        /// <para>Requires a plan with WebSocket support enabled.</para>
        /// </summary>
        IWebSocketClient WebSocket { get; }

        /// <summary>
        /// Call the RealMarket MCP server — usable from AI assistants (GitHub Copilot,
        /// Claude, etc.) and from .NET code via this typed wrapper.
        /// <para>Endpoint: <c>https://api.realmarketapi.com/mcp</c></para>
        /// </summary>
        IMcpMarketClient Mcp { get; }
    }
}
