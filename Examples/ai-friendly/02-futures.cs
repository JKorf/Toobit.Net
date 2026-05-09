// 02-futures.cs
//
// Demonstrates: Toobit USDT futures client usage. Toobit futures symbols use
// contract names such as "ETH-SWAP-USDT", not spot symbols such as "ETHUSDT".
//
// Setup: dotnet add package Toobit.Net

using Toobit.Net;
using Toobit.Net.Clients;
using Toobit.Net.Enums;

var client = new ToobitRestClient(options =>
{
    options.ApiCredentials = new ToobitCredentials("API_KEY", "API_SECRET");
});

const string symbol = "ETH-SWAP-USDT";

var leverageResult = await client.UsdtFuturesApi.Account.SetLeverageAsync(symbol, 10);
if (!leverageResult.Success)
{
    Console.WriteLine($"Set leverage failed: {leverageResult.Error}");
    return;
}

var orderResult = await client.UsdtFuturesApi.Trading.PlaceOrderAsync(
    symbol: symbol,
    orderSide: FuturesOrderSide.BuyOpen,
    orderType: FuturesNewOrderType.Limit,
    quantity: 10,
    price: 2000m,
    timeInForce: TimeInForce.GoodTillCanceled);

if (!orderResult.Success)
{
    Console.WriteLine($"Open order failed: {orderResult.Error}");
    return;
}

Console.WriteLine($"Opened futures order id: {orderResult.Data.OrderId}");

var positionsResult = await client.UsdtFuturesApi.Trading.GetPositionsAsync(symbol);
if (!positionsResult.Success)
{
    Console.WriteLine($"Position request failed: {positionsResult.Error}");
    return;
}

foreach (var position in positionsResult.Data)
    Console.WriteLine($"{position.Symbol} {position.PositionSide}: {position.Position}");

// Close long exposure with SellClose. Use BuyClose to close a short position.
var closeResult = await client.UsdtFuturesApi.Trading.PlaceOrderAsync(
    symbol: symbol,
    orderSide: FuturesOrderSide.SellClose,
    orderType: FuturesNewOrderType.Limit,
    quantity: 10,
    price: 2100m,
    timeInForce: TimeInForce.GoodTillCanceled);

if (!closeResult.Success)
    Console.WriteLine($"Close order failed: {closeResult.Error}");
