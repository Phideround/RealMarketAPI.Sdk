namespace RealTimeMarketAPI.Sdk.Models.Indicator
{
    /// <summary>
    /// A single data point returned by indicator endpoints (SMA, EMA, RSI).
    /// </summary>
    public class IndicatorPoint
    {
        public DateTimeOffset OpenTime { get; set; }
        public decimal Value { get; set; }
    }
}
