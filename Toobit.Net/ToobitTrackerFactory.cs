using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using CryptoExchange.Net.Trackers.UserData.Interfaces;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using Toobit.Net.Clients;
using Toobit.Net.Interfaces;
using Toobit.Net.Interfaces.Clients;

namespace Toobit.Net
{
    /// <inheritdoc />
    public class ToobitTrackerFactory : IToobitTrackerFactory
    {
        private readonly IServiceProvider? _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        public ToobitTrackerFactory()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public ToobitTrackerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public bool CanCreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval)
        {
            var client = (_serviceProvider?.GetRequiredService<IToobitSocketClient>() ?? new ToobitSocketClient());
            SubscribeKlineOptions klineOptions = symbol.TradingMode == TradingMode.Spot ? client.SpotApi.SharedClient.SubscribeKlineOptions : client.UsdtFuturesApi.SharedClient.SubscribeKlineOptions;
            return klineOptions.IsSupported(interval);
        }

        /// <inheritdoc />
        public bool CanCreateTradeTracker(SharedSymbol symbol) => true;


        /// <inheritdoc />
        public IKlineTracker CreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval, int? limit = null, TimeSpan? period = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IToobitRestClient>() ?? new ToobitRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IToobitSocketClient>() ?? new ToobitSocketClient();

            IKlineRestClient sharedRestClient;
            IKlineSocketClient sharedSocketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                sharedRestClient = restClient.SpotApi.SharedClient;
                sharedSocketClient = socketClient.SpotApi.SharedClient;
            }
            else
            {
                sharedRestClient = restClient.UsdtFuturesApi.SharedClient;
                sharedSocketClient = socketClient.UsdtFuturesApi.SharedClient;
            }

            return new KlineTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                sharedRestClient,
                sharedSocketClient,
                symbol,
                interval,
                limit,
                period
                );
        }
        /// <inheritdoc />
        public ITradeTracker CreateTradeTracker(SharedSymbol symbol, int? limit = null, TimeSpan? period = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IToobitRestClient>() ?? new ToobitRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IToobitSocketClient>() ?? new ToobitSocketClient();

            IRecentTradeRestClient? sharedRestClient;
            ITradeSocketClient sharedSocketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                sharedRestClient = restClient.SpotApi.SharedClient;
                sharedSocketClient = socketClient.SpotApi.SharedClient;
            }
            else
            {
                sharedRestClient = restClient.UsdtFuturesApi.SharedClient;
                sharedSocketClient = socketClient.UsdtFuturesApi.SharedClient;
            }

            return new TradeTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                sharedRestClient,
                null,
                sharedSocketClient,
                symbol,
                limit,
                period
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(SpotUserDataTrackerConfig? config = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IToobitRestClient>() ?? new ToobitRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IToobitSocketClient>() ?? new ToobitSocketClient();
            return new ToobitUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<ToobitUserSpotDataTracker>>() ?? new NullLogger<ToobitUserSpotDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, ApiCredentials credentials, SpotUserDataTrackerConfig? config = null, ToobitEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IToobitUserClientProvider>() ?? new ToobitUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new ToobitUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<ToobitUserSpotDataTracker>>() ?? new NullLogger<ToobitUserSpotDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserUsdtFuturesDataTracker(FuturesUserDataTrackerConfig? config = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IToobitRestClient>() ?? new ToobitRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IToobitSocketClient>() ?? new ToobitSocketClient();
            return new ToobitUserUsdtFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<ToobitUserUsdtFuturesDataTracker>>() ?? new NullLogger<ToobitUserUsdtFuturesDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserUsdtFuturesDataTracker(string userIdentifier, ApiCredentials credentials, FuturesUserDataTrackerConfig? config = null, ToobitEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IToobitUserClientProvider>() ?? new ToobitUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new ToobitUserUsdtFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<ToobitUserUsdtFuturesDataTracker>>() ?? new NullLogger<ToobitUserUsdtFuturesDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }
    }
}
