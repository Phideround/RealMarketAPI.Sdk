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
        /// Gets market prices filtered by asset category.
        /// <para>Endpoint: GET /api/v1/price/category</para>
        /// </summary>
        /// <param name="category">Category name: <c>Forex</c>, <c>Crypto</c>, <c>Commodity</c>, <c>Equity</c>, <c>Stock</c>, or <c>Index</c>.</param>
        Task<ListResult<PriceMarketResult>> GetPriceByCategoryAsync(string category, CancellationToken ct = default);

        /// <summary>
        /// Gets 24-hour price statistics (open, close, high, low, volume, change %) for all plan symbols.
        /// <para>Endpoint: GET /api/v1/price/24hr</para>
        /// </summary>
        Task<ListResult<Price24hrResult>> Get24hrStatsAsync(CancellationToken ct = default);

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
