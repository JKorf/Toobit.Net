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
        IGetKlinesEndpoint,
        IGetSpotSymbolsEndpoint,
        IGetSpotTickerEndpoint,
        IGetAllSpotTickersEndpoint,
        IGetBookTickerEndpoint,
        IGetRecentTradesEndpoint,
        IGetOrderBookEndpoint,
        IGetBalancesEndpoint,
        IPlaceSpotOrderEndpoint,
        IGetSpotOrderEndpoint,
        IGetOpenSpotOrdersEndpoint,
        IGetClosedSpotOrdersEndpoint,
        IGetSpotOrderTradesEndpoint,
        IGetSpotUserTradeHistoryEndpoint,
        ICancelSpotOrderEndpoint,
        IGetSpotOrderByClientOrderIdEndpoint,
        ICancelSpotOrderByClientOrderIdEndpoint,
        IGetAssetEndpoint,
        IGetAllAssetsEndpoint,
        IGetDepositAddressesEndpoint,
        IGetDepositHistoryEndpoint,
        IGetWithdrawalHistoryEndpoint,
        IWithdrawEndpoint,
        ITransferEndpoint
    {
    }
}
