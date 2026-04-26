using RealTimeMarketAPI.Sdk.Models.Liquidity;

namespace RealTimeMarketAPI.Sdk.Clients
{
    /// <summary>Access the Liquidity module endpoints.</summary>
    public interface ILiquidityClient
    {
        /// <summary>Returns key support and resistance clusters acting as liquidity pools, ordered by proximity to the current price.</summary>
        Task<LiquidityZonesResult> GetZonesAsync(string symbolCode, string timeFrame, CancellationToken ct = default);
    }
}
