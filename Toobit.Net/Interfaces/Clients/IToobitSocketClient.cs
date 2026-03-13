using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;
using Toobit.Net.Interfaces.Clients.SpotApi;
using Toobit.Net.Interfaces.Clients.UsdtFuturesApi;

namespace Toobit.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Toobit websocket API
    /// </summary>
    public interface IToobitSocketClient : ISocketClient<ToobitCredentials>
    {
        
        /// <summary>
        /// UsdtFutures API endpoints
        /// </summary>
        /// <see cref="IToobitSocketClientUsdtFuturesApi"/>
        public IToobitSocketClientUsdtFuturesApi UsdtFuturesApi { get; }

        /// <summary>
        /// Spot API endpoints
        /// </summary>
        /// <see cref="IToobitSocketClientSpotApi"/>
        public IToobitSocketClientSpotApi SpotApi { get; }

    }
}
