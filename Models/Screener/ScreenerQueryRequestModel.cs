namespace RealTimeMarketAPI.Sdk.Models.Screener
{
    public class ScreenerQueryRequestModel
    {
        public string TimeFrame { get; set; } = "H1";
        public string? Trend { get; set; }
        public decimal? MinRsi { get; set; }
        public decimal? MaxVolatilityPct { get; set; }
        public decimal? MinLiquidityScore { get; set; }
        public string SortField { get; set; } = "SignalScore";
        public string SortDirection { get; set; } = "Desc";
        public int Size { get; set; } = 25;
    }
}
