using Toobit.Net;
using Toobit.Net.Clients;
using Toobit.Net.Enums;

const string spotSymbol = "BTCUSDT";
const string futuresSymbol = "ETH-SWAP-USDT";

// Replace with valid credentials or order placement will always fail
var apiKey = "KEY";
var apiSecret = "SECRET";

Console.WriteLine("Toobit.Net order placement example");
Console.WriteLine();
Console.WriteLine("This example can place real orders when valid credentials are configured.");
Console.WriteLine();

var client = new ToobitRestClient(options =>
{
    options.ApiCredentials = new ToobitCredentials(apiKey, apiSecret);
});

await PlaceSpotLimitOrderAsync(client);
Console.WriteLine();
await PlaceFuturesCloseOrderExampleAsync(client);

static async Task PlaceSpotLimitOrderAsync(ToobitRestClient client)
{
    Console.WriteLine($"Placing spot limit buy order for {spotSymbol}...");

    var ticker = await client.SpotApi.ExchangeData.GetTickersAsync(spotSymbol);
    if (!ticker.Success)
    {
        Console.WriteLine($"Failed to get spot ticker: {ticker.Error}");
        return;
    }

    var lastPrice = ticker.Data.Single().LastPrice;
    if (lastPrice == null)
    {
        Console.WriteLine("Spot ticker did not include a last price");
        return;
    }

    var safePrice = Math.Round(lastPrice.Value * 0.95m, 2);
    var order = await client.SpotApi.Trading.PlaceOrderAsync(
        symbol: spotSymbol,
        orderSide: OrderSide.Buy,
        orderType: OrderType.Limit,
        quantity: 0.001m,
        price: safePrice,
        timeInForce: TimeInForce.GoodTillCanceled);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place spot order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed spot order {order.Data.OrderId}, status: {order.Data.Status}");

    var orderStatus = await client.SpotApi.Trading.GetOrderAsync(orderId: order.Data.OrderId);
    if (orderStatus.Success)
        Console.WriteLine($"Spot order status: {orderStatus.Data.Status}, filled: {orderStatus.Data.QuantityFilled}");
    else
        Console.WriteLine($"Failed to query spot order: {orderStatus.Error}");

    var cancel = await client.SpotApi.Trading.CancelOrderAsync(orderId: order.Data.OrderId);
    Console.WriteLine(cancel.Success
        ? $"Cancelled spot order {order.Data.OrderId}"
        : $"Failed to cancel spot order: {cancel.Error}");
}

static async Task PlaceFuturesCloseOrderExampleAsync(ToobitRestClient client)
{
    Console.WriteLine($"Placing futures limit close order for {futuresSymbol}...");

    var ticker = await client.UsdtFuturesApi.ExchangeData.GetTickersAsync(futuresSymbol);
    if (!ticker.Success)
    {
        Console.WriteLine($"Failed to get futures ticker: {ticker.Error}");
        return;
    }

    var lastPrice = ticker.Data.Single().LastPrice;
    if (lastPrice == null)
    {
        Console.WriteLine("Futures ticker did not include a last price");
        return;
    }

    var safePrice = Math.Round(lastPrice.Value * 1.05m, 2);
    var order = await client.UsdtFuturesApi.Trading.PlaceOrderAsync(
        symbol: futuresSymbol,
        orderSide: FuturesOrderSide.SellClose,
        orderType: FuturesNewOrderType.Limit,
        quantity: 1,
        price: safePrice,
        timeInForce: TimeInForce.GoodTillCanceled);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place futures order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed futures order {order.Data.OrderId}, status: {order.Data.Status}");

    var orderStatus = await client.UsdtFuturesApi.Trading.GetOrderAsync(orderId: order.Data.OrderId);
    if (orderStatus.Success)
        Console.WriteLine($"Futures order status: {orderStatus.Data.Status}, executed: {orderStatus.Data.QuantityFilled}");
    else
        Console.WriteLine($"Failed to query futures order: {orderStatus.Error}");

    var cancel = await client.UsdtFuturesApi.Trading.CancelOrderAsync(orderId: order.Data.OrderId);
    Console.WriteLine(cancel.Success
        ? $"Cancelled futures order {order.Data.OrderId}"
        : $"Failed to cancel futures order: {cancel.Error}");
}
