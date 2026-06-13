using RealTimeMarketAPI.Sdk.Models.Alert;
using RealTimeMarketAPI.Sdk.Models.Common;

namespace RealTimeMarketAPI.Sdk.Clients
{
    public interface IAlertClient
    {
        Task<CreateAlertResult> CreateAsync(CreateAlertRequestModel request, CancellationToken ct = default);
        Task<ListResult<AlertResult>> GetAsync(string? status = null, CancellationToken ct = default);
        Task<DeleteAlertResult> DeleteAsync(string alertId, CancellationToken ct = default);
    }
}
