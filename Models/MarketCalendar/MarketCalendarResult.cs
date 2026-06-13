namespace RealTimeMarketAPI.Sdk.Models.MarketCalendar
{
    public class MarketSessionResult
    {
        public string Market { get; set; } = string.Empty;
        public DateTimeOffset OpenUtc { get; set; }
        public DateTimeOffset CloseUtc { get; set; }
    }

    public class MarketEventResult
    {
        public string Title { get; set; } = string.Empty;
        public string Impact { get; set; } = string.Empty;
        public DateTimeOffset TimeUtc { get; set; }
    }

    public class MarketCalendarResult
    {
        public DateOnly Date { get; set; }
        public List<MarketSessionResult> Sessions { get; set; } = [];
        public List<MarketEventResult> Events { get; set; } = [];
    }
}
