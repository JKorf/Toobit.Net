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
        IGetKlinesEndpoint,
        IGetMarkPriceKlinesEndpoint,
        IGetIndexPriceKlinesEndpoint,
        IGetFuturesSymbolsEndpoint,
        IGetFuturesTickerEndpoint,
        IGetAllFuturesTickersEndpoint,
        IGetBookTickerEndpoint,
        IGetRecentTradesEndpoint,
        IPlaceFuturesOrderEndpoint,
        IGetFuturesOrderEndpoint,
        IGetOpenFuturesOrdersEndpoint,
        IGetClosedFuturesOrdersEndpoint,
        IGetFuturesOrderTradesEndpoint,
        IGetFuturesUserTradeHistoryEndpoint,
        ICancelFuturesOrderEndpoint,
        IGetPositionsEndpoint,
        IClosePositionEndpoint,
        IGetFuturesOrderByClientOrderIdEndpoint,
        ICancelFuturesOrderByClientOrderIdEndpoint,
        IGetLeverageEndpoint,
        ISetLeverageEndpoint,
        IGetOrderBookEndpoint,
        IGetFundingRateHistoryEndpoint,
        IGetBalancesEndpoint,
        IGetFeesEndpoint,
        IPlaceFuturesTriggerOrderEndpoint,
        IGetFuturesTriggerOrderEndpoint,
        ICancelFuturesTriggerOrderEndpoint
    {
    }
}
