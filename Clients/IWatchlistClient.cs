using RealTimeMarketAPI.Sdk.Models.Common;
using RealTimeMarketAPI.Sdk.Models.Watchlist;

namespace RealTimeMarketAPI.Sdk.Clients
{
    public interface IWatchlistClient
    {
        Task<WatchlistResult> CreateAsync(CreateWatchlistRequestModel request, CancellationToken ct = default);
        Task<ListResult<WatchlistResult>> GetAsync(CancellationToken ct = default);
        Task<WatchlistItemResult> AddItemAsync(string watchlistId, AddWatchlistItemRequestModel request, CancellationToken ct = default);
        Task<bool> RemoveItemAsync(string watchlistId, string symbolCode, CancellationToken ct = default);
    }
}
