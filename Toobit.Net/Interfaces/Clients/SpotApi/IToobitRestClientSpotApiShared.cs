using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Text;

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
        IListenKeyRestClient,
        ITransferRestClient
    {
    }
}
