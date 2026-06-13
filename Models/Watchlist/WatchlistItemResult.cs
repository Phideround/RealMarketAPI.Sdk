namespace RealTimeMarketAPI.Sdk.Models.Watchlist
{
    public class WatchlistItemResult
    {
        public string WatchlistId { get; set; } = string.Empty;
        public string SymbolCode { get; set; } = string.Empty;
        public int Order { get; set; }
    }
}
