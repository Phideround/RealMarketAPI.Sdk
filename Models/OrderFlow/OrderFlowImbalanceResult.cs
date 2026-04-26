namespace RealTimeMarketAPI.Sdk.Models.OrderFlow
{
    public sealed class OrderFlowImbalanceResult
    {
        public string SymbolCode { get; set; } = string.Empty;
        public string TimeFrame { get; set; } = string.Empty;
        public DateTimeOffset CalculatedAt { get; set; }
        public string CurrentImbalance { get; set; } = string.Empty;
        public decimal BullishRatio { get; set; }
        public decimal BearishRatio { get; set; }
        public List<ImbalanceZone> RecentImbalanceZones { get; set; } = [];
    }

    public sealed class ImbalanceZone
    {
        public DateTimeOffset OpenTime { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public string Direction { get; set; } = string.Empty;
        public decimal BodyMultiplier { get; set; }
        public decimal Volume { get; set; }
    }
}
