using CryptoExchange.Net.SharedApis;

namespace Toobit.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Shared interface for Spot rest API usage
    /// </summary>
    public interface IToobitRestClientSpotApiShared :
        IKlineRestClient,
        ISpotSymbolRestClient,
        ISpotTickerRestClient,
        IBookTickerRestClient,
        IRecentTradeRestClient,
        IOrderBookRestClient,
        IBalanceRestClient,
        ISpotOrderRestClient,
        ISpotOrderClientIdRestClient,
        IAssetsRestClient,
        IDepositRestClient,
        IWithdrawalRestClient,
        IWithdrawRestClient,
        ITransferRestClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IToobitRestClientSpotSharedApi :
        IGetKlinesRest,
        IGetSpotSymbolsRest,
        IGetSpotTickerRest,
        IGetAllSpotTickersRest,
        IGetBookTickerRest,
        IGetRecentTradesRest,
        IGetOrderBookRest,
        IGetBalancesRest,
        IPlaceSpotOrderRest,
        IGetSpotOrderRest,
        IGetOpenSpotOrdersRest,
        IGetClosedSpotOrdersRest,
        IGetSpotOrderTradesRest,
        IGetSpotUserTradeHistoryRest,
        ICancelSpotOrderRest,
        IGetSpotOrderByClientOrderIdRest,
        ICancelSpotOrderByClientOrderIdRest,
        IGetAssetRest,
        IGetAllAssetsRest,
        IGetDepositAddressesRest,
        IGetDepositHistoryRest,
        IGetWithdrawalHistoryRest,
        IWithdrawRest,
        ITransferRest
    {
    }
}
