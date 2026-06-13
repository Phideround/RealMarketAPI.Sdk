using RealTimeMarketAPI.Sdk.Internal;
using RealTimeMarketAPI.Sdk.Models.Alert;
using RealTimeMarketAPI.Sdk.Models.Common;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealTimeMarketAPI.Sdk.Clients
{
    internal sealed class AlertClient(HttpClient httpClient, string apiKey) : IAlertClient
    {
        private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

        public async Task<CreateAlertResult> CreateAsync(CreateAlertRequestModel request, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string> { ["apiKey"] = apiKey });
            var response = await httpClient.PostAsJsonAsync($"api/v1/alerts?{query}", request, ct);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<CreateAlertResult>(JsonOptions, ct);
            return result!;
        }

        public async Task<ListResult<AlertResult>> GetAsync(string? status = null, CancellationToken ct = default)
        {
            var dict = new Dictionary<string, string> { ["apiKey"] = apiKey };
            if (!string.IsNullOrWhiteSpace(status))
            {
                dict["status"] = status;
            }

            var query = QueryBuilder.Build(dict);
            var result = await httpClient.GetFromJsonAsync<ListResult<AlertResult>>($"api/v1/alerts?{query}", JsonOptions, ct);
            return result!;
        }

        public async Task<DeleteAlertResult> DeleteAsync(string alertId, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string> { ["apiKey"] = apiKey });
            var response = await httpClient.DeleteAsync($"api/v1/alerts/{Uri.EscapeDataString(alertId)}?{query}", ct);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<DeleteAlertResult>(JsonOptions, ct);
            return result!;
        }
    }
}
