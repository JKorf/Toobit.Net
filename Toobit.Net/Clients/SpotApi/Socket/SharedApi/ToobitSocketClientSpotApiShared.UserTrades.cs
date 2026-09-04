using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Toobit.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net;
using System.Linq;
using CryptoExchange.Net.Objects;
using Toobit.Net.Enums;

namespace Toobit.Net.Clients.SpotApi
{
    internal partial class ToobitSocketClientSpotSharedApi
    {

        #region Subscribe User Trades

        public SubscribeUserTradeOptions SubscribeUserTradeOptions { get; } = new SubscribeUserTradeOptions(_exchangeName, true);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(SubscribeUserTradeRequest request, Action<DataEvent<SharedUserTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeUserTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToUserDataUpdatesAsync(
                onUserTradeMessage: update =>
                {
                    // Filter for spot updates
                    var data = update.Data.Where(x => !x.Symbol.Contains("-SWAP-"));
                    if (!data.Any())
                        return;

                    handler(update.ToType(data.Select(x =>
                        new SharedUserTrade(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                            x.Symbol,
                            x.OrderId.ToString(),
                            x.TradeId,
                            x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            new SharedOrderQuantity(x.Quantity),
                            x.Price,
                            x.Timestamp)
                        {
                            ClientOrderId = x.ClientOrderId,
                            Role = x.IsMaker ? SharedRole.Maker : SharedRole.Taker
                        }
                    ).ToArray()));
                },
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
