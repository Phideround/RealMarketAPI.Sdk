namespace RealTimeMarketAPI.Sdk.Models.Insight
{
    public sealed class InsightScoreResult
    {
        public string SymbolCode { get; set; } = string.Empty;
        public string TimeFrame { get; set; } = string.Empty;
        public DateTimeOffset CalculatedAt { get; set; }
        public int Score { get; set; }
        public string Label { get; set; } = string.Empty;
        public decimal Rsi { get; set; }
        public decimal MacdHistogram { get; set; }
        public decimal Adx { get; set; }
        public decimal Price { get; set; }
        public decimal Ema21 { get; set; }
        public decimal Ema50 { get; set; }
        public List<string> Factors { get; set; } = [];
    }
}
