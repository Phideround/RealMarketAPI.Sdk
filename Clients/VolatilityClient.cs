using RealTimeMarketAPI.Sdk.Internal;
using RealTimeMarketAPI.Sdk.Models.Common;
using RealTimeMarketAPI.Sdk.Models.Volatility;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealTimeMarketAPI.Sdk.Clients
{
    internal sealed class VolatilityClient(HttpClient httpClient, string apiKey) : IVolatilityClient
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<ListResult<VolatilityPoint>> GetVolatilityAsync(string symbolCode, string timeFrame, int period = 14, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey,
                ["symbolCode"] = symbolCode,
                ["timeFrame"] = timeFrame,
                ["period"] = period.ToString()
            });

            var result = await httpClient.GetFromJsonAsync<ListResult<VolatilityPoint>>($"api/v1/volatility?{query}", JsonOptions, ct);
            return result!;
        }

        public async Task<ListResult<VolatilitySpikePoint>> GetSpikesAsync(string symbolCode, string timeFrame, int period = 14, decimal spikeMultiplier = 2.0m, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey,
                ["symbolCode"] = symbolCode,
                ["timeFrame"] = timeFrame,
                ["period"] = period.ToString(),
                ["spikeMultiplier"] = spikeMultiplier.ToString(System.Globalization.CultureInfo.InvariantCulture)
            });

            var result = await httpClient.GetFromJsonAsync<ListResult<VolatilitySpikePoint>>($"api/v1/volatility/spikes?{query}", JsonOptions, ct);
            return result!;
        }

        public async Task<ListResult<VolatilityHeatmapPoint>> GetHeatmapAsync(string symbolCode, string timeFrame, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey,
                ["symbolCode"] = symbolCode,
                ["timeFrame"] = timeFrame
            });

            var result = await httpClient.GetFromJsonAsync<ListResult<VolatilityHeatmapPoint>>($"api/v1/volatility/heatmap?{query}", JsonOptions, ct);
            return result!;
        }
    }
}
