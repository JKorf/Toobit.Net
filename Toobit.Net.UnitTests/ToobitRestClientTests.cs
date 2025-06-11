using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using Toobit.Net.Clients;

namespace Toobit.Net.UnitTests
{
    [TestFixture()]
    public class ToobitRestClientTests
    {
        [Test]
        public void CheckSignatureExample1()
        {
            var authProvider = new ToobitAuthenticationProvider(new ApiCredentials("XXX", "XXX"));
            var client = (RestApiClient)new ToobitRestClient().SpotApi;

            CryptoExchange.Net.Testing.TestHelpers.CheckSignature(
                client,
                authProvider,
                HttpMethod.Post,
                "/api/v3/order",
                (uriParams, bodyParams, headers) =>
                {
                    return uriParams["signature"].ToString();
                },
                "c2a5ec39e186f05cf65000aac8c6707c6541337bda16bc1edaea13ea35d1eb0e",
                new Dictionary<string, object>
                {
                    { "symbol", "LTCBTC" },
                },
                DateTimeConverter.ParseFromDouble(1499827319559),
                true,
                false);
        }

        [Test]
        public void CheckInterfaces()
        {
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingRestInterfaces<ToobitRestClient>();
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingSocketInterfaces<ToobitSocketClient>();
        }
    }
}
