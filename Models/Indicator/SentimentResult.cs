namespace RealTimeMarketAPI.Sdk.Models.Indicator
{
    /// <summary>
    /// Market sentiment analysis result for a symbol and timeframe.
    /// </summary>
    public class SentimentResult
    {
        public string SymbolCode { get; set; } = string.Empty;
        public string TimeFrame { get; set; } = string.Empty;
        public DateTimeOffset CalculatedAt { get; set; }

        /// <summary>
        /// Overall price trend. Values: StrongUptrend, Uptrend, Sideways, Downtrend, StrongDowntrend.
        /// </summary>
        public string Trend { get; set; } = string.Empty;

        /// <summary>
        /// Market sentiment label. Values: ExtremeFear, Fear, Neutral, Greed, ExtremeGreed.
        /// </summary>
        public string Sentiment { get; set; } = string.Empty;

        /// <summary>
        /// Fear &amp; Greed score from 0 (Extreme Fear) to 100 (Extreme Greed).
        /// </summary>
        public int FearGreedScore { get; set; }

        public decimal Rsi { get; set; }
        public decimal MacdHistogram { get; set; }
        public decimal Ema50 { get; set; }
        public decimal Ema100 { get; set; }
        public decimal CurrentClose { get; set; }
    }
}
