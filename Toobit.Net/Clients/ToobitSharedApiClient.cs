using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Options;
using Toobit.Net.Interfaces.Clients;
using Toobit.Net.Interfaces.Clients.SpotApi;
using Toobit.Net.Interfaces.Clients.UsdtFuturesApi;
using Toobit.Net.Objects.Options;

namespace Toobit.Net.Clients
{
    /// <inheritdoc />
    public class ToobitSharedApiClient : SharedApiClientBase, IToobitSharedApiClient
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
            IToobitSocketClient socketClient,
            IOptions<ToobitOptions> options)
            : base(options.Value.SharedApi.PreferredTransport,
                restClient.SpotApi.SharedApi,
                restClient.UsdtFuturesApi.SharedApi,
                socketClient.SpotApi.SharedApi,
                socketClient.UsdtFuturesApi.SharedApi
                )
        {
            SpotRest = restClient.SpotApi.SharedApi;
            FuturesRest = restClient.UsdtFuturesApi.SharedApi;
            SpotSocket = socketClient.SpotApi.SharedApi;
            FuturesSocket = socketClient.UsdtFuturesApi.SharedApi;
        }
    }
}
