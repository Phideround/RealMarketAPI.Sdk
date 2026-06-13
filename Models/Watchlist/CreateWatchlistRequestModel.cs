namespace RealTimeMarketAPI.Sdk.Models.Watchlist
{
    public class CreateWatchlistRequestModel
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = [];
        public string? Notes { get; set; }
    }
}
