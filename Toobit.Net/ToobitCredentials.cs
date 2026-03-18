using CryptoExchange.Net.Authentication;

namespace Toobit.Net
{
    /// <summary>
    /// Toobit API credentials
    /// </summary>
    public class ToobitCredentials : HMACCredential
    {
        /// <summary>
        /// Create new credentials providing only credentials in HMAC format
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        public ToobitCredentials(string key, string secret) : base(key, secret)
        {
        }
    }
}
