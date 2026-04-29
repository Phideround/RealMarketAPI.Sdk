namespace RealTimeMarketAPI.Sdk.Models.Ticker
{
    /// <summary>24-hour price statistics for a symbol.</summary>
    public sealed class Price24hrResult
    {
        public string SymbolCode { get; set; } = string.Empty;
        public decimal OpenPrice24h { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal HighPrice24h { get; set; }
        public decimal LowPrice24h { get; set; }
        public decimal Volume24h { get; set; }
        public decimal ChangePercent24h { get; set; }
        public DateTimeOffset OpenTime { get; set; }
    }
}
