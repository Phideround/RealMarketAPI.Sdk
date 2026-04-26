namespace RealTimeMarketAPI.Sdk.Models.StopHunt
{
    public sealed class StopHuntZonesResult
    {
        public string SymbolCode { get; set; } = string.Empty;
        public string TimeFrame { get; set; } = string.Empty;
        public DateTimeOffset CalculatedAt { get; set; }
        public decimal CurrentPrice { get; set; }
        public List<StopHuntZone> Zones { get; set; } = [];
    }

    public sealed class StopHuntZone
    {
        public decimal Price { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool RecentlyHunted { get; set; }
        public DateTimeOffset? HuntedAt { get; set; }
    }
}
