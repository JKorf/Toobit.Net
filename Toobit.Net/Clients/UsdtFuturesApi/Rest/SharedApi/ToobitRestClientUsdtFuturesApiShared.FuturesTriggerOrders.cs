using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Toobit.Net.Enums;
using Toobit.Net.Interfaces.Clients.UsdtFuturesApi;
using Toobit.Net.Objects.Models;

namespace Toobit.Net.Clients.UsdtFuturesApi
{
    internal partial class ToobitRestClientUsdtFuturesSharedApi
    {
        #region Trigger Order Client
        public PlaceFuturesTriggerOrderOptions PlaceFuturesTriggerOrderOptions { get; } = new PlaceFuturesTriggerOrderOptions(_exchangeName, false);
        public async Task<HttpResult<SharedId>> PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
        {
            var side = GetTriggerOrderSide(request);
            var validationError = PlaceFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                side,
                FuturesNewOrderType.Stop,
                (long)(request.Quantity.QuantityInContracts ?? 0),
                timeInForce: request.OrderPrice == null ? null : TimeInForce.GoodTillCanceled,
                price: request.OrderPrice,
                stopPrice: request.TriggerPrice,
                clientOrderId: request.ClientOrderId,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data.OrderId.ToString()));
        }

        public GetFuturesTriggerOrderOptions GetFuturesTriggerOrderOptions { get; } = new GetFuturesTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesTriggerOrder>> GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var id))
                throw new ArgumentException($"Invalid order id");

            var order = await _api.Trading.GetOrderAsync(
                id,
                orderType: FuturesOrderType.Stop,
                ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(order);

            // Return
            return HttpResult.Ok(order, new SharedFuturesTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.OrderId.ToString(),
                order.Data.PriceType == PriceType.Market ? SharedOrderType.Market : SharedOrderType.Limit,
                order.Data.OrderSide == FuturesOrderSide.BuyOpen || order.Data.OrderSide == FuturesOrderSide.SellOpen ? SharedTriggerOrderDirection.Enter : SharedTriggerOrderDirection.Exit,
                ParseTriggerStatus(order.Data),
                order.Data.StopPrice ?? 0,
                order.Data.OrderSide == FuturesOrderSide.BuyOpen || order.Data.OrderSide == FuturesOrderSide.SellClose ? SharedPositionSide.Long : SharedPositionSide.Short,
                order.Data.CreateTime
                )
            {
                AveragePrice = order.Data.AveragePrice == 0 ? null : order.Data.AveragePrice,
                OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Data.Quantity),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: order.Data.QuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                UpdateTime = order.Data.UpdateTime,
                PositionSide = order.Data.OrderSide == FuturesOrderSide.BuyOpen || order.Data.OrderSide == FuturesOrderSide.SellClose ? SharedPositionSide.Long : SharedPositionSide.Short, 
                PlacedOrderId = order.Data.OrderId.ToString(),
                ClientOrderId = order.Data.ClientOrderId
            });
        }

        private SharedTriggerOrderStatus ParseTriggerStatus(ToobitFuturesOrder data)
        {
            if (data.Status == OrderStatus.Filled)
                return SharedTriggerOrderStatus.Filled;

            if (data.Status == OrderStatus.Canceled || data.Status == OrderStatus.Rejected)
                return SharedTriggerOrderStatus.CanceledOrRejected;

            if (data.Status == OrderStatus.New
                || data.Status == OrderStatus.PartiallyFilled
                || data.Status == OrderStatus.PendingCancel)
            {
                return SharedTriggerOrderStatus.Active;
            }

            return SharedTriggerOrderStatus.Unknown;
        }

        public CancelFuturesTriggerOrderOptions CancelFuturesTriggerOrderOptions { get; } = new CancelFuturesTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var order = await _api.Trading.CancelOrderAsync(orderId, orderType: FuturesOrderType.Stop, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data.OrderId.ToString()));
        }

        private FuturesOrderSide GetTriggerOrderSide(PlaceFuturesTriggerOrderRequest request)
        {
            if (request.PositionSide == SharedPositionSide.Long)
            {
                if (request.OrderDirection == SharedTriggerOrderDirection.Enter)
                    return FuturesOrderSide.BuyOpen;
                return FuturesOrderSide.SellClose;
            }

            if (request.OrderDirection == SharedTriggerOrderDirection.Enter)
                return FuturesOrderSide.SellOpen;
            return FuturesOrderSide.BuyClose;
        }

        #endregion
    }
}
