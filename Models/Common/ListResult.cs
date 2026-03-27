namespace RealTimeMarketAPI.Sdk.Models.Common
{
    /// <summary>
    /// Represents a list result from the API.
    /// </summary>
    public class ListResult<T>
    {
        /// <summary>The result items.</summary>
        public IEnumerable<T> Data { get; set; } = [];

        /// <summary>Total number of items in the result.</summary>
        public int TotalCount { get; set; }
    }
}
