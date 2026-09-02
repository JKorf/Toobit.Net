using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Toobit.Net.Clients.SpotApi;
using Toobit.Net.Enums;
using Toobit.Net.Interfaces.Clients.UsdtFuturesApi;

namespace Toobit.Net.Clients.UsdtFuturesApi
{
    internal partial class ToobitSocketClientUsdtFuturesSharedApi :
        SharedApiBase,
        IToobitSocketClientUsdtFuturesSharedApi,
        IToobitSocketClientUsdtFuturesApiShared
    {
        private readonly ToobitSocketClientUsdtFuturesApi _api;

        private const string _topicId = "ToobitUsdtFutures";
        private const string _exchangeName = "Toobit";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(ToobitExchange.Metadata, this);

        public ToobitSocketClientUsdtFuturesSharedApi(ToobitSocketClientUsdtFuturesApi api)
            : base(
                  api.Exchange,
                  [TradingMode.PerpetualLinear],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeTickerOptions,
                SubscribeTradeOptions,
                SubscribeBalanceOptions,
                SubscribeKlineOptions,
                SubscribeOrderBookOptions,
                SubscribeFuturesOrderOptions,
                SubscribePositionOptions,
                SubscribeUserTradeOptions
                );
        }

    }
}
