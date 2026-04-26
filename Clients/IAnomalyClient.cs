using RealTimeMarketAPI.Sdk.Models.Anomaly;

namespace RealTimeMarketAPI.Sdk.Clients
{
    /// <summary>Access the Anomaly module endpoints.</summary>
    public interface IAnomalyClient
    {
        /// <summary>Scans the last 50 candles for price spikes, unusual volume, and fake breakouts.</summary>
        Task<AnomalyResult> GetAsync(string symbolCode, string timeFrame, CancellationToken ct = default);
    }
}
