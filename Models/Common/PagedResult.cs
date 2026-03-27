namespace RealTimeMarketAPI.Sdk.Models.Common
{
    /// <summary>
    /// Represents a paginated result from the API.
    /// </summary>
    public class PagedResult<T> : ListResult<T>
    {
        /// <summary>The current page number (1-based).</summary>
        public int CurrentPage { get; set; }

        /// <summary>The total number of pages.</summary>
        public int TotalPages { get; set; }

        /// <summary>The number of items per page.</summary>
        public int PageSize { get; set; }
    }
}
