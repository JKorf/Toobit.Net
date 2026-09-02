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
        #region Spot Order client

        async Task<WebSocketResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrder[]>> handler, CancellationToken ct)
            => await SubscribeToSpotOrderUpdatesAsync(request, x => handler(x.ToType<SharedSpotOrder[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeSpotOrderOptions SubscribeSpotOrderOptions { get; } = new SubscribeSpotOrderOptions(_exchangeName, true);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToUserDataUpdatesAsync(
                onOrderMessage: update => handler(update.ToType(update.Data.Select(x => 
                    new SharedSpotOrderUpdate(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                        x.Symbol,
                        x.OrderId.ToString(),
                        ParseOrderType(x.OrderType),
                        x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(x.Status),
                        x.CreateTime)
                    {
                        ClientOrderId = x.ClientOrderId,
                        OrderPrice = x.Price == 0 ? null : x.Price,
                        OrderQuantity = new SharedOrderQuantity(x.OrderSide == OrderSide.Buy && x.OrderType == OrderType.Market ? null : x.Quantity, x.OrderSide == OrderSide.Buy && x.OrderType == OrderType.Market ? x.Quantity : null),
                        QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.QuoteQuantityFilled),
                        UpdateTime = x.EventTime,
#pragma warning disable CS0618 // Type or member is obsolete
                        Fee = x.Fee,
                        FeeAsset = x.FeeAsset,
#pragma warning restore CS0618 // Type or member is obsolete
                        TimeInForce = x.TimeInForce == Enums.TimeInForce.ImmediateOrCancel ? SharedTimeInForce.ImmediateOrCancel : x.TimeInForce == Enums.TimeInForce.FillOrKill ? SharedTimeInForce.FillOrKill : SharedTimeInForce.GoodTillCanceled,
                        LastTrade = x.LastFillQuantity > 0 ? 
                            new SharedUserTrade(
                                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                                x.Symbol,
                                x.OrderId.ToString(), 
                                x.LastTradeId!.ToString()!,
                                x.OrderSide == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell, 
                                new SharedOrderQuantity(x.LastFillQuantity.Value),
                                x.LastFillPrice!.Value,
                                x.EventTime)
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

        private SharedOrderType ParseOrderType(OrderType type)
        {
            if (type == OrderType.Market)
                return SharedOrderType.Market;

            if (type == OrderType.Limit)
                return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }
        #endregion
    }
}
