using RealTimeMarketAPI.Sdk.Internal;
using RealTimeMarketAPI.Sdk.Models.Common;
using RealTimeMarketAPI.Sdk.Models.Ticker;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealTimeMarketAPI.Sdk.Clients
{
    internal sealed class TickerClient(HttpClient httpClient, string apiKey) : ITickerClient
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<PriceTickerResult> GetPriceAsync(string symbolCode, string timeFrame, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey,
                ["symbolCode"] = symbolCode,
                ["timeFrame"] = timeFrame
            });

            var result = await httpClient.GetFromJsonAsync<PriceTickerResult>($"api/v1/price?{query}", JsonOptions, ct);
            return result!;
        }

        public async Task<ListResult<PriceMarketResult>> GetMarketPricesAsync(CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey
            });

            var result = await httpClient.GetFromJsonAsync<ListResult<PriceMarketResult>>($"api/v1/price/market?{query}", JsonOptions, ct);
            return result!;
        }

        public async Task<ListResult<PriceMarketResult>> GetPriceByCategoryAsync(string category, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey,
                ["category"] = category
            });

            var result = await httpClient.GetFromJsonAsync<ListResult<PriceMarketResult>>($"api/v1/price/category?{query}", JsonOptions, ct);
            return result!;
        }

        public async Task<ListResult<Price24hrResult>> Get24hrStatsAsync(CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey
            });

            var result = await httpClient.GetFromJsonAsync<ListResult<Price24hrResult>>($"api/v1/price/24hr?{query}", JsonOptions, ct);
            return result!;
        }

        public async Task<ListResult<PriceCandleResult>> GetCandlesAsync(string symbolCode, string timeFrame, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey,
                ["symbolCode"] = symbolCode,
                ["timeFrame"] = timeFrame
            });

            var result = await httpClient.GetFromJsonAsync<ListResult<PriceCandleResult>>($"api/v1/candle?{query}", JsonOptions, ct);
            return result!;
        }

        public async Task<PagedResult<PriceTickerResult>> GetHistoryAsync(
            string symbolCode,
            DateTimeOffset startTime,
            DateTimeOffset endTime,
            int pageNumber = 1,
            int pageSize = 20,
            CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey,
                ["symbolCode"] = symbolCode,
                ["startTime"] = startTime.ToString("O"),
                ["endTime"] = endTime.ToString("O"),
                ["pageNumber"] = pageNumber.ToString(),
                ["pageSize"] = pageSize.ToString()
            });

            var result = await httpClient.GetFromJsonAsync<PagedResult<PriceTickerResult>>($"api/v1/history?{query}", JsonOptions, ct);
            return result!;
        }
    }
}
