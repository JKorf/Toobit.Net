using CryptoExchange.Net.SharedApis;

namespace Toobit.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Shared interface for Spot socket API usage
    /// </summary>
    public interface IToobitSocketClientSpotApiShared :
        ITickerSocketClient,
        ITradeSocketClient,
        IKlineSocketClient,
        IOrderBookSocketClient,
        IBalanceSocketClient,
        ISpotOrderSocketClient,
        IUserTradeSocketClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IToobitSocketClientSpotSharedApi :
        ISubscribeTickerOperation,
        ISubscribeTradesOperation,
        ISubscribeKlinesOperation,
        ISubscribeOrderBookOperation,
        ISubscribeBalancesOperation,
        ISubscribeSpotOrdersOperation,
        ISubscribeUserTradesOperation
    {
    }
}
