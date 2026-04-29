namespace RealTimeMarketAPI.Sdk.Models.Account
{
    /// <summary>Account information and plan details returned by <c>GET /api/v1/me</c>.</summary>
    public sealed class AccountMeResult
    {
        public string PlanCode { get; set; } = string.Empty;
        public int RequestCount { get; set; }
        public int RequestLimit { get; set; }
        public decimal UsagePercentage { get; set; }
        public List<string> Symbols { get; set; } = [];
        public List<string> Timeframes { get; set; } = [];
        public bool IsSocketSupport { get; set; }
        public int WebSocketConcurrentLimit { get; set; }
        public int HistoricalRangeMonth { get; set; }
    }
}
