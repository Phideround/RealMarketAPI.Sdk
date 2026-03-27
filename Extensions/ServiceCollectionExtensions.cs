using Microsoft.Extensions.DependencyInjection;

namespace RealTimeMarketAPI.Sdk.Extensions
{
    /// <summary>
    /// Extension methods to register the RealMarket API SDK with the dependency injection container.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers <see cref="IRealMarketApiClient"/> with the provided API key.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="apiKey">Your RealMarket API key.</param>
        /// <param name="baseUrl">
        /// Optional base URL override. Defaults to <c>https://api.realmarketapi.com/</c>.
        /// </param>
        /// <returns>The same <see cref="IServiceCollection"/> for chaining.</returns>
        public static IServiceCollection AddRealMarketApiClient(
            this IServiceCollection services,
            string apiKey,
            string baseUrl = RealMarketApiOptions.DefaultBaseUrl)
        {
            return services.AddRealMarketApiClient(options =>
            {
                options.ApiKey = apiKey;
                options.BaseUrl = baseUrl;
            });
        }

        /// <summary>
        /// Registers <see cref="IRealMarketApiClient"/> using a configuration delegate.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configure">A delegate to configure <see cref="RealMarketApiOptions"/>.</param>
        /// <returns>The same <see cref="IServiceCollection"/> for chaining.</returns>
        public static IServiceCollection AddRealMarketApiClient(
            this IServiceCollection services,
            Action<RealMarketApiOptions> configure)
        {
            var options = new RealMarketApiOptions();
            configure(options);

            services.AddSingleton(options);

            services.AddHttpClient<IRealMarketApiClient, RealMarketApiClient>(client =>
            {
                client.BaseAddress = new Uri(options.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            return services;
        }
    }
}
