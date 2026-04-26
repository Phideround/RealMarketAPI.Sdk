using RealTimeMarketAPI.Sdk.Internal;
using RealTimeMarketAPI.Sdk.Models.MultiTimeframe;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealTimeMarketAPI.Sdk.Clients
{
    internal sealed class MultiTimeframeClient(HttpClient httpClient, string apiKey) : IMultiTimeframeClient
    {
        private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

        public async Task<MultiTimeframeResult> GetAsync(string symbolCode, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey,
                ["symbolCode"] = symbolCode
            });
            var result = await httpClient.GetFromJsonAsync<MultiTimeframeResult>($"api/v1/multi-timeframe?{query}", JsonOptions, ct);
            return result!;
        }
    }
}
