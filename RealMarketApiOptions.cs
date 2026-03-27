namespace RealTimeMarketAPI.Sdk
{
    /// <summary>
    /// Configuration options for the RealMarket API client.
    /// </summary>
    public class RealMarketApiOptions
    {
        /// <summary>
        /// The default base URL for the RealMarket API.
        /// </summary>
        public const string DefaultBaseUrl = "https://api.realmarketapi.com/";

        /// <summary>
        /// The base URL of the RealMarket API. Defaults to <see cref="DefaultBaseUrl"/>.
        /// </summary>
        public string BaseUrl { get; set; } = DefaultBaseUrl;

        /// <summary>
        /// Your RealMarket API key.
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;
    }
}
