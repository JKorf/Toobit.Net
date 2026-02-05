using Toobit.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.UserData;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.Logging;

namespace Toobit.Net
{
    /// <inheritdoc/>
    public class ToobitUserSpotDataTracker : UserSpotDataTracker
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ToobitUserSpotDataTracker(
            ILogger<ToobitUserSpotDataTracker> logger,
            IToobitRestClient restClient,
            IToobitSocketClient socketClient,
            string? userIdentifier,
            SpotUserDataTrackerConfig? config) : base(
                logger,
                restClient.SpotApi.SharedClient,
                restClient.SpotApi.SharedClient,
                restClient.SpotApi.SharedClient,
                socketClient.SpotApi.SharedClient,
                restClient.SpotApi.SharedClient,
                socketClient.SpotApi.SharedClient,
                socketClient.SpotApi.SharedClient,
                userIdentifier,
                config ?? new SpotUserDataTrackerConfig())
        {
        }
    }

    /// <inheritdoc/>
    public class ToobitUserUsdtFuturesDataTracker : UserFuturesDataTracker
    {
        /// <inheritdoc/>
        protected override bool WebsocketPositionUpdatesAreFullSnapshots => false;

        /// <summary>
        /// ctor
        /// </summary>
        public ToobitUserUsdtFuturesDataTracker(
            ILogger<ToobitUserUsdtFuturesDataTracker> logger,
            IToobitRestClient restClient,
            IToobitSocketClient socketClient,
            string? userIdentifier,
            FuturesUserDataTrackerConfig? config) : base(logger,
                restClient.UsdtFuturesApi.SharedClient,
                restClient.UsdtFuturesApi.SharedClient,
                restClient.UsdtFuturesApi.SharedClient,
                socketClient.UsdtFuturesApi.SharedClient,
                restClient.UsdtFuturesApi.SharedClient,
                socketClient.UsdtFuturesApi.SharedClient,
                socketClient.UsdtFuturesApi.SharedClient,
                socketClient.UsdtFuturesApi.SharedClient,
                userIdentifier,
                config ?? new FuturesUserDataTrackerConfig())
        {
        }
    }
}
