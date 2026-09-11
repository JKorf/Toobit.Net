using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;

namespace Toobit.Net.Objects.Options
{
    /// <summary>
    /// Toobit options
    /// </summary>
    public class ToobitOptions : LibraryOptions<ToobitRestOptions, ToobitSocketOptions, ToobitCredentials, ToobitEnvironment>
    {
        /// <summary>
        /// Options for Shared API usage
        /// </summary>
        public SharedApiOptions SharedApi { get; set; } = new();
    }
}
