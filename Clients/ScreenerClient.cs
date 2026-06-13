using RealTimeMarketAPI.Sdk.Internal;
using RealTimeMarketAPI.Sdk.Models.Common;
using RealTimeMarketAPI.Sdk.Models.Screener;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealTimeMarketAPI.Sdk.Clients
{
    internal sealed class ScreenerClient(HttpClient httpClient, string apiKey) : IScreenerClient
    {
        private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

        public async Task<ListResult<ScreenerResult>> QueryAsync(ScreenerQueryRequestModel request, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string> { ["apiKey"] = apiKey });
            var response = await httpClient.PostAsJsonAsync($"api/v1/screener/query?{query}", request, ct);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ListResult<ScreenerResult>>(JsonOptions, ct);
            return result!;
        }
    }
}
