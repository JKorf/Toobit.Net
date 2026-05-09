// 01-spot-quickstart.cs
//
// Demonstrates: ToobitRestClient setup, public spot ticker, authenticated
// balances, limit order placement, and order status lookup.
//
// Setup: dotnet add package Toobit.Net

using Toobit.Net;
using Toobit.Net.Clients;
using Toobit.Net.Enums;

var publicClient = new ToobitRestClient();

// Public market data does not require API credentials.
var tickerResult = await publicClient.SpotApi.ExchangeData.GetTickersAsync("BTCUSDT");
if (!tickerResult.Success)
{
    Console.WriteLine($"Ticker request failed: {tickerResult.Error}");
    return;
}

var ticker = tickerResult.Data.Single();
Console.WriteLine($"{ticker.Symbol} last price: {ticker.LastPrice}");

// Private endpoints require ToobitCredentials.
var restClient = new ToobitRestClient(options =>
{
    options.ApiCredentials = new ToobitCredentials("API_KEY", "API_SECRET");
});

var balancesResult = await restClient.SpotApi.Account.GetBalancesAsync();
if (!balancesResult.Success)
{
    Console.WriteLine($"Balance request failed: {balancesResult.Error}");
    return;
}

foreach (var balance in balancesResult.Data.Balances.Where(x => x.Total > 0))
    Console.WriteLine($"{balance.Asset}: free={balance.Free}, locked={balance.Locked}");

// Example only: verify symbol filters and balances before placing real orders.
var orderResult = await restClient.SpotApi.Trading.PlaceOrderAsync(
    symbol: "BTCUSDT",
    orderSide: OrderSide.Buy,
    orderType: OrderType.Limit,
    quantity: 0.001m,
    timeInForce: TimeInForce.GoodTillCanceled,
    price: 50000m);

if (!orderResult.Success)
{
    Console.WriteLine($"Order failed: {orderResult.Error}");
    return;
}

Console.WriteLine($"Placed order id: {orderResult.Data.OrderId}");

var statusResult = await restClient.SpotApi.Trading.GetOrderAsync(orderId: orderResult.Data.OrderId);
if (!statusResult.Success)
{
    Console.WriteLine($"Order status failed: {statusResult.Error}");
    return;
}

Console.WriteLine($"Order status: {statusResult.Data.Status}");
