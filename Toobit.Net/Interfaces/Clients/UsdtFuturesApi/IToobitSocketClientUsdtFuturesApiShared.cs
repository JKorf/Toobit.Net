using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Toobit.Net.Interfaces.Clients.UsdtFuturesApi
{
    /// <summary>
    /// Shared interface for UsdtFutures socket API usage
    /// </summary>
    public interface IToobitSocketClientUsdtFuturesApiShared :
        ITickerSocketClient,
        ITradeSocketClient,
        IBalanceSocketClient,
        IKlineSocketClient,
        IOrderBookSocketClient,
        IFuturesOrderSocketClient,
        IPositionSocketClient,
        IUserTradeSocketClient
    {
    }
}
