using RealTimeMarketAPI.Sdk.Internal;
using RealTimeMarketAPI.Sdk.Models.Common;
using RealTimeMarketAPI.Sdk.Models.Symbol;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealTimeMarketAPI.Sdk.Clients
{
    internal sealed class SymbolClient(HttpClient httpClient) : ISymbolClient
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<ListResult<SymbolInfo>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var result = await httpClient.GetFromJsonAsync<ListResult<SymbolInfo>>("api/v1/symbol", JsonOptions, ct);
            return result!;
        }
    }
}
