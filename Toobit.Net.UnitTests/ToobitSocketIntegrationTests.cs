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
        public override bool Run { get; set; } = false;

        public ToobitSocketIntegrationTests()
        {
        }

        public override ToobitSocketClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new ToobitSocketClient(Options.Create(new ToobitSocketOptions
            {
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null
            }), loggerFactory);
        }

        private ToobitRestClient GetRestClient()
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new ToobitRestClient(x =>
            {
                x.ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null;
            });
        }

        [Test]
        public async Task TestSubscriptions()
        {
            var listenKey = await GetRestClient().SpotApi.Account.StartUserStreamAsync();
            await RunAndCheckUpdate<ToobitAccountUpdate>((client, updateHandler) => client.SpotApi.SubscribeToUserDataUpdatesAsync(listenKey.Data, updateHandler, default, default, default), false, true);
            
            await RunAndCheckUpdate<ToobitTickerUpdate>((client, updateHandler) => client.SpotApi.SubscribeToTickerUpdatesAsync("ETHUSDT", updateHandler, default), true, false);
            await RunAndCheckUpdate<ToobitTickerUpdate>((client, updateHandler) => client.UsdtFuturesApi.SubscribeToTickerUpdatesAsync("ETH-SWAP-USDT", updateHandler, default), true, false);
        }
    }
}
