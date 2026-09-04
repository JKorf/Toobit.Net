using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Toobit.Net.Clients.SpotApi;
using Toobit.Net.Enums;
using Toobit.Net.Interfaces.Clients.UsdtFuturesApi;

namespace Toobit.Net.Clients.UsdtFuturesApi
{
    internal partial class ToobitSocketClientUsdtFuturesSharedApi
    {
        #region Subscribe Balances

        public SubscribeBalanceOptions SubscribeBalanceOptions { get; } = new SubscribeBalanceOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBalanceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToUserDataUpdatesAsync(
                onAccountMessage: update => handler(update.ToType(update.Data.Balances.Select(x => 
                    new SharedBalance(SupportedTradingModes, x.Asset, x.Free, x.Free + x.Locked)).ToArray())),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion

    }
}
