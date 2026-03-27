namespace RealTimeMarketAPI.Sdk.Models.Ticker
{
    /// <summary>
    /// Market overview for a symbol including change percentages.
    /// </summary>
    public class PriceMarketResult
    {
        public string SymbolCode { get; set; } = string.Empty;
        public decimal ClosePrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public double Volume { get; set; }
        public List<double> HistoryVolumes { get; set; } = [];
        public decimal HourlyChangePercent { get; set; }
        public decimal DailyChangePercent { get; set; }
    }
}
