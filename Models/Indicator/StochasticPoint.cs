namespace RealTimeMarketAPI.Sdk.Models.Indicator
{
    /// <summary>
    /// A single Stochastic Oscillator data point containing %K and %D lines.
    /// </summary>
    public class StochasticPoint
    {
        public DateTimeOffset OpenTime { get; set; }
        public decimal K { get; set; }
        public decimal D { get; set; }
    }
}
