namespace RealTimeMarketAPI.Sdk.Models.Ticker
{
    /// <summary>
    /// OHLCV candle data for a symbol.
    /// </summary>
    public class PriceCandleResult
    {
        public string SymbolCode { get; set; } = string.Empty;
        public decimal OpenPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public double Volume { get; set; }
        public DateTimeOffset OpenTime { get; set; }
    }
}
