using Toobit.Net.Clients;
using Toobit.Net.Objects.Options;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Toobit.Net.Objects.Models;

namespace Toobit.Net.UnitTests
{
    internal class ToobitSocketIntegrationTests : SocketIntegrationTest<ToobitSocketClient>
    {
        public override bool Run { get; set; } = true;

        public ToobitSocketIntegrationTests()
        {
        }

        public override ToobitSocketClient GetClient(ILoggerFactory loggerFactory, bool useUpdatedDeserialization)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new ToobitSocketClient(Options.Create(new ToobitSocketOptions
            {
                UseUpdatedDeserialization = useUpdatedDeserialization,
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null
            }), loggerFactory);
        }

        private ToobitRestClient GetRestClient(bool useUpdatedDeserialization)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new ToobitRestClient(x =>
            {
                x.UseUpdatedDeserialization = useUpdatedDeserialization;
                x.ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null;
            });
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestSubscriptions(bool useUpdatedDeserialization)
        {
            var listenKey = await GetRestClient(useUpdatedDeserialization).SpotApi.Account.StartUserStreamAsync();
            await RunAndCheckUpdate<ToobitAccountUpdate>(useUpdatedDeserialization, (client, updateHandler) => client.SpotApi.SubscribeToUserDataUpdatesAsync(listenKey.Data, updateHandler, default, default, default), false, true);
            
            await RunAndCheckUpdate<ToobitTickerUpdate>(useUpdatedDeserialization, (client, updateHandler) => client.SpotApi.SubscribeToTickerUpdatesAsync("ETHUSDT", updateHandler, default), true, false);
            await RunAndCheckUpdate<ToobitTickerUpdate>(useUpdatedDeserialization, (client, updateHandler) => client.UsdtFuturesApi.SubscribeToTickerUpdatesAsync("ETH-SWAP-USDT", updateHandler, default), true, false);
        }
    }
}
