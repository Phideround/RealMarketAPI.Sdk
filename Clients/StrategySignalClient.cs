using RealTimeMarketAPI.Sdk.Internal;
using RealTimeMarketAPI.Sdk.Models.StrategySignal;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealTimeMarketAPI.Sdk.Clients
{
    internal sealed class StrategySignalClient(HttpClient httpClient, string apiKey) : IStrategySignalClient
    {
        private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

        public async Task<StrategySignalResult> GetAsync(string symbolCode, string timeFrame, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey,
                ["symbolCode"] = symbolCode,
                ["timeFrame"] = timeFrame
            });

            var result = await httpClient.GetFromJsonAsync<StrategySignalResult>($"api/v1/signals/strategy?{query}", JsonOptions, ct);
            return result!;
        }
    }
}
