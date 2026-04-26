using RealTimeMarketAPI.Sdk.Internal;
using RealTimeMarketAPI.Sdk.Models.OrderFlow;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealTimeMarketAPI.Sdk.Clients
{
    internal sealed class OrderFlowClient(HttpClient httpClient, string apiKey) : IOrderFlowClient
    {
        private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

        public async Task<OrderFlowImbalanceResult> GetImbalanceAsync(string symbolCode, string timeFrame, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey,
                ["symbolCode"] = symbolCode,
                ["timeFrame"] = timeFrame
            });
            var result = await httpClient.GetFromJsonAsync<OrderFlowImbalanceResult>($"api/v1/orderflow/imbalance?{query}", JsonOptions, ct);
            return result!;
        }
    }
}
