namespace RealTimeMarketAPI.Sdk.Models.Watchlist
{
    public class AddWatchlistItemRequestModel
    {
        public string SymbolCode { get; set; } = string.Empty;
        public int Order { get; set; }
    }
}
