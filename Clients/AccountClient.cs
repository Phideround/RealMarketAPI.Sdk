using RealTimeMarketAPI.Sdk.Internal;
using RealTimeMarketAPI.Sdk.Models.Account;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealTimeMarketAPI.Sdk.Clients
{
    internal sealed class AccountClient(HttpClient httpClient, string apiKey) : IAccountClient
    {
        private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

        public async Task<AccountMeResult> GetMeAsync(CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey
            });
            var result = await httpClient.GetFromJsonAsync<AccountMeResult>($"api/v1/me?{query}", JsonOptions, ct);
            return result!;
        }
    }
}
