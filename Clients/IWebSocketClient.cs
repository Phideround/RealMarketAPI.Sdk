using RealTimeMarketAPI.Sdk.Models.Ticker;

namespace RealTimeMarketAPI.Sdk.Clients
{
    /// <summary>
    /// Provides real-time streaming via the RealMarket WebSocket feed.
    /// </summary>
    public interface IWebSocketClient
    {
        /// <summary>
        /// Opens a WebSocket connection and streams real-time price ticks for a symbol
        /// until the <paramref name="ct"/> is cancelled.
        /// <para>Endpoint: <c>wss://api.realmarketapi.com/price</c></para>
        /// </summary>
        /// <param name="symbolCode">Symbol code, e.g. <c>"EURUSD"</c>.</param>
        /// <param name="timeFrame">Timeframe code, e.g. <c>"M1"</c>, <c>"H1"</c>.</param>
        /// <param name="ct">Cancel this token to close the stream gracefully.</param>
        /// <remarks>Requires a plan with WebSocket support (<c>IsSocketSupport = true</c>).</remarks>
        IAsyncEnumerable<PriceTickerResult> StreamPriceAsync(
            string symbolCode,
            string timeFrame,
            CancellationToken ct = default);
    }
}
