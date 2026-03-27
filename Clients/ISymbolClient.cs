using RealTimeMarketAPI.Sdk.Models.Common;
using RealTimeMarketAPI.Sdk.Models.Symbol;

namespace RealTimeMarketAPI.Sdk.Clients
{
    /// <summary>
    /// Provides access to the symbols endpoint.
    /// </summary>
    public interface ISymbolClient
    {
        /// <summary>
        /// Gets all available trading symbols.
        /// <para>Endpoint: GET /api/v1/symbol</para>
        /// </summary>
        Task<ListResult<SymbolInfo>> GetSymbolsAsync(CancellationToken ct = default);
    }
}
