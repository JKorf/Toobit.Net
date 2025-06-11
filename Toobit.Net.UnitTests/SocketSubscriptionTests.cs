using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System.Threading.Tasks;
using Toobit.Net.Clients;
using Toobit.Net.Objects.Models;

namespace Toobit.Net.UnitTests
{
    [TestFixture]
    public class SocketSubscriptionTests
    {
        [Test]
        public async Task ValidateSpotSubscriptions()
        {
            var client = new ToobitSocketClient(opts =>
            {
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new SocketSubscriptionValidator<ToobitSocketClient>(client, "Subscriptions/Spot", "wss://stream.toobit.com");
            await tester.ValidateAsync<ToobitTradeUpdate[]>((client, handler) => client.SpotApi.SubscribeToTradeUpdatesAsync("ETHUSDT", handler), "Trades", ignoreProperties: ["v"], nestedJsonProperty: "data");
            await tester.ValidateAsync<ToobitKlineUpdate>((client, handler) => client.SpotApi.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneDay, handler), "Klines", ignoreProperties: ["sn"], nestedJsonProperty: "data");
            await tester.ValidateAsync<ToobitTickerUpdate>((client, handler) => client.SpotApi.SubscribeToTickerUpdatesAsync("ETHUSDT", handler), "Tickers", nestedJsonProperty: "data");
            await tester.ValidateAsync<ToobitOrderBookUpdate>((client, handler) => client.SpotApi.SubscribeToPartialOrderBookUpdatesAsync("ETHUSDT", handler), "PartialBook", nestedJsonProperty: "data");
            await tester.ValidateAsync<ToobitOrderBookUpdate>((client, handler) => client.SpotApi.SubscribeToOrderBookUpdatesAsync("ETHUSDT", handler), "OrderBook", nestedJsonProperty: "data");
            await tester.ValidateAsync<ToobitAccountUpdate>((client, handler) => client.SpotApi.SubscribeToUserDataUpdatesAsync("123", handler), "UserAccount");
            await tester.ValidateAsync<ToobitOrderUpdate[]>((client, handler) => client.SpotApi.SubscribeToUserDataUpdatesAsync("123", null, handler), "UserOrder", ignoreProperties: ["u"]);
            await tester.ValidateAsync<ToobitUserTradeUpdate[]>((client, handler) => client.SpotApi.SubscribeToUserDataUpdatesAsync("123", null, null, handler), "UserTrade");
        }

        [Test]
        public async Task ValidateFuturesSubscriptions()
        {
            var client = new ToobitSocketClient(opts =>
            {
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new SocketSubscriptionValidator<ToobitSocketClient>(client, "Subscriptions/UsdtFutures", "wss://stream.toobit.com");

            await tester.ValidateAsync<ToobitAccountUpdate>((client, handler) => client.UsdtFuturesApi.SubscribeToUserDataUpdatesAsync("123", handler), "UserAccount", useFirstUpdateItem: true);
            await tester.ValidateAsync<ToobitFuturesOrderUpdate[]>((client, handler) => client.UsdtFuturesApi.SubscribeToUserDataUpdatesAsync("123", null, handler), "UserOrder", ignoreProperties: ["u"]);
            await tester.ValidateAsync<ToobitPositionUpdate[]>((client, handler) => client.UsdtFuturesApi.SubscribeToUserDataUpdatesAsync("123", null, null, handler), "UserPosition");
            await tester.ValidateAsync<ToobitUserTradeUpdate[]>((client, handler) => client.UsdtFuturesApi.SubscribeToUserDataUpdatesAsync("123", null, null, null, handler), "UserTrade");
        }
    }
}
