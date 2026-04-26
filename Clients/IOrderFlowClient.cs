using RealTimeMarketAPI.Sdk.Models.OrderFlow;

namespace RealTimeMarketAPI.Sdk.Clients
{
    /// <summary>Access the Order Flow module endpoints.</summary>
    public interface IOrderFlowClient
    {
        /// <summary>Measures order flow imbalance by analysing bullish vs bearish candle ratios over the last 50 candles.</summary>
        Task<OrderFlowImbalanceResult> GetImbalanceAsync(string symbolCode, string timeFrame, CancellationToken ct = default);
    }
}
