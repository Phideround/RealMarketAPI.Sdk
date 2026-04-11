using RealTimeMarketAPI.Sdk.Models.Common;
using RealTimeMarketAPI.Sdk.Models.Volatility;

namespace RealTimeMarketAPI.Sdk.Clients
{
    /// <summary>
    /// Provides access to volatility endpoints.
    /// </summary>
    public interface IVolatilityClient
    {
        /// <summary>
        /// Gets a time-series of volatility metrics (ATR, ATR%, Band Width, Historical Volatility).
        /// <para>Endpoint: GET /api/v1/volatility</para>
        /// </summary>
        /// <param name="symbolCode">Symbol code (e.g. "BTCUSDT").</param>
        /// <param name="timeFrame">Timeframe (e.g. "H1", "D1").</param>
        /// <param name="period">Lookback window (2–500, default 14).</param>
        Task<ListResult<VolatilityPoint>> GetVolatilityAsync(string symbolCode, string timeFrame, int period = 14, CancellationToken ct = default);

        /// <summary>
        /// Gets candles where ATR exceeded a given multiple of the series average (volatility spikes).
        /// <para>Endpoint: GET /api/v1/volatility/spikes</para>
        /// </summary>
        /// <param name="symbolCode">Symbol code (e.g. "BTCUSDT").</param>
        /// <param name="timeFrame">Timeframe (e.g. "H1", "D1").</param>
        /// <param name="period">ATR lookback period (2–500, default 14).</param>
        /// <param name="spikeMultiplier">Minimum ATR-to-average ratio to classify as a spike (1.1–10.0, default 2.0).</param>
        Task<ListResult<VolatilitySpikePoint>> GetSpikesAsync(string symbolCode, string timeFrame, int period = 14, decimal spikeMultiplier = 2.0m, CancellationToken ct = default);

        /// <summary>
        /// Gets a Day-of-Week × Hour-of-Day volatility heatmap.
        /// <para>Endpoint: GET /api/v1/volatility/heatmap</para>
        /// </summary>
        /// <param name="symbolCode">Symbol code (e.g. "BTCUSDT").</param>
        /// <param name="timeFrame">Timeframe (e.g. "H1", "D1").</param>
        Task<ListResult<VolatilityHeatmapPoint>> GetHeatmapAsync(string symbolCode, string timeFrame, CancellationToken ct = default);
    }
}
