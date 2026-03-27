using RealTimeMarketAPI.Sdk.Models.Common;
using RealTimeMarketAPI.Sdk.Models.Ticker;

namespace RealTimeMarketAPI.Sdk.Clients
{
    /// <summary>
    /// Provides access to real-time price, candle, and historical data endpoints.
    /// </summary>
    public interface ITickerClient
    {
        /// <summary>
        /// Gets the latest real-time price ticker for a symbol and timeframe.
        /// <para>Endpoint: GET /api/v1/price</para>
        /// </summary>
        /// <param name="symbolCode">Symbol code (e.g. "EURUSD", "BTCUSDT").</param>
        /// <param name="timeFrame">Timeframe (e.g. "M1", "H1", "D1").</param>
        Task<PriceTickerResult> GetPriceAsync(string symbolCode, string timeFrame, CancellationToken ct = default);

        /// <summary>
        /// Gets market overview for all symbols accessible by your plan.
        /// <para>Endpoint: GET /api/v1/price/market</para>
        /// </summary>
        Task<ListResult<PriceMarketResult>> GetMarketPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets the latest OHLCV candles for a symbol and timeframe.
        /// <para>Endpoint: GET /api/v1/candle</para>
        /// </summary>
        /// <param name="symbolCode">Symbol code (e.g. "EURUSD").</param>
        /// <param name="timeFrame">Timeframe (e.g. "M1", "H1", "D1").</param>
        Task<ListResult<PriceCandleResult>> GetCandlesAsync(string symbolCode, string timeFrame, CancellationToken ct = default);

        /// <summary>
        /// Gets paginated historical candle data for a symbol within a date range.
        /// <para>Endpoint: GET /api/v1/history</para>
        /// </summary>
        /// <param name="symbolCode">Symbol code (e.g. "EURUSD").</param>
        /// <param name="startTime">Start of the date range.</param>
        /// <param name="endTime">End of the date range.</param>
        /// <param name="pageNumber">Page number (1-based).</param>
        /// <param name="pageSize">Number of items per page.</param>
        Task<PagedResult<PriceTickerResult>> GetHistoryAsync(
            string symbolCode,
            DateTimeOffset startTime,
            DateTimeOffset endTime,
            int pageNumber = 1,
            int pageSize = 20,
            CancellationToken ct = default);
    }
}
