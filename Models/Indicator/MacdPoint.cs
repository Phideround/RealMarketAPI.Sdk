namespace RealTimeMarketAPI.Sdk.Models.Indicator
{
    /// <summary>
    /// A single MACD data point containing MACD line, signal line, and histogram.
    /// </summary>
    public class MacdPoint
    {
        public DateTimeOffset OpenTime { get; set; }
        public decimal Macd { get; set; }
        public decimal Signal { get; set; }
        public decimal Histogram { get; set; }
    }
}
