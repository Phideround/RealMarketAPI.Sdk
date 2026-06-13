using RealTimeMarketAPI.Sdk.Models.MarketCalendar;

namespace RealTimeMarketAPI.Sdk.Clients
{
    public interface IMarketCalendarClient
    {
        Task<MarketCalendarResult> GetAsync(DateOnly? date = null, string timezone = "UTC", CancellationToken ct = default);
    }
}
