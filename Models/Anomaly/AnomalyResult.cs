namespace RealTimeMarketAPI.Sdk.Models.Anomaly
{
    public sealed class AnomalyResult
    {
        public string SymbolCode { get; set; } = string.Empty;
        public string TimeFrame { get; set; } = string.Empty;
        public DateTimeOffset CalculatedAt { get; set; }
        public bool HasAnomalies { get; set; }
        public List<AnomalyItem> Anomalies { get; set; } = [];
    }

    public sealed class AnomalyItem
    {
        public DateTimeOffset OpenTime { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public decimal Threshold { get; set; }
    }
}
