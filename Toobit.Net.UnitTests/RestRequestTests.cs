using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Toobit.Net.Clients;
using Toobit.Net.Enums;

namespace Toobit.Net.UnitTests
{
    [TestFixture]
    public class RestRequestTests
    {
        [Test]
        public async Task ValidateSpotAccountCalls()
        {
            var client = new ToobitRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<ToobitRestClient>(client, "Endpoints/Spot/Account", "https://api.toobit.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotApi.Account.GetBalancesAsync(), "GetBalances", nestedJsonProperty: "balances");
            await tester.ValidateAsync(client => client.SpotApi.Account.WithdrawAsync("123", "123", 0.1m), "Withdraw");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetWithdrawalsAsync(), "GetWithdrawals");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDepositAddressAsync("123", "123"), "GetDepositAddress");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDepositsAsync("123", "123"), "GetDeposits");
            await tester.ValidateAsync(client => client.SpotApi.Account.TransferAsync(123, 123, AccountType.Spot, AccountType.Futures, "123", 0.1m), "Transfer");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetTransactionHistoryAsync(), "GetTransactionHistory");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetSubAccountsAsync(), "GetSubAccounts");
        }

        [Test]
        public async Task ValidateSpotExchangeDataCalls()
        {
            var client = new ToobitRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<ToobitRestClient>(client, "Endpoints/Spot/ExchangeData", "https://api.toobit.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetExchangeInfoAsync(), "GetExchangeInfo");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetRecentTradesAsync("123"), "GetRecentTrades");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetOrderBookAsync("123"), "GetOrderBook");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetKlinesAsync("123", KlineInterval.OneDay), "GetKlines");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTickersAsync(), "GetTickers");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetPricesAsync(), "GetPrice");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetBookTickersAsync(), "GetBookTickers");
        }

        [Test]
        public async Task ValidateSpotTradingCalls()
        {
            var client = new ToobitRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<ToobitRestClient>(client, "Endpoints/Spot/Trading", "https://api.toobit.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceOrderAsync("ETHUSDT", OrderSide.Buy, OrderType.Market, 1), "PlaceOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOrderAsync(), "CancelOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelAllOrdersAsync(), "CancelAllOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrderAsync(123, "123"), "GetOrder", ignoreProperties: ["stopPrice", "icebergQty"]);
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOpenOrdersAsync(), "GetOpenOrders", ignoreProperties: ["stopPrice", "icebergQty"]);
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrdersAsync(), "GetOrders", ignoreProperties: ["stopPrice", "icebergQty"]);
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetUserTradesAsync("ETHUSDT"), "GetUserTrades");
        }

        [Test]
        public async Task ValidateUsdtFuturesAccountCalls()
        {
            var client = new ToobitRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<ToobitRestClient>(client, "Endpoints/UsdtFutures/Account", "https://api.toobit.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.SetMarginTypeAsync("123", MarginType.Isolated), "SetMarginType");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.SetLeverageAsync("123", 123), "SetLeverage");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetLeverageInfoAsync("123"), "GetLeverageInfo");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetBalancesAsync(), "GetBalances");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.SetPositionMarginAsync("123", PositionSide.Long, 0.1m), "SetPositionMargin");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetTransactionHistoryAsync(), "GetTransactionHistory");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetFeesAsync("123"), "GetFees");
        }

        [Test]
        public async Task ValidateUsdtFuturesExchangeDataCalls()
        {
            var client = new ToobitRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<ToobitRestClient>(client, "Endpoints/UsdtFutures/ExchangeData", "https://api.toobit.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetIndexPricesAsync(), "GetIndexPrices");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetMarkPriceAsync("123"), "GetMarkPrice");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetFundingRateAsync(), "GetFundingRate");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetFundingRateHistoryAsync("123"), "GetFundingRateHistory");
        }


        [Test]
        public async Task ValidateUsdtFuturesTradingCalls()
        {
            var client = new ToobitRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<ToobitRestClient>(client, "Endpoints/UsdtFutures/Trading", "https://api.toobit.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.PlaceOrderAsync("123", FuturesOrderSide.SellOpen, FuturesNewOrderType.Limit, 123), "PlaceOrder");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetOrderAsync(123), "GetOrder");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CancelOrderAsync(123), "CancelOrder");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.CancelAllOrdersAsync("123", OrderSide.Sell), "CancelAllOrders");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetOpenOrdersAsync(), "GetOpenOrders");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetPositionsAsync(), "GetPositions");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.SetTradingStopAsync("123", PositionSide.Long), "SetTradingStop");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetOrderHistoryAsync(), "GetOrderHistory");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Trading.GetUserTradesAsync("123"), "GetUserTrades");
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestUrl?.Contains("signature") == true || result.RequestBody?.Contains("signature=") == true;
        }
    }
}
