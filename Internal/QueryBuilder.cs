namespace RealTimeMarketAPI.Sdk.Internal
{
    internal static class QueryBuilder
    {
        internal static string Build(Dictionary<string, string> parameters)
        {
            var pairs = parameters
                .Where(p => p.Value is not null)
                .Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value!)}");

            return string.Join("&", pairs);
        }
    }
}
