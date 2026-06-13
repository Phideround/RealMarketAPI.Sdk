namespace RealTimeMarketAPI.Sdk.Models.Watchlist
{
    public class WatchlistResult
    {
        public string WatchlistId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = [];
        public int ItemCount { get; set; }
    }
}
