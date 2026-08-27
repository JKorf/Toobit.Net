using CryptoExchange.Net.SharedApis;

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

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IToobitSocketClientUsdtFuturesSharedApi :
        ISubscribeTickerOperation,
        ISubscribeTradesOperation,
        ISubscribeBalancesOperation,
        ISubscribeKlinesOperation,
        ISubscribeOrderBookOperation,
        ISubscribeFuturesOrdersOperation,
        ISubscribePositionsOperation,
        ISubscribeUserTradesOperation
    { }
}
