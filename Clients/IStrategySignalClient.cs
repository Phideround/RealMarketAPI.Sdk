using RealTimeMarketAPI.Sdk.Models.StrategySignal;

namespace RealTimeMarketAPI.Sdk.Clients
{
    public interface IStrategySignalClient
    {
        Task<StrategySignalResult> GetAsync(string symbolCode, string timeFrame, CancellationToken ct = default);
    }
}
