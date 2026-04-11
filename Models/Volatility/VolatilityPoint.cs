namespace RealTimeMarketAPI.Sdk.Models.Volatility
{
    /// <summary>
    /// A single volatility data point covering ATR, normalised ATR, Bollinger Band Width,
    /// and Historical Volatility for one candle.
    /// </summary>
    public class VolatilityPoint
    {
        public DateTimeOffset OpenTime { get; set; }

        /// <summary>Average True Range — Wilder smoothed over the requested period.</summary>
        public decimal Atr { get; set; }

        /// <summary>ATR as a percentage of closing price (ATR ÷ Close × 100).</summary>
        public decimal AtrPercent { get; set; }

        /// <summary>Bollinger Band Width ((Upper − Lower) ÷ Middle × 100).</summary>
        public decimal BandWidth { get; set; }

        /// <summary>Rolling std dev of log returns over the period, expressed as a percentage.</summary>
        public decimal HistoricalVolatility { get; set; }
    }
}
