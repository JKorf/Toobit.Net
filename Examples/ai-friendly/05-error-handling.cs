// 05-error-handling.cs
//
// Demonstrates: HttpResult patterns, retry logic, and common Toobit.Net
// error handling rules.
//
// Setup: dotnet add package Toobit.Net

using CryptoExchange.Net.Objects;
using Toobit.Net;
using Toobit.Net.Clients;
using Toobit.Net.Enums;

var client = new ToobitRestClient(options =>
{
    options.ApiCredentials = new ToobitCredentials("API_KEY", "API_SECRET");
});

var result = await client.SpotApi.ExchangeData.GetTickersAsync("BTCUSDT");

if (result.Success)
{
    Console.WriteLine($"Price: {result.Data.Single().LastPrice}");
}
else
{
    Console.WriteLine($"Code: {result.Error?.Code}");
    Console.WriteLine($"Message: {result.Error?.Message}");
    Console.WriteLine($"Type: {result.Error?.ErrorType}");
    Console.WriteLine($"Transient: {result.Error?.IsTransient}");
}

async Task<HttpResult<T>> WithRetry<T>(
    Func<Task<HttpResult<T>>> call,
    int maxAttempts = 3)
{
    HttpResult<T> last = default!;
    for (var attempt = 1; attempt <= maxAttempts; attempt++)
    {
        last = await call();
        if (last.Success)
            return last;

        if (last.Error?.IsTransient != true)
            return last;

        await Task.Delay(TimeSpan.FromMilliseconds(250 * Math.Pow(2, attempt)));
    }

    return last;
}

var ticker = await WithRetry(() => client.SpotApi.ExchangeData.GetTickersAsync("BTCUSDT"));
if (!ticker.Success)
{
    Console.WriteLine($"Ticker still failed: {ticker.Error}");
}

// Common Toobit.Net guidance:
// - Direct and SharedApis REST methods return HttpResult<T> or HttpResult.
// - Direct and SharedApis WebSocket subscriptions return WebSocketResult<UpdateSubscription>.
// - Shared symbol/cache helpers can return ExchangeCallResult<T>.
// - API errors are returned in result.Error; do not expect normal API failures
//   to be thrown as exceptions.
// - Retry only transient errors. Validation errors, bad credentials, and
//   insufficient balance should be surfaced to the caller.
// - Use ToobitCredentials for private calls.
// - Use "ETH-SWAP-USDT" style symbols for USDT futures.
// - Check exchange info and balances before placing real orders.

var testOrder = await client.SpotApi.Trading.PlaceTestOrderAsync(
    symbol: "BTCUSDT",
    orderSide: OrderSide.Buy,
    orderType: OrderType.Limit,
    quantity: 0.001m,
    timeInForce: TimeInForce.GoodTillCanceled,
    price: 50000m);

if (!testOrder.Success)
{
    var category = testOrder.Error?.IsTransient == true
        ? "Transient, retry with backoff"
        : "Permanent, fix request or surface to user";

    Console.WriteLine($"{category}: {testOrder.Error?.Code} {testOrder.Error?.Message}");
}
