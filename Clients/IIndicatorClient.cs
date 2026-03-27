using RealTimeMarketAPI.Sdk.Models.Common;
using RealTimeMarketAPI.Sdk.Models.Indicator;

namespace RealTimeMarketAPI.Sdk.Clients
{
    /// <summary>
    /// Provides access to technical indicator endpoints.
    /// </summary>
    public interface IIndicatorClient
    {
        /// <summary>
        /// Gets Simple Moving Average (SMA) values.
        /// <para>Endpoint: GET /api/v1/indicator/sma</para>
        /// </summary>
        /// <param name="symbolCode">Symbol code (e.g. "EURUSD").</param>
        /// <param name="timeFrame">Timeframe (e.g. "M1", "H1", "D1").</param>
        /// <param name="period">Number of periods (2–500).</param>
        Task<ListResult<IndicatorPoint>> GetSmaAsync(string symbolCode, string timeFrame, int period, CancellationToken ct = default);

        /// <summary>
        /// Gets Exponential Moving Average (EMA) values.
        /// <para>Endpoint: GET /api/v1/indicator/ema</para>
        /// </summary>
        /// <param name="symbolCode">Symbol code (e.g. "EURUSD").</param>
        /// <param name="timeFrame">Timeframe (e.g. "M1", "H1", "D1").</param>
        /// <param name="period">Number of periods (2–500).</param>
        Task<ListResult<IndicatorPoint>> GetEmaAsync(string symbolCode, string timeFrame, int period, CancellationToken ct = default);

        /// <summary>
        /// Gets Relative Strength Index (RSI) values.
        /// <para>Endpoint: GET /api/v1/indicator/rsi</para>
        /// </summary>
        /// <param name="symbolCode">Symbol code (e.g. "EURUSD").</param>
        /// <param name="timeFrame">Timeframe (e.g. "M1", "H1", "D1").</param>
        /// <param name="period">RSI period (default 14).</param>
        Task<ListResult<IndicatorPoint>> GetRsiAsync(string symbolCode, string timeFrame, int period = 14, CancellationToken ct = default);

        /// <summary>
        /// Gets Moving Average Convergence Divergence (MACD) values.
        /// <para>Endpoint: GET /api/v1/indicator/macd</para>
        /// </summary>
        /// <param name="symbolCode">Symbol code (e.g. "EURUSD").</param>
        /// <param name="timeFrame">Timeframe (e.g. "M1", "H1", "D1").</param>
        /// <param name="fastPeriod">Fast EMA period (default 12). Must be less than <paramref name="slowPeriod"/>.</param>
        /// <param name="slowPeriod">Slow EMA period (default 26).</param>
        /// <param name="signalPeriod">Signal line period (default 9).</param>
        Task<ListResult<MacdPoint>> GetMacdAsync(string symbolCode, string timeFrame, int fastPeriod = 12, int slowPeriod = 26, int signalPeriod = 9, CancellationToken ct = default);

        /// <summary>
        /// Gets support and resistance levels derived from historical price pivots.
        /// <para>Endpoint: GET /api/v1/indicator/support-resistance</para>
        /// </summary>
        /// <param name="symbolCode">Symbol code (e.g. "EURUSD").</param>
        /// <param name="timeFrame">Timeframe (e.g. "M1", "H1", "D1").</param>
        Task<SupportResistanceResult> GetSupportResistanceAsync(string symbolCode, string timeFrame, CancellationToken ct = default);

        /// <summary>
        /// Gets Fibonacci retracement and extension levels.
        /// <para>Endpoint: GET /api/v1/indicator/fibonacci</para>
        /// </summary>
        /// <param name="symbolCode">Symbol code (e.g. "EURUSD").</param>
        /// <param name="timeFrame">Timeframe (e.g. "M1", "H1", "D1").</param>
        /// <param name="lookback">Number of candles to look back (10–1000, default 100).</param>
        Task<ListResult<FibonacciLevel>> GetFibonacciAsync(string symbolCode, string timeFrame, int lookback = 100, CancellationToken ct = default);
    }
}
