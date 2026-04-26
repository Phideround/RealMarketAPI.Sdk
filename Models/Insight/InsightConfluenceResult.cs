namespace RealTimeMarketAPI.Sdk.Models.Insight
{
    public sealed class InsightConfluenceResult
    {
        public string SymbolCode { get; set; } = string.Empty;
        public string TimeFrame { get; set; } = string.Empty;
        public DateTimeOffset CalculatedAt { get; set; }
        public string Signal { get; set; } = string.Empty;
        public string Strength { get; set; } = string.Empty;
        public int Score { get; set; }
        public List<string> Reasons { get; set; } = [];
        public decimal Rsi { get; set; }
        public decimal Ema21 { get; set; }
        public decimal Ema50 { get; set; }
        public decimal Price { get; set; }
        public decimal NearestSupport { get; set; }
        public decimal NearestResistance { get; set; }
    }
}
