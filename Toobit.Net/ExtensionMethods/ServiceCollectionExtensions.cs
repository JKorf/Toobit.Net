using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading;
using Toobit.Net;
using Toobit.Net.Clients;
using Toobit.Net.Interfaces;
using Toobit.Net.Interfaces.Clients;
using Toobit.Net.Objects.Options;
using Toobit.Net.SymbolOrderBooks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Add services such as the IToobitRestClient and IToobitSocketClient. Configures the services based on the provided configuration.<br />
        /// See <see href="https://github.com/JKorf/Toobit.Net/blob/main/Examples/example-config.json" /> for an example of how to set up the configuration.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration(section) containing the options</param>
        /// <returns></returns>
        public static IServiceCollection AddToobit(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = ToobitOptions.CreateFromConfiguration(configuration);

            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

            return AddToobitCore(services, options.SocketClientLifeTime);
        }

        /// <summary>
        /// Add services such as the IToobitRestClient and IToobitSocketClient. Services will be configured based on the provided options.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="optionsDelegate">Set options for the Toobit services</param>
        /// <returns></returns>
        public static IServiceCollection AddToobit(
            this IServiceCollection services,
            Action<ToobitOptions>? optionsDelegate = null)
        {
            var options = ToobitOptions.Create(optionsDelegate);

            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

            return AddToobitCore(services, options.SocketClientLifeTime);
        }

        private static IServiceCollection AddToobitCore(
            this IServiceCollection services,
            ServiceLifetime? socketClientLifeTime = null)
        {
            services.AddHttpClient<IToobitRestClient, ToobitRestClient>((client, serviceProvider) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<ToobitRestOptions>>().Value;
                client.Timeout = options.RequestTimeout;
                return new ToobitRestClient(client, serviceProvider.GetRequiredService<ILoggerFactory>(), serviceProvider.GetRequiredService<IOptions<ToobitRestOptions>>());
            }).ConfigurePrimaryHttpMessageHandler((serviceProvider) => {
                var options = serviceProvider.GetRequiredService<IOptions<ToobitRestOptions>>().Value;
                return LibraryHelpers.CreateHttpClientMessageHandler(options);
            }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
            services.Add(new ServiceDescriptor(typeof(IToobitSocketClient), x => { return new ToobitSocketClient(x.GetRequiredService<IOptions<ToobitSocketOptions>>(), x.GetRequiredService<ILoggerFactory>()); }, socketClientLifeTime ?? ServiceLifetime.Singleton));

            services.AddTransient<IToobitOrderBookFactory, ToobitOrderBookFactory>();
            services.AddTransient<IToobitTrackerFactory, ToobitTrackerFactory>();
            services.AddTransient<ITrackerFactory, ToobitTrackerFactory>();
            services.AddSingleton<IToobitUserClientProvider, ToobitUserClientProvider>(x => 
            new ToobitUserClientProvider(
                x.GetRequiredService<IHttpClientFactory>().CreateClient(typeof(IToobitRestClient).Name),
                x.GetRequiredService<ILoggerFactory>(),
                x.GetRequiredService<IOptions<ToobitRestOptions>>(),
                x.GetRequiredService<IOptions<ToobitSocketOptions>>()));

            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IToobitRestClient>().SpotApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IToobitSocketClient>().SpotApi.SharedClient);
            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IToobitRestClient>().UsdtFuturesApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IToobitSocketClient>().UsdtFuturesApi.SharedClient);

            services.RegisterSharedApiClient<
                IToobitSharedApiClient,
                ToobitSharedApiClient>(sharedApis => sharedApis
                    .Add(client => client.SpotRest)
                    .Add(client => client.SpotSocket)
                    .Add(client => client.FuturesRest)
                    .Add(client => client.FuturesSocket)
                    );
            return services;
        }
    }
}
