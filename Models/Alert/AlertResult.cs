namespace RealTimeMarketAPI.Sdk.Models.Alert
{
    public class AlertResult
    {
        public string AlertId { get; set; } = string.Empty;
        public string SymbolCode { get; set; } = string.Empty;
        public string TimeFrame { get; set; } = string.Empty;
        public string RuleType { get; set; } = string.Empty;
        public decimal Threshold { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTimeOffset CreatedDate { get; set; }
    }
}
