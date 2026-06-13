namespace RealTimeMarketAPI.Sdk.Models.Alert
{
    public class CreateAlertRequestModel
    {
        public string SymbolCode { get; set; } = string.Empty;
        public string TimeFrame { get; set; } = string.Empty;
        public string RuleType { get; set; } = string.Empty;
        public decimal Threshold { get; set; }
        public int CooldownSeconds { get; set; } = 300;
        public List<string> Channels { get; set; } = [];
    }
}
