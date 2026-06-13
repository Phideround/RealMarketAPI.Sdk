namespace RealTimeMarketAPI.Sdk.Models.Screener
{
    public class ScreenerResult
    {
        public string SymbolCode { get; set; } = string.Empty;
        public string Trend { get; set; } = string.Empty;
        public decimal SignalScore { get; set; }
        public decimal Rsi { get; set; }
        public decimal VolatilityPct { get; set; }
        public decimal LiquidityScore { get; set; }
    }
}
