using RealTimeMarketAPI.Sdk.Internal;
using RealTimeMarketAPI.Sdk.Models.Common;
using RealTimeMarketAPI.Sdk.Models.Indicator;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealTimeMarketAPI.Sdk.Clients
{
    internal sealed class IndicatorClient(HttpClient httpClient, string apiKey) : IIndicatorClient
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<ListResult<IndicatorPoint>> GetSmaAsync(string symbolCode, string timeFrame, int period, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey,
                ["symbolCode"] = symbolCode,
                ["timeFrame"] = timeFrame,
                ["period"] = period.ToString()
            });

            var result = await httpClient.GetFromJsonAsync<ListResult<IndicatorPoint>>($"api/v1/indicator/sma?{query}", JsonOptions, ct);
            return result!;
        }

        public async Task<ListResult<IndicatorPoint>> GetEmaAsync(string symbolCode, string timeFrame, int period, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey,
                ["symbolCode"] = symbolCode,
                ["timeFrame"] = timeFrame,
                ["period"] = period.ToString()
            });

            var result = await httpClient.GetFromJsonAsync<ListResult<IndicatorPoint>>($"api/v1/indicator/ema?{query}", JsonOptions, ct);
            return result!;
        }

        public async Task<ListResult<IndicatorPoint>> GetRsiAsync(string symbolCode, string timeFrame, int period = 14, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey,
                ["symbolCode"] = symbolCode,
                ["timeFrame"] = timeFrame,
                ["period"] = period.ToString()
            });

            var result = await httpClient.GetFromJsonAsync<ListResult<IndicatorPoint>>($"api/v1/indicator/rsi?{query}", JsonOptions, ct);
            return result!;
        }

        public async Task<ListResult<MacdPoint>> GetMacdAsync(string symbolCode, string timeFrame, int fastPeriod = 12, int slowPeriod = 26, int signalPeriod = 9, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey,
                ["symbolCode"] = symbolCode,
                ["timeFrame"] = timeFrame,
                ["fastPeriod"] = fastPeriod.ToString(),
                ["slowPeriod"] = slowPeriod.ToString(),
                ["signalPeriod"] = signalPeriod.ToString()
            });

            var result = await httpClient.GetFromJsonAsync<ListResult<MacdPoint>>($"api/v1/indicator/macd?{query}", JsonOptions, ct);
            return result!;
        }

        public async Task<SupportResistanceResult> GetSupportResistanceAsync(string symbolCode, string timeFrame, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey,
                ["symbolCode"] = symbolCode,
                ["timeFrame"] = timeFrame
            });

            var result = await httpClient.GetFromJsonAsync<SupportResistanceResult>($"api/v1/indicator/support-resistance?{query}", JsonOptions, ct);
            return result!;
        }

        public async Task<ListResult<FibonacciLevel>> GetFibonacciAsync(string symbolCode, string timeFrame, int lookback = 100, CancellationToken ct = default)
        {
            var query = QueryBuilder.Build(new Dictionary<string, string>
            {
                ["apiKey"] = apiKey,
                ["symbolCode"] = symbolCode,
                ["timeFrame"] = timeFrame,
                ["lookback"] = lookback.ToString()
            });

            var result = await httpClient.GetFromJsonAsync<ListResult<FibonacciLevel>>($"api/v1/indicator/fibonacci?{query}", JsonOptions, ct);
            return result!;
        }
    }
}
