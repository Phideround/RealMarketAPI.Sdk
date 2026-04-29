using RealTimeMarketAPI.Sdk.Models.MultiTimeframe;
using RealTimeMarketAPI.Sdk.Models.OrderFlow;
using RealTimeMarketAPI.Sdk.Models.Ticker;

namespace RealTimeMarketAPI.Sdk.Clients
{
    /// <summary>
    /// Provides real-time streaming via the RealMarket WebSocket feed.
    /// </summary>
    public interface IWebSocketClient
    {
        /// <summary>
        /// Streams real-time price ticks for a symbol.
        /// <para>Endpoint: <c>wss://api.realmarketapi.com/price</c></para>
        /// </summary>
        IAsyncEnumerable<PriceTickerResult> StreamPriceAsync(
            string symbolCode,
            string timeFrame,
            CancellationToken ct = default);

        /// <summary>
        /// Streams real-time OHLCV candle updates for a symbol.
        /// <para>Endpoint: <c>wss://api.realmarketapi.com/candles</c></para>
        /// </summary>
        IAsyncEnumerable<PriceCandleResult> StreamCandlesAsync(
            string symbolCode,
            string timeFrame,
            CancellationToken ct = default);

        /// <summary>
        /// Streams live order flow imbalance updates for a symbol. Requires PRO plan or above.
        /// <para>Endpoint: <c>wss://api.realmarketapi.com/orderflow/imbalance</c></para>
        /// </summary>
        IAsyncEnumerable<OrderFlowImbalanceResult> StreamOrderFlowImbalanceAsync(
            string symbolCode,
            string timeFrame,
            CancellationToken ct = default);

        /// <summary>
        /// Streams trend direction across all plan-allowed timeframes, pushed on any candle update. Requires PRO plan or above.
        /// <para>Endpoint: <c>wss://api.realmarketapi.com/multi-timeframe</c></para>
        /// </summary>
        IAsyncEnumerable<MultiTimeframeResult> StreamMultiTimeframeAsync(
            string symbolCode,
            CancellationToken ct = default);

        /// <summary>
        /// Streams a full market snapshot pushed on every H1 tick for all plan symbols.
        /// <para>Endpoint: <c>wss://api.realmarketapi.com/market</c></para>
        /// </summary>
        IAsyncEnumerable<List<PriceMarketResult>> StreamMarketAsync(
            CancellationToken ct = default);
    }
}
