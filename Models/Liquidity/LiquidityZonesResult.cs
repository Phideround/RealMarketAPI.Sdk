namespace RealTimeMarketAPI.Sdk.Models.Liquidity
{
    public sealed class LiquidityZonesResult
    {
        public string SymbolCode { get; set; } = string.Empty;
        public string TimeFrame { get; set; } = string.Empty;
        public DateTimeOffset CalculatedAt { get; set; }
        public decimal CurrentPrice { get; set; }
        public List<LiquidityZone> Zones { get; set; } = [];
    }

    public sealed class LiquidityZone
    {
        public decimal Price { get; set; }
        public string Type { get; set; } = string.Empty;
        public int TouchCount { get; set; }
        public DateTimeOffset LastTouchedAt { get; set; }
        public string Strength { get; set; } = string.Empty;
    }
}
