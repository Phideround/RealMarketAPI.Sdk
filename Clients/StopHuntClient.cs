using RealTimeMarketAPI.Sdk.Internal;
using RealTimeMarketAPI.Sdk.Models.StopHunt;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealTimeMarketAPI.Sdk.Clients
{
    internal sealed class StopHuntClient(HttpClient httpClient, string apiKey) : IStopHuntClient
    {
        private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

        public async Task<StopHuntZonesResult> GetZonesAsync(string symbolCode, string timeFrame, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey,
                ["symbolCode"] = symbolCode,
                ["timeFrame"] = timeFrame
            });
            var result = await httpClient.GetFromJsonAsync<StopHuntZonesResult>($"api/v1/stop-hunt/zones?{query}", JsonOptions, ct);
            return result!;
        }
    }
}
