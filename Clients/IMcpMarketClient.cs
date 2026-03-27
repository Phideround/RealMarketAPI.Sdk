using RealTimeMarketAPI.Sdk.Models.Common;
using RealTimeMarketAPI.Sdk.Models.Indicator;
using RealTimeMarketAPI.Sdk.Models.Symbol;
using RealTimeMarketAPI.Sdk.Models.Ticker;

namespace RealTimeMarketAPI.Sdk.Clients
{
    /// <summary>
    /// Provides access to the RealMarket MCP (Model Context Protocol) server.
    /// All tools are callable from AI assistants (GitHub Copilot, Claude, etc.)
    /// and from .NET code via this typed wrapper.
    /// <para>MCP endpoint: <c>https://api.realmarketapi.com/mcp</c></para>
    /// </summary>
    public interface IMcpMarketClient
    {
        // ── Market data ────────────────────────────────────────────────────────

        /// <summary>Latest real-time price ticker. MCP tool: <c>get_price</c></summary>
        Task<PriceTickerResult> GetPriceAsync(string symbolCode, string timeFrame, CancellationToken ct = default);

        /// <summary>Latest 10 OHLCV candles. MCP tool: <c>get_candles</c></summary>
        Task<ListResult<PriceCandleResult>> GetCandlesAsync(string symbolCode, string timeFrame, CancellationToken ct = default);

        /// <summary>Paginated historical candles. MCP tool: <c>get_history</c></summary>
        Task<PagedResult<PriceTickerResult>> GetHistoryAsync(
            string symbolCode,
            DateTimeOffset startTime,
            DateTimeOffset endTime,
            int pageNumber = 1,
            int pageSize = 50,
            CancellationToken ct = default);

        // ── Reference data ─────────────────────────────────────────────────────

        /// <summary>All available trading symbols, filtered to your plan. MCP tool: <c>get_symbols</c></summary>
        Task<ListResult<SymbolInfo>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>Timeframe codes supported by your plan. MCP tool: <c>get_timeframes</c></summary>
        Task<IEnumerable<string>> GetTimeframesAsync(CancellationToken ct = default);

        // ── Indicators (PRO plan and above) ────────────────────────────────────

        /// <summary>Simple Moving Average. MCP tool: <c>get_sma</c></summary>
        Task<ListResult<IndicatorPoint>> GetSmaAsync(string symbolCode, string timeFrame, int period = 20, CancellationToken ct = default);

        /// <summary>Exponential Moving Average. MCP tool: <c>get_ema</c></summary>
        Task<ListResult<IndicatorPoint>> GetEmaAsync(string symbolCode, string timeFrame, int period = 20, CancellationToken ct = default);

        /// <summary>Relative Strength Index. MCP tool: <c>get_rsi</c></summary>
        Task<ListResult<IndicatorPoint>> GetRsiAsync(string symbolCode, string timeFrame, int period = 14, CancellationToken ct = default);

        /// <summary>MACD line, signal, and histogram. MCP tool: <c>get_macd</c></summary>
        Task<ListResult<MacdPoint>> GetMacdAsync(string symbolCode, string timeFrame, int fastPeriod = 12, int slowPeriod = 26, int signalPeriod = 9, CancellationToken ct = default);

        /// <summary>Support and resistance price levels. MCP tool: <c>get_support_resistance</c></summary>
        Task<SupportResistanceResult> GetSupportResistanceAsync(string symbolCode, string timeFrame, CancellationToken ct = default);

        /// <summary>Fibonacci retracement and extension levels. MCP tool: <c>get_fibonacci</c></summary>
        Task<ListResult<FibonacciLevel>> GetFibonacciAsync(string symbolCode, string timeFrame, int lookback = 100, CancellationToken ct = default);
    }
}
