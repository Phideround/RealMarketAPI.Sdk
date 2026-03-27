namespace RealTimeMarketAPI.Sdk.Models.Indicator
{
    /// <summary>
    /// A single support or resistance price level.
    /// </summary>
    public class SupportResistanceLevel
    {
        public decimal Price { get; set; }
        public int TouchCount { get; set; }
        public DateTimeOffset LastTouchedAt { get; set; }
    }

    /// <summary>
    /// Support and resistance levels for a symbol and timeframe.
    /// </summary>
    public class SupportResistanceResult
    {
        public string SymbolCode { get; set; } = string.Empty;
        public string TimeFrame { get; set; } = string.Empty;
        public List<SupportResistanceLevel> Supports { get; set; } = [];
        public List<SupportResistanceLevel> Resistances { get; set; } = [];
    }
}
