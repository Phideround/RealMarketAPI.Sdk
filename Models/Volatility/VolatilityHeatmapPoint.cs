namespace RealTimeMarketAPI.Sdk.Models.Volatility
{
    /// <summary>
    /// A single cell in the Day-of-Week × Hour-of-Day volatility heatmap.
    /// </summary>
    public class VolatilityHeatmapPoint
    {
        /// <summary>Day name (Monday … Sunday).</summary>
        public string DayOfWeek { get; set; } = string.Empty;

        /// <summary>Hour of day in UTC (0 – 23).</summary>
        public int Hour { get; set; }

        /// <summary>Mean true range across all candles that fell in this time bucket.</summary>
        public decimal AvgRange { get; set; }

        /// <summary>avgRange ÷ max(avgRange) across all buckets — 1.0 marks the most volatile slot.</summary>
        public decimal Intensity { get; set; }
    }
}
