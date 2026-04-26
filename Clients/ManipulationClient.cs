using RealTimeMarketAPI.Sdk.Internal;
using RealTimeMarketAPI.Sdk.Models.Manipulation;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealTimeMarketAPI.Sdk.Clients
{
    internal sealed class ManipulationClient(HttpClient httpClient, string apiKey) : IManipulationClient
    {
        private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

        public async Task<ManipulationRiskResult> GetRiskAsync(string symbolCode, string timeFrame, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey,
                ["symbolCode"] = symbolCode,
                ["timeFrame"] = timeFrame
            });
            var result = await httpClient.GetFromJsonAsync<ManipulationRiskResult>($"api/v1/manipulation-risk?{query}", JsonOptions, ct);
            return result!;
        }
    }
}
