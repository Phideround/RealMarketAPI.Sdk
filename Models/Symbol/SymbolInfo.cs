namespace RealTimeMarketAPI.Sdk.Models.Symbol
{
    /// <summary>
    /// Tradable symbol information.
    /// </summary>
    public class SymbolInfo
    {
        public string SymbolCode { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string MarketClass { get; set; } = string.Empty;
    }
}
