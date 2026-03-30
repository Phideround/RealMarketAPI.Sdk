namespace RealTimeMarketAPI.Sdk.Models.Indicator
{
    /// <summary>
    /// A single Average Directional Index (ADX) data point with trend direction components.
    /// </summary>
    public class AdxPoint
    {
        public DateTimeOffset OpenTime { get; set; }
        public decimal Adx { get; set; }
        public decimal PlusDI { get; set; }
        public decimal MinusDI { get; set; }
    }
}
