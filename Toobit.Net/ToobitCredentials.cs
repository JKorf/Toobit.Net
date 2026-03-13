using CryptoExchange.Net.Authentication;

namespace Toobit.Net
{
    /// <summary>
    /// Toobit credentials
    /// </summary>
    public class ToobitCredentials : ApiCredentials
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="apiKey">The API key</param>
        /// <param name="secret">The API secret</param>
        public ToobitCredentials(string apiKey, string secret) : this(new HMACCredential(apiKey, secret)) { }
       
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="credential">The HMAC credentials</param>
        public ToobitCredentials(HMACCredential credential) : base(credential) { }

        /// <inheritdoc />
        public override ApiCredentials Copy() => new ToobitCredentials(Hmac!);
    }
}
