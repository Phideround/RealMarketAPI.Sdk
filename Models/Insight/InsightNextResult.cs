namespace RealTimeMarketAPI.Sdk.Models.Insight
{
    public sealed class InsightNextResult
    {
        public string SymbolCode { get; set; } = string.Empty;
        public string TimeFrame { get; set; } = string.Empty;
        public DateTimeOffset CalculatedAt { get; set; }
        public decimal Price { get; set; }
        public decimal Ema21 { get; set; }
        public decimal Ema50 { get; set; }
        public decimal Rsi { get; set; }
        public decimal Atr { get; set; }
        public decimal Volume { get; set; }
        public decimal AvgVolume { get; set; }
        public decimal Support { get; set; }
        public decimal Resistance { get; set; }
        public string Bias { get; set; } = string.Empty;
        public int BullScore { get; set; }
        public int BearScore { get; set; }
        public decimal TargetUp { get; set; }
        public decimal TargetDown { get; set; }
        public List<InsightSignal> Signals { get; set; } = [];
    }

    public sealed class InsightSignal
    {
        public string Name { get; set; } = string.Empty;
        public string Direction { get; set; } = string.Empty;
    }
}
