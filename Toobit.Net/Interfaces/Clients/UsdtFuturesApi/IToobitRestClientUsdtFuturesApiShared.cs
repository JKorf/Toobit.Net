using CryptoExchange.Net.SharedApis;

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
        IFeeRestClient,
        IFuturesTriggerOrderRestClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IToobitRestClientUsdtFuturesSharedApi :
        IGetKlinesRest,
        IGetMarkPriceKlinesRest,
        IGetIndexPriceKlinesRest,
        IGetFuturesSymbolsRest,
        IGetTickerRest,
        IGetAllTickersRest,
        IGetBookTickerRest,
        IGetRecentTradesRest,
        IPlaceFuturesOrderRest,
        IGetFuturesOrderRest,
        IGetOpenFuturesOrdersRest,
        IGetClosedFuturesOrdersRest,
        IGetFuturesOrderTradesRest,
        IGetFuturesUserTradeHistoryRest,
        ICancelFuturesOrderRest,
        IGetPositionsRest,
        IGetFuturesOrderByClientOrderIdRest,
        ICancelFuturesOrderByClientOrderIdRest,
        IGetLeverageRest,
        ISetLeverageRest,
        IGetOrderBookRest,
        IGetFundingRateHistoryRest,
        IGetBalancesRest,
        IGetFeesRest,
        IPlaceFuturesTriggerOrderRest,
        IGetFuturesTriggerOrderRest,
        ICancelFuturesTriggerOrderRest
    {
    }
}
