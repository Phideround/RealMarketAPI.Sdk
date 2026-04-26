namespace RealTimeMarketAPI.Sdk.Models.Insight
{
    public sealed class InsightTrendResult
    {
        public string SymbolCode { get; set; } = string.Empty;
        public string TimeFrame { get; set; } = string.Empty;
        public DateTimeOffset CalculatedAt { get; set; }
        public string Trend { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Adx { get; set; }
        public decimal Ema21 { get; set; }
        public decimal Ema50 { get; set; }
        public decimal Price { get; set; }
    }
}
