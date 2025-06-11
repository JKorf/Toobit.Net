using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Toobit.Net.Interfaces.Clients.UsdtFuturesApi
{
    /// <summary>
    /// Shared interface for UsdtFutures rest API usage
    /// </summary>
    public interface IToobitRestClientUsdtFuturesApiShared :
        IKlineRestClient,
        IMarkPriceKlineRestClient,
        IIndexPriceKlineRestClient,
        IFuturesSymbolRestClient,
        IFuturesTickerRestClient,
        IBookTickerRestClient,
        IRecentTradeRestClient,
        IFuturesOrderRestClient,
        IFuturesOrderClientIdRestClient,
        ILeverageRestClient,
        IOrderBookRestClient,
        IFundingRateRestClient,
        IBalanceRestClient,
        IListenKeyRestClient,
        IFeeRestClient,
        IFuturesTriggerOrderRestClient
    {
    }
}
