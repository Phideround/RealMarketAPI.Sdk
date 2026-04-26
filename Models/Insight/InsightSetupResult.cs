namespace RealTimeMarketAPI.Sdk.Models.Insight
{
    public sealed class InsightSetupResult
    {
        public string SymbolCode { get; set; } = string.Empty;
        public string TimeFrame { get; set; } = string.Empty;
        public DateTimeOffset CalculatedAt { get; set; }
        public string Setup { get; set; } = string.Empty;
        public string Direction { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal NearestSupport { get; set; }
        public decimal NearestResistance { get; set; }
    }
}
