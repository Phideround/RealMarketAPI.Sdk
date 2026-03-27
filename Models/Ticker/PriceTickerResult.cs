namespace RealTimeMarketAPI.Sdk.Models.Ticker
{
    /// <summary>
    /// Real-time price ticker with bid/ask spread.
    /// </summary>
    public class PriceTickerResult
    {
        public string SymbolCode { get; set; } = string.Empty;
        public decimal OpenPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public double Volume { get; set; }
        public DateTimeOffset OpenTime { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
    }
}
