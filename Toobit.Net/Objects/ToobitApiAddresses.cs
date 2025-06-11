namespace Toobit.Net.Objects
{
    /// <summary>
    /// Api addresses
    /// </summary>
    public class ToobitApiAddresses
    {
        /// <summary>
        /// The address used by the ToobitRestClient for the API
        /// </summary>
        public string RestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the ToobitSocketClient for the websocket API
        /// </summary>
        public string SocketClientAddress { get; set; } = "";

        /// <summary>
        /// The default addresses to connect to the Toobit API
        /// </summary>
        public static ToobitApiAddresses Default = new ToobitApiAddresses
        {
            RestClientAddress = "https://api.toobit.com",
            SocketClientAddress = "wss://stream.toobit.com"
        };
    }
}
