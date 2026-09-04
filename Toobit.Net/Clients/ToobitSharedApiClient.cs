using Toobit.Net.Interfaces.Clients;
using Toobit.Net.Interfaces.Clients.SpotApi;
using Toobit.Net.Interfaces.Clients.UsdtFuturesApi;

namespace Toobit.Net.Clients
{
    /// <inheritdoc />
    public class ToobitSharedApiClient : IToobitSharedApiClient
    {
        /// <inheritdoc />
        public IToobitRestClientSpotSharedApi SpotRest { get; }
        /// <inheritdoc />
        public IToobitRestClientUsdtFuturesSharedApi FuturesRest { get; }
        /// <inheritdoc />
        public IToobitSocketClientSpotSharedApi SpotSocket { get; }
        /// <inheritdoc />
        public IToobitSocketClientUsdtFuturesSharedApi FuturesSocket { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public ToobitSharedApiClient(
            IToobitRestClient restClient,
            IToobitSocketClient socketClient)
        {
            SpotRest = restClient.SpotApi.SharedApi;
            FuturesRest = restClient.UsdtFuturesApi.SharedApi;
            SpotSocket = socketClient.SpotApi.SharedApi;
            FuturesSocket = socketClient.UsdtFuturesApi.SharedApi;
        }
    }
}
