using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
                ApiCredentials = Authenticated ? new ToobitCredentials(key, sec) : null
            }));
        }

        [Test]
        public async Task TestErrorResponseParsing()
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient().SpotApi.ExchangeData.GetTickersAsync("TSTTST", default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.ErrorCode, Is.EqualTo("-100011"));
            Assert.That(result.Error.ErrorType, Is.EqualTo(ErrorType.UnknownSymbol));
        }

        [Test]
        public async Task TestSpotAccount()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetWithdrawalsAsync(default, default, default, default, default, default, default), true);
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetDepositAddressAsync("ETH", "ETH", default), true);
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetDepositsAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetTransactionHistoryAsync(default, default, default, default, default, default, default, default), true);
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestSpotExchangeData()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetExchangeInfoAsync(default), false, ignoreProperties: [
                "rateLimits", // Deprecated
                "exchangeId", // Same value for a symbols
                "brokerFilters", // Unknown model
                "options" // Unknown model
                ]);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetOrderBookAsync("ETHUSDT", 1, default), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetRecentTradesAsync("ETHUSDT", default, default), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay, default, default, default, default), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetTickersAsync(default, default), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetPricesAsync(default, default), false, ignoreProperties: [
                "si" // Same value as s
                ]);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetBookTickersAsync(default, default), false, ignoreProperties: [
                "E" // Undocumented, always 0
                ]);
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestSpotTrading()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetOpenOrdersAsync(default, default, default, default), true);
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetOrdersAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetUserTradesAsync("ETHUSDT", default, default, default, default, default, default), true);
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestUsdtFuturesAccount()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.Account.GetTransactionHistoryAsync(default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.Account.GetBalancesAsync(default), true);
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.Account.GetLeverageInfoAsync("ETH-SWAP-USDT", default), true);
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.Account.GetFeesAsync("ETH-SWAP-USDT", default), true);
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestUsdtFuturesExchangeData()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetExchangeInfoAsync(default), false, ignoreProperties: [
                "rateLimits", // Deprecated
                "exchangeId", // Same value for a symbols
                "brokerFilters", // Unknown model
                "options" // Unknown model
                ]);
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetOrderBookAsync("ETH-SWAP-USDT", 1, default), false);
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetRecentTradesAsync("ETH-SWAP-USDT", default, default), false);
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetKlinesAsync("ETH-SWAP-USDT", Enums.KlineInterval.OneDay, default, default, default, default), false);
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetTickersAsync(default, default, default), false);
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetPricesAsync(default, default), false, ignoreProperties: [
                "si" // Same value as s
                ]);
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.ExchangeData.GetBookTickersAsync(default, default), false, ignoreProperties: [
                "E" // Undocumented, always 0
                ]);
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestUsdtFuturesTrading()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.Trading.GetOpenOrdersAsync(default, default, default, default, default), true);
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.Trading.GetOrderHistoryAsync(default, default, default, default, default, default, default), true);
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.Trading.GetUserTradesAsync("ETH-SWAP-USDT", default, default, default, default, default, default), true);
            await RunAndCheckResult(warnings, client => client.UsdtFuturesApi.Trading.GetPositionsAsync(default, default, default), true);
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }
    }
}
