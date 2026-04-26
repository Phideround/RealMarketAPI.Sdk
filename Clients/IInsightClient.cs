using RealTimeMarketAPI.Sdk.Models.Insight;

namespace RealTimeMarketAPI.Sdk.Clients
{
    /// <summary>Access the Insight module endpoints.</summary>
    public interface IInsightClient
    {
        /// <summary>Returns a full next-candle forecast with key indicators, bias scoring, and ATR-based price targets.</summary>
        Task<InsightNextResult> GetNextAsync(string symbolCode, string timeFrame, CancellationToken ct = default);

        /// <summary>Classifies the current trend direction using EMA alignment and ADX strength.</summary>
        Task<InsightTrendResult> GetTrendAsync(string symbolCode, string timeFrame, CancellationToken ct = default);

        /// <summary>Detects the current market setup pattern: breakout, pullback, or range-bound.</summary>
        Task<InsightSetupResult> GetSetupAsync(string symbolCode, string timeFrame, CancellationToken ct = default);

        /// <summary>Combines RSI, moving averages, and S/R into a single actionable signal with confidence strength.</summary>
        Task<InsightConfluenceResult> GetConfluenceAsync(string symbolCode, string timeFrame, CancellationToken ct = default);

        /// <summary>Returns a composite 0–100 bullish/bearish strength score built from five components.</summary>
        Task<InsightScoreResult> GetScoreAsync(string symbolCode, string timeFrame, CancellationToken ct = default);
    }
}
