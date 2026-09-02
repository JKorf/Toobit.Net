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
        #region Futures Order client

        async Task<WebSocketResult<UpdateSubscription>> IFuturesOrderSocketClient.SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrder[]>> handler, CancellationToken ct)
            => await SubscribeToFuturesOrderUpdatesAsync(request, x => handler(x.ToType<SharedFuturesOrder[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeFuturesOrderOptions SubscribeFuturesOrderOptions { get; } = new SubscribeFuturesOrderOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToUserDataUpdatesAsync(
                onOrderMessage: update => handler(update.ToType(update.Data.Select(x =>
                    new SharedFuturesOrderUpdate(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol), x.Symbol,
                        x.OrderId.ToString(),
                        (x.PriceType == Enums.PriceType.Market || x.OrderType == Enums.FuturesUpdateOrderType.Market) ? SharedOrderType.Market : x.OrderType != Enums.FuturesUpdateOrderType.Limit ? SharedOrderType.Other : SharedOrderType.Limit,
                        x.OrderSide == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(x.Status),
                        x.UpdateTime)
                    {
                        ClientOrderId = x.ClientOrderId,
                        OrderPrice = x.Price == 0 ? null : x.Price,
                        OrderQuantity = new SharedOrderQuantity(contractQuantity: x.Quantity),
                        QuantityFilled = new SharedOrderQuantity(contractQuantity : x.QuantityFilled),
                        UpdateTime = x.UpdateTime,
#pragma warning disable CS0618 // Type or member is obsolete
                        Fee = x.Fee,
                        FeeAsset = x.FeeAsset,
#pragma warning restore CS0618 // Type or member is obsolete
                        TimeInForce = x.TimeInForce == Enums.TimeInForce.ImmediateOrCancel ? SharedTimeInForce.ImmediateOrCancel : x.TimeInForce == Enums.TimeInForce.FillOrKill ? SharedTimeInForce.FillOrKill : SharedTimeInForce.GoodTillCanceled,
                        IsCloseOrder = x.IsCloseOrder,
                        LastTrade = x.LastFillQuantity > 0 ? 
                            new SharedUserTrade(
                                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol), 
                                x.Symbol,
                                x.OrderId.ToString(),
                                x.LastTradeId?.ToString()!,
                                x.OrderSide == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                new SharedOrderQuantity(contractQuantity: x.LastFillQuantity),
                                x.LastFillPrice ?? 0,
                                x.UpdateTime)
                            {
                                ClientOrderId = x.ClientOrderId,
                                Role = x.IsMaker ? SharedRole.Maker : SharedRole.Taker
                            } : null
                    }
                ).ToArray())),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == Enums.OrderStatus.Canceled || status == Enums.OrderStatus.Rejected || status == OrderStatus.PartiallyCanceled)
                return SharedOrderStatus.Canceled;
            if (status == Enums.OrderStatus.New || status == Enums.OrderStatus.PartiallyFilled || status == Enums.OrderStatus.PendingCancel)
                return SharedOrderStatus.Open;
            if (status == OrderStatus.Filled)
                return SharedOrderStatus.Filled;

            return SharedOrderStatus.Unknown;
        }

        #endregion
    }
}
