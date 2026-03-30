using RealTimeMarketAPI.Sdk.Models.Common;
using RealTimeMarketAPI.Sdk.Models.Indicator;
using RealTimeMarketAPI.Sdk.Models.Symbol;
using RealTimeMarketAPI.Sdk.Models.Ticker;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace RealTimeMarketAPI.Sdk.Clients
{
    /// <summary>
    /// Typed MCP client that speaks the JSON-RPC + Streamable HTTP / SSE transport
    /// directly — no external MCP package required.
    /// </summary>
    internal sealed class McpMarketClient(HttpClient httpClient, RealMarketApiOptions options) : IMcpMarketClient
    {
        private readonly string _apiKey = options.ApiKey;

        // Session state — lazily initialized on first call
        private string _sessionId;
        private readonly SemaphoreSlim _initLock = new(1, 1);

        private static readonly JsonSerializerOptions JsonOptions =
            new() { PropertyNameCaseInsensitive = true };

        private static readonly JsonSerializerOptions RequestOptions =
            new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        private const string McpPath = "mcp";
        private const string McpProtocolVersion = "2025-03-26";

        // ── Market data ────────────────────────────────────────────────────────

        public Task<PriceTickerResult> GetPriceAsync(string symbolCode, string timeFrame, CancellationToken ct = default)
            => CallToolAsync<PriceTickerResult>("get_price", new
            {
                symbolCode,
                timeFrame,
                apiKey = _apiKey
            }, ct);

        public async Task<ListResult<PriceCandleResult>> GetCandlesAsync(string symbolCode, string timeFrame, CancellationToken ct = default)
        {
            var items = await CallToolAsync<List<PriceCandleResult>>("get_candles", new
            {
                symbolCode,
                timeFrame,
                apiKey = _apiKey
            }, ct);
            return ToListResult(items);
        }

        public async Task<PagedResult<PriceTickerResult>> GetHistoryAsync(
            string symbolCode,
            DateTimeOffset startTime,
            DateTimeOffset endTime,
            int pageNumber = 1,
            int pageSize = 50,
            CancellationToken ct = default)
        {
            var dto = await CallToolAsync<McpHistoryDto>("get_history", new
            {
                symbolCode,
                startTime = startTime.ToString("O"),
                endTime = endTime.ToString("O"),
                apiKey = _apiKey,
                pageNumber,
                pageSize
            }, ct);

            return new PagedResult<PriceTickerResult>
            {
                Data = dto?.Items ?? [],
                TotalCount = dto?.TotalCount ?? 0,
                CurrentPage = dto?.PageNumber ?? pageNumber,
                PageSize = dto?.PageSize ?? pageSize,
                TotalPages = dto?.TotalPages ?? 0
            };
        }

        // ── Reference data ─────────────────────────────────────────────────────

        public async Task<ListResult<SymbolInfo>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var items = await CallToolAsync<List<SymbolInfo>>("get_symbols", new
            {
                apiKey = _apiKey
            }, ct);
            return ToListResult(items);
        }

        public Task<IEnumerable<string>> GetTimeframesAsync(CancellationToken ct = default)
            => CallToolAsync<IEnumerable<string>>("get_timeframes", new
            {
                apiKey = _apiKey
            }, ct);

        // ── Indicators ─────────────────────────────────────────────────────────

        public async Task<ListResult<IndicatorPoint>> GetSmaAsync(string symbolCode, string timeFrame, int period = 20, CancellationToken ct = default)
        {
            var items = await CallToolAsync<List<IndicatorPoint>>("get_sma", new
            {
                symbolCode,
                timeFrame,
                apiKey = _apiKey,
                period
            }, ct);
            return ToListResult(items);
        }

        public async Task<ListResult<IndicatorPoint>> GetEmaAsync(string symbolCode, string timeFrame, int period = 20, CancellationToken ct = default)
        {
            var items = await CallToolAsync<List<IndicatorPoint>>("get_ema", new
            {
                symbolCode,
                timeFrame,
                apiKey = _apiKey,
                period
            }, ct);
            return ToListResult(items);
        }

        public async Task<ListResult<IndicatorPoint>> GetRsiAsync(string symbolCode, string timeFrame, int period = 14, CancellationToken ct = default)
        {
            var items = await CallToolAsync<List<IndicatorPoint>>("get_rsi", new
            {
                symbolCode,
                timeFrame,
                apiKey = _apiKey,
                period
            }, ct);
            return ToListResult(items);
        }

        public async Task<ListResult<MacdPoint>> GetMacdAsync(string symbolCode, string timeFrame, int fastPeriod = 12, int slowPeriod = 26, int signalPeriod = 9, CancellationToken ct = default)
        {
            var items = await CallToolAsync<List<MacdPoint>>("get_macd", new
            {
                symbolCode,
                timeFrame,
                apiKey = _apiKey,
                fastPeriod,
                slowPeriod,
                signalPeriod
            }, ct);
            return ToListResult(items);
        }

        public Task<SupportResistanceResult> GetSupportResistanceAsync(string symbolCode, string timeFrame, CancellationToken ct = default)
            => CallToolAsync<SupportResistanceResult>("get_support_resistance", new
            {
                symbolCode,
                timeFrame,
                apiKey = _apiKey
            }, ct);

        public async Task<ListResult<FibonacciLevel>> GetFibonacciAsync(string symbolCode, string timeFrame, int lookback = 100, CancellationToken ct = default)
        {
            var items = await CallToolAsync<List<FibonacciLevel>>("get_fibonacci", new
            {
                symbolCode,
                timeFrame,
                apiKey = _apiKey,
                lookback
            }, ct);
            return ToListResult(items);
        }

        public async Task<ListResult<BollingerBandPoint>> GetBollingerBandsAsync(string symbolCode, string timeFrame, int period = 20, decimal multiplier = 2m, CancellationToken ct = default)
        {
            var items = await CallToolAsync<List<BollingerBandPoint>>("get_bollinger_bands", new
            {
                symbolCode,
                timeFrame,
                apiKey = _apiKey,
                period,
                multiplier
            }, ct);
            return ToListResult(items);
        }

        public async Task<ListResult<StochasticPoint>> GetStochasticAsync(string symbolCode, string timeFrame, int kPeriod = 14, int dPeriod = 3, CancellationToken ct = default)
        {
            var items = await CallToolAsync<List<StochasticPoint>>("get_stochastic", new
            {
                symbolCode,
                timeFrame,
                apiKey = _apiKey,
                kPeriod,
                dPeriod
            }, ct);
            return ToListResult(items);
        }

        public async Task<ListResult<IndicatorPoint>> GetAtrAsync(string symbolCode, string timeFrame, int period = 14, CancellationToken ct = default)
        {
            var items = await CallToolAsync<List<IndicatorPoint>>("get_atr", new
            {
                symbolCode,
                timeFrame,
                apiKey = _apiKey,
                period
            }, ct);
            return ToListResult(items);
        }

        public async Task<ListResult<IndicatorPoint>> GetCciAsync(string symbolCode, string timeFrame, int period = 20, CancellationToken ct = default)
        {
            var items = await CallToolAsync<List<IndicatorPoint>>("get_cci", new
            {
                symbolCode,
                timeFrame,
                apiKey = _apiKey,
                period
            }, ct);
            return ToListResult(items);
        }

        public async Task<ListResult<IndicatorPoint>> GetWilliamsRAsync(string symbolCode, string timeFrame, int period = 14, CancellationToken ct = default)
        {
            var items = await CallToolAsync<List<IndicatorPoint>>("get_williams_r", new
            {
                symbolCode,
                timeFrame,
                apiKey = _apiKey,
                period
            }, ct);
            return ToListResult(items);
        }

        public async Task<ListResult<AdxPoint>> GetAdxAsync(string symbolCode, string timeFrame, int period = 14, CancellationToken ct = default)
        {
            var items = await CallToolAsync<List<AdxPoint>>("get_adx", new
            {
                symbolCode,
                timeFrame,
                apiKey = _apiKey,
                period
            }, ct);
            return ToListResult(items);
        }

        public Task<SentimentResult> GetSentimentAsync(string symbolCode, string timeFrame, CancellationToken ct = default)
            => CallToolAsync<SentimentResult>("get_sentiment", new
            {
                symbolCode,
                timeFrame,
                apiKey = _apiKey
            }, ct);

        // ── MCP protocol core ──────────────────────────────────────────────────

        /// <summary>
        /// Lazily initializes the MCP session (JSON-RPC initialize handshake) and
        /// then calls the requested tool, returning the deserialized result.
        /// </summary>
        private async Task<T> CallToolAsync<T>(string toolName, object arguments, CancellationToken ct)
        {
            await EnsureInitializedAsync(ct);

            var message = new
            {
                jsonrpc = "2.0",
                method = "tools/call",
                id = 1,
                @params = new { name = toolName, arguments }
            };

            using var response = await PostMcpAsync(message, _sessionId, ct);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync(ct);
            var text = ExtractToolText(body, toolName);

            // Detect tool-level error: { "error": "..." }
            using var doc = JsonDocument.Parse(text);
            if (doc.RootElement.ValueKind == JsonValueKind.Object
                && doc.RootElement.TryGetProperty("error", out var errProp))
            {
                throw new InvalidOperationException(
                    $"MCP tool '{toolName}' error: {errProp.GetString()}");
            }

            return JsonSerializer.Deserialize<T>(text, JsonOptions)
                ?? throw new InvalidOperationException($"MCP tool '{toolName}' returned null.");
        }

        private async Task EnsureInitializedAsync(CancellationToken ct)
        {
            if (_sessionId is not null) return;

            await _initLock.WaitAsync(ct);
            try
            {
                if (_sessionId is not null) return;

                var initMessage = new
                {
                    jsonrpc = "2.0",
                    method = "initialize",
                    id = 0,
                    @params = new
                    {
                        protocolVersion = McpProtocolVersion,
                        clientInfo = new { name = "RealMarketAPI-Sdk", version = "1.0.0" },
                        capabilities = new { }
                    }
                };

                using var response = await PostMcpAsync(initMessage, null, ct);
                response.EnsureSuccessStatusCode();

                // Capture the session ID for all subsequent requests
                _sessionId = response.Headers.TryGetValues("Mcp-Session-Id", out var vals)
                    ? vals.FirstOrDefault()
                    : null;

                // Consume the initialization response body to complete the handshake
                await response.Content.ReadAsStringAsync(ct);
            }
            finally
            {
                _initLock.Release();
            }
        }

        private async Task<HttpResponseMessage> PostMcpAsync(object message, string sessionId, CancellationToken ct)
        {
            var json = JsonSerializer.Serialize(message, RequestOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, McpPath) { Content = content };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/event-stream"));
            request.Headers.TryAddWithoutValidation("MCP-Protocol-Version", McpProtocolVersion);

            if (sessionId is not null)
                request.Headers.TryAddWithoutValidation("Mcp-Session-Id", sessionId);

            return await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct);
        }

        // ── SSE / JSON body parser ─────────────────────────────────────────────

        /// <summary>
        /// Extracts the tool-result text from either a plain JSON or SSE response body.
        /// MCP Streamable HTTP sends: <c>event: message\ndata: {json-rpc}\n\n</c>
        /// </summary>
        private static string ExtractToolText(string body, string toolName)
        {
            body = body.Trim();

            string jsonRpc;

            // SSE stream: find the first non-empty "data:" line
            if (body.StartsWith("event:") || body.StartsWith("data:"))
            {
                jsonRpc = body
                    .Split('\n')
                    .Where(l => l.StartsWith("data:"))
                    .Select(l => l[5..].Trim())
                    .FirstOrDefault(l => l.Length > 0)
                    ?? throw new InvalidOperationException(
                        $"MCP tool '{toolName}': SSE response contained no data.");
            }
            else
            {
                jsonRpc = body;
            }

            // Parse the JSON-RPC envelope
            using var doc = JsonDocument.Parse(jsonRpc);

            // JSON-RPC error object
            if (doc.RootElement.TryGetProperty("error", out var rpcErr))
                throw new InvalidOperationException(
                    $"MCP tool '{toolName}' JSON-RPC error: {rpcErr.GetRawText()}");

            var result = doc.RootElement.GetProperty("result");

            // Tool-level isError flag
            if (result.TryGetProperty("isError", out var isErrProp) && isErrProp.GetBoolean())
            {
                var errText = result.GetProperty("content")[0]
                    .GetProperty("text").GetString();
                throw new InvalidOperationException($"MCP tool '{toolName}' error: {errText}");
            }

            // Walk content array and return first text block
            var content = result.GetProperty("content");
            for (int i = 0; i < content.GetArrayLength(); i++)
            {
                var item = content[i];
                if (item.GetProperty("type").GetString() == "text")
                    return item.GetProperty("text").GetString()!;
            }

            throw new InvalidOperationException(
                $"MCP tool '{toolName}' returned no text content.");
        }

        // ── Helpers ────────────────────────────────────────────────────────────

        private static ListResult<T> ToListResult<T>(List<T> items)
        {
            items ??= [];
            return new ListResult<T> { Data = items, TotalCount = items.Count };
        }

        private sealed class McpHistoryDto
        {
            public int TotalCount { get; set; }
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public int TotalPages { get; set; }
            public List<PriceTickerResult> Items { get; set; } = [];
        }
    }
}
