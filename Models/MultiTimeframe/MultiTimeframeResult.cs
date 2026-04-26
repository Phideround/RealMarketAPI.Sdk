namespace RealTimeMarketAPI.Sdk.Models.MultiTimeframe
{
    public sealed class MultiTimeframeResult
    {
        public string SymbolCode { get; set; } = string.Empty;
        public DateTimeOffset CalculatedAt { get; set; }
        public Dictionary<string, string> Timeframes { get; set; } = [];
    }
}
