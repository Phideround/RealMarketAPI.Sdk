namespace RealTimeMarketAPI.Sdk.Models.Indicator
{
    /// <summary>
    /// A single Fibonacci retracement/extension level.
    /// </summary>
    public class FibonacciLevel
    {
        public decimal Ratio { get; set; }
        public decimal Price { get; set; }
    }
}
