namespace RealTimeMarketAPI.Sdk.Models.Volatility
{
    /// <summary>
    /// A candle where ATR exceeded a given multiple of the series average (volatility spike).
    /// </summary>
    public class VolatilitySpikePoint
    {
        public DateTimeOffset OpenTime { get; set; }

        /// <summary>ATR value at the spike candle.</summary>
        public decimal Atr { get; set; }

        /// <summary>Mean ATR across the entire series used for comparison.</summary>
        public decimal AtrAverage { get; set; }

        /// <summary>atr ÷ atrAverage — how many times above average the spike was.</summary>
        public decimal MultiplierReached { get; set; }

        /// <summary>Candle direction: Bullish, Bearish, or Neutral.</summary>
        public string Direction { get; set; } = string.Empty;
    }
}
