namespace RealTimeMarketAPI.Sdk.Models.Manipulation
{
    public sealed class ManipulationRiskResult
    {
        public string SymbolCode { get; set; } = string.Empty;
        public string TimeFrame { get; set; } = string.Empty;
        public DateTimeOffset CalculatedAt { get; set; }
        public string RiskLevel { get; set; } = string.Empty;
        public int RiskScore { get; set; }
        public List<string> Factors { get; set; } = [];
        public decimal AvgWickToBodyRatio { get; set; }
        public decimal CurrentVolume { get; set; }
        public decimal AvgVolume { get; set; }
    }
}
