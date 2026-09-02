using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Toobit.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net;
using System.Linq;
using CryptoExchange.Net.Objects;
using Toobit.Net.Enums;

namespace Toobit.Net.Clients.SpotApi
{
    internal partial class ToobitSocketClientSpotSharedApi :
        SharedApiBase,
        IToobitSocketClientSpotApiShared,
        IToobitSocketClientSpotSharedApi
    {
        private readonly ToobitSocketClientSpotApi _api;

        private const string _topicId = "ToobitSpot";
        private const string _exchangeName = "Toobit";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(ToobitExchange.Metadata, this);

        public ToobitSocketClientSpotSharedApi(ToobitSocketClientSpotApi api)
            : base(
                  api.Exchange,
                  [TradingMode.Spot],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeTickerOptions,
                SubscribeTradeOptions,
                SubscribeKlineOptions,
                SubscribeOrderBookOptions,
                SubscribeBalanceOptions,
                SubscribeSpotOrderOptions,
                SubscribeUserTradeOptions
                );
        }

    }
}
