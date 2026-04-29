using RealTimeMarketAPI.Sdk.Models.Account;

namespace RealTimeMarketAPI.Sdk.Clients
{
    /// <summary>Access account and plan information.</summary>
    public interface IAccountClient
    {
        /// <summary>
        /// Returns plan code, request count/limit, usage percentage, allowed symbols and timeframes,
        /// WebSocket limits, and historical range.
        /// <para>Endpoint: GET /api/v1/me</para>
        /// </summary>
        /// <remarks>Does <b>not</b> consume your monthly request quota.</remarks>
        Task<AccountMeResult> GetMeAsync(CancellationToken ct = default);
    }
}
