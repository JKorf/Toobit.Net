using CryptoExchange.Net.Interfaces;
using Toobit.Net.Clients;
using Toobit.Net.Interfaces.Clients;

namespace CryptoExchange.Net.Interfaces
{
    /// <summary>
    /// Extensions for the ICryptoRestClient and ICryptoSocketClient interfaces
    /// </summary>
    public static class CryptoClientExtensions
    {
        /// <summary>
        /// Get the Toobit REST Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IToobitRestClient Toobit(this ICryptoRestClient baseClient) => baseClient.TryGet<IToobitRestClient>(() => new ToobitRestClient());

        /// <summary>
        /// Get the Toobit Websocket Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IToobitSocketClient Toobit(this ICryptoSocketClient baseClient) => baseClient.TryGet<IToobitSocketClient>(() => new ToobitSocketClient());
    }
}
