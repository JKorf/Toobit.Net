using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Toobit.Net.Clients;
using Toobit.Net.Objects.Options;

namespace Toobit.Net.UnitTests
{
    [NonParallelizable]
    public class ToobitRestIntegrationTests : RestIntegrationTest<ToobitRestClient>
    {
        public override bool Run { get; set; } = false;

        public override ToobitRestClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new ToobitRestClient(null, loggerFactory, Options.Create(new ToobitRestOptions
            {
                AutoTimestamp = false,
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null
            }));
        }

        [Test]
        public async Task TestErrorResponseParsing()
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient().SpotApi.ExchangeData.GetTickersAsync("TSTTST", default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.Code, Is.EqualTo(-100011));
        }

        [Test]
        public async Task TestSpotAccount()
        {
            await RunAndCheckResult(client => client.SpotApi.Account.GetWithdrawalsAsync(default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetDepositAddressAsync("ETH", "ETH", default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetDepositsAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetTransactionHistoryAsync(default, default, default, default, default, default, default, default), true);
        }

        [Test]
        public async Task TestSpotExchangeData()
        {
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetExchangeInfoAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetOrderBookAsync("ETHUSDT", 1, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetRecentTradesAsync("ETHUSDT", default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay, default, default, default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetTickersAsync(default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetPricesAsync(default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetBookTickersAsync(default, default), false);
        }

        [Test]
        public async Task TestSpotTrading()
        {
            await RunAndCheckResult(client => client.SpotApi.Trading.GetOpenOrdersAsync(default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetOrdersAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetUserTradesAsync("ETHUSDT", default, default, default, default, default, default), true);
        }

        [Test]
        public async Task TestUsdtFuturesAccount()
        {
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetTransactionHistoryAsync(default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetBalancesAsync(default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetLeverageInfoAsync("ETH-SWAP-USDT", default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetFeesAsync("ETH-SWAP-USDT", default), true);
        }

        [Test]
        public async Task TestUsdtFuturesExchangeData()
        {
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetExchangeInfoAsync(default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetOrderBookAsync("ETH-SWAP-USDT", 1, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetRecentTradesAsync("ETH-SWAP-USDT", default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetKlinesAsync("ETH-SWAP-USDT", Enums.KlineInterval.OneDay, default, default, default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetTickersAsync(default, default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetPricesAsync(default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetBookTickersAsync(default, default), false);
        }

        [Test]
        public async Task TestUsdtFuturesTrading()
        {
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetOpenOrdersAsync(default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetOrderHistoryAsync(default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetUserTradesAsync("ETH-SWAP-USDT", default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetPositionsAsync(default, default, default), true);
        }
    }
}
