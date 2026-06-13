namespace RealTimeMarketAPI.Sdk.Models.StrategySignal
{
    public class StrategySignalResult
    {
        public string SymbolCode { get; set; } = string.Empty;
        public string TimeFrame { get; set; } = string.Empty;
        public string Signal { get; set; } = string.Empty;
        public decimal Confidence { get; set; }
        public string RiskLevel { get; set; } = string.Empty;
        public decimal InvalidationPrice { get; set; }
        public List<string> Reasons { get; set; } = [];
    }
}
