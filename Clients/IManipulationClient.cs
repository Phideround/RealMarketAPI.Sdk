using RealTimeMarketAPI.Sdk.Models.Manipulation;

namespace RealTimeMarketAPI.Sdk.Clients
{
    /// <summary>Access the Manipulation Risk module endpoints.</summary>
    public interface IManipulationClient
    {
        /// <summary>Assesses the probability of price manipulation based on wick-to-body ratio, volume divergence, and fake breakout frequency.</summary>
        Task<ManipulationRiskResult> GetRiskAsync(string symbolCode, string timeFrame, CancellationToken ct = default);
    }
}
