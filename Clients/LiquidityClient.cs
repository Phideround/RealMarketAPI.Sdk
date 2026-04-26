using RealTimeMarketAPI.Sdk.Internal;
using RealTimeMarketAPI.Sdk.Models.Liquidity;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealTimeMarketAPI.Sdk.Clients
{
    internal sealed class LiquidityClient(HttpClient httpClient, string apiKey) : ILiquidityClient
    {
        private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

        public async Task<LiquidityZonesResult> GetZonesAsync(string symbolCode, string timeFrame, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey,
                ["symbolCode"] = symbolCode,
                ["timeFrame"] = timeFrame
            });
            var result = await httpClient.GetFromJsonAsync<LiquidityZonesResult>($"api/v1/liquidity/zones?{query}", JsonOptions, ct);
            return result!;
        }
    }
}
