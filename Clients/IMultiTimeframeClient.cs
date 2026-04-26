using RealTimeMarketAPI.Sdk.Models.MultiTimeframe;

namespace RealTimeMarketAPI.Sdk.Clients
{
    /// <summary>Access the Multi-Timeframe module endpoints.</summary>
    public interface IMultiTimeframeClient
    {
        /// <summary>Returns the trend direction for every timeframe allowed by your plan in a single call.</summary>
        Task<MultiTimeframeResult> GetAsync(string symbolCode, CancellationToken ct = default);
    }
}
