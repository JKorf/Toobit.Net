using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Toobit.Net.Enums;
using Toobit.Net.Interfaces.Clients.UsdtFuturesApi;
using Toobit.Net.Objects.Models;

namespace Toobit.Net.Clients.UsdtFuturesApi
{
    internal partial class ToobitRestClientUsdtFuturesSharedApi :
        SharedApiBase,
        IToobitRestClientUsdtFuturesSharedApi,
        IToobitRestClientUsdtFuturesApiShared
    {
        private readonly ToobitRestClientUsdtFuturesApi _api;

        private const string _topicId = "ToobitUsdtFutures";
        private const string _exchangeName = "Toobit";
        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(ToobitExchange.Metadata, this);

        private static readonly HashSet<string> _fiatCurrencies = ["USD", "EUR"];

        public ToobitRestClientUsdtFuturesSharedApi(ToobitRestClientUsdtFuturesApi api)
            : base(
                  SharedTransport.Rest,
                  api.Exchange,
                  [TradingMode.PerpetualLinear],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetKlinesOptions,
                GetMarkPriceKlinesOptions,
                GetIndexPriceKlinesOptions,
                GetFuturesSymbolsOptions,
                GetFuturesTickerOptions,
                GetAllFuturesTickersOptions,
                GetBookTickerOptions,
                GetRecentTradesOptions,
                PlaceFuturesOrderOptions,
                GetFuturesOrderOptions,
                GetOpenFuturesOrdersOptions,
                GetClosedFuturesOrdersOptions,
                GetFuturesOrderTradesOptions,
                GetFuturesUserTradeHistoryOptions,
                CancelFuturesOrderOptions,
                GetPositionsOptions,
                ClosePositionOptions,
                GetFuturesOrderByClientOrderIdOptions,
                CancelFuturesOrderByClientOrderIdOptions,
                GetLeverageOptions,
                SetLeverageOptions,
                GetOrderBookOptions,
                GetFundingRateHistoryOptions,
                GetBalancesOptions,
                GetFeeOptions,
                PlaceFuturesTriggerOrderOptions,
                GetFuturesTriggerOrderOptions,
                CancelFuturesTriggerOrderOptions
                );

        }

    }
}
