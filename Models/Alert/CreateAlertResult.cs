namespace RealTimeMarketAPI.Sdk.Models.Alert
{
    public class CreateAlertResult
    {
        public string AlertId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTimeOffset CreatedDate { get; set; }
    }
}
