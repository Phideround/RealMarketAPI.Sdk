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

        /// <summary>Access account and plan information.</summary>
        IAccountClient Account { get; }

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

        /// <summary>Access Insight module endpoints: next-candle forecast, trend, setup, confluence, and score.</summary>
        IInsightClient Insight { get; }

        /// <summary>Access Multi-Timeframe module — returns trend direction across all plan-allowed timeframes in one call.</summary>
        IMultiTimeframeClient MultiTimeframe { get; }

        /// <summary>Access Liquidity module — key support/resistance clusters ordered by proximity.</summary>
        ILiquidityClient Liquidity { get; }

        /// <summary>Access Order Flow module — measures bullish/bearish candle imbalance over the last 50 candles.</summary>
        IOrderFlowClient OrderFlow { get; }

        /// <summary>Access Stop Hunt module — identifies stop-cluster zones beyond key S/R levels.</summary>
        IStopHuntClient StopHunt { get; }

        /// <summary>Access Anomaly module — scans for price spikes, unusual volume, and fake breakouts.</summary>
        IAnomalyClient Anomaly { get; }

        /// <summary>Access Manipulation Risk module — assesses manipulation probability from wick ratio, volume divergence, and fake breakouts.</summary>
        IManipulationClient Manipulation { get; }
    }
}
