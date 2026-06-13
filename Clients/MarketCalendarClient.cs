using RealTimeMarketAPI.Sdk.Internal;
using RealTimeMarketAPI.Sdk.Models.MarketCalendar;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealTimeMarketAPI.Sdk.Clients
{
    internal sealed class MarketCalendarClient(HttpClient httpClient, string apiKey) : IMarketCalendarClient
    {
        private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

        public async Task<MarketCalendarResult> GetAsync(DateOnly? date = null, string timezone = "UTC", CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey,
                ["timezone"] = timezone,
                ["date"] = (date ?? DateOnly.FromDateTime(DateTime.UtcNow)).ToString("yyyy-MM-dd")
            });

            var result = await httpClient.GetFromJsonAsync<MarketCalendarResult>($"api/v1/market-calendar?{query}", JsonOptions, ct);
            return result!;
        }
    }
}
