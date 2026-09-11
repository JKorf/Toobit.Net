using CryptoExchange.Net.SharedApis;
using Toobit.Net.Interfaces.Clients.SpotApi;
using Toobit.Net.Interfaces.Clients.UsdtFuturesApi;

namespace Toobit.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for the shared REST and WebSocket API implementations of Toobit
    /// </summary>
    public interface IToobitSharedApiClient : ISharedApiClientBase
    {
        /// <summary>
        /// Spot REST shared API implementations
        /// </summary>
        IToobitRestClientSpotSharedApi SpotRest { get; }

        /// <summary>
        /// Futures REST shared API implementations
        /// </summary>
        IToobitRestClientUsdtFuturesSharedApi FuturesRest { get; }

        /// <summary>
        /// Spot WebSocket shared API implementations
        /// </summary>
        IToobitSocketClientSpotSharedApi SpotSocket { get; }

        /// <summary>
        /// Futures WebSocket shared API implementations
        /// </summary>
        IToobitSocketClientUsdtFuturesSharedApi FuturesSocket { get; }
    }
}
