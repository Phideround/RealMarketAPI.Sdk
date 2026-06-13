using RealTimeMarketAPI.Sdk.Models.Common;
using RealTimeMarketAPI.Sdk.Models.Screener;

namespace RealTimeMarketAPI.Sdk.Clients
{
    public interface IScreenerClient
    {
        Task<ListResult<ScreenerResult>> QueryAsync(ScreenerQueryRequestModel request, CancellationToken ct = default);
    }
}
