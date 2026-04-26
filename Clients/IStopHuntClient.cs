using RealTimeMarketAPI.Sdk.Models.StopHunt;

namespace RealTimeMarketAPI.Sdk.Clients
{
    /// <summary>Access the Stop Hunt module endpoints.</summary>
    public interface IStopHuntClient
    {
        /// <summary>Identifies price levels just beyond key S/R where retail stop orders are likely clustered.</summary>
        Task<StopHuntZonesResult> GetZonesAsync(string symbolCode, string timeFrame, CancellationToken ct = default);
    }
}
