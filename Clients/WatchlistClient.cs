using RealTimeMarketAPI.Sdk.Internal;
using RealTimeMarketAPI.Sdk.Models.Common;
using RealTimeMarketAPI.Sdk.Models.Watchlist;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealTimeMarketAPI.Sdk.Clients
{
    internal sealed class WatchlistClient(HttpClient httpClient, string apiKey) : IWatchlistClient
    {
        private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

        public async Task<WatchlistResult> CreateAsync(CreateWatchlistRequestModel request, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string> { ["apiKey"] = apiKey });
            var response = await httpClient.PostAsJsonAsync($"api/v1/watchlists?{query}", request, ct);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<WatchlistResult>(JsonOptions, ct);
            return result!;
        }

        public async Task<ListResult<WatchlistResult>> GetAsync(CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string> { ["apiKey"] = apiKey });
            var result = await httpClient.GetFromJsonAsync<ListResult<WatchlistResult>>($"api/v1/watchlists?{query}", JsonOptions, ct);
            return result!;
        }

        public async Task<WatchlistItemResult> AddItemAsync(string watchlistId, AddWatchlistItemRequestModel request, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string> { ["apiKey"] = apiKey });
            var encodedId = Uri.EscapeDataString(watchlistId);
            var response = await httpClient.PostAsJsonAsync($"api/v1/watchlists/{encodedId}/items?{query}", request, ct);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<WatchlistItemResult>(JsonOptions, ct);
            return result!;
        }

        public async Task<bool> RemoveItemAsync(string watchlistId, string symbolCode, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string> { ["apiKey"] = apiKey });
            var encodedId = Uri.EscapeDataString(watchlistId);
            var encodedSymbol = Uri.EscapeDataString(symbolCode);
            var response = await httpClient.DeleteAsync($"api/v1/watchlists/{encodedId}/items/{encodedSymbol}?{query}", ct);
            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
