namespace RealTimeMarketAPI.Sdk.Models.Indicator
{
    /// <summary>
    /// A single Bollinger Bands data point containing upper, middle (SMA), and lower bands.
    /// </summary>
    public class BollingerBandPoint
    {
        public DateTimeOffset OpenTime { get; set; }
        public decimal Upper { get; set; }
        public decimal Middle { get; set; }
        public decimal Lower { get; set; }
    }
}
