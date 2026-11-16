# Toobit.Net usage

## Imports

```csharp
using System;
using System.Linq;
using System.Threading.Tasks;
using CryptoExchange.Net.SharedApis;
using Toobit.Net;
using Toobit.Net.Clients;
using Toobit.Net.Enums;
using Toobit.Net.Interfaces.Clients;
using Microsoft.Extensions.DependencyInjection;
```

## Public ticker

```csharp
using var client = new ToobitRestClient();

var ticker = await client.SpotApi.ExchangeData.GetTickersAsync("BTCUSDT");
if (!ticker.Success)
{
    Console.WriteLine($"Ticker failed: {ticker.Error}");
    return;
}

Console.WriteLine(ticker.Data.Single().LastPrice);
```

## Credentials

```csharp
using var client = new ToobitRestClient(options =>
{
    options.ApiCredentials = new ToobitCredentials(
        Environment.GetEnvironmentVariable("TOOBIT_KEY") ?? throw new InvalidOperationException("API key is required"),
        Environment.GetEnvironmentVariable("TOOBIT_SECRET") ?? throw new InvalidOperationException("API secret is required"));
});
```

## Spot balances

Use an authenticated client configured as above.

```csharp
var balances = await client.SpotApi.Account.GetBalancesAsync();
if (!balances.Success)
{
    Console.WriteLine($"Balance failed: {balances.Error}");
    return;
}

foreach (var balance in balances.Data.Balances.Where(x => x.Total > 0))
    Console.WriteLine($"{balance.Asset}: {balance.Free}");
```

## Spot test order

Use an authenticated client configured as above.

```csharp
var test = await client.SpotApi.Trading.PlaceTestOrderAsync(
    symbol: "BTCUSDT",
    orderSide: OrderSide.Buy,
    orderType: OrderType.Limit,
    quantity: 0.001m,
    timeInForce: TimeInForce.GoodTillCanceled,
    price: 50000m);

if (!test.Success)
{
    Console.WriteLine($"Test order failed: {test.Error}");
    return;
}
```

## Futures positions

Use an authenticated client configured as above.

```csharp
var positions = await client.UsdtFuturesApi.Trading.GetPositionsAsync("ETH-SWAP-USDT");
if (!positions.Success)
{
    Console.WriteLine($"Positions failed: {positions.Error}");
    return;
}

foreach (var position in positions.Data)
    Console.WriteLine($"{position.Symbol} {position.PositionSide}: {position.Position}");
```

Spot test placement does not submit a live order. Futures placement, leverage and margin changes affect live account state; see [execution considerations](safety.md#usdt-futures-exposure) before adapting a trading example.

## Public subscription lifecycle

```csharp
using var socketClient = new ToobitSocketClient();
var subscription = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync(
    "BTCUSDT", update => Console.WriteLine(update.Data.LastPrice));
if (!subscription.Success)
{
    Console.WriteLine(subscription.Error);
    return;
}
try { await Task.Delay(TimeSpan.FromSeconds(30)); }
finally { await socketClient.UnsubscribeAsync(subscription.Data); }
```

## Private stream with managed listen key

```csharp
var key = Environment.GetEnvironmentVariable("TOOBIT_KEY")
    ?? throw new InvalidOperationException("API key is required");
var secret = Environment.GetEnvironmentVariable("TOOBIT_SECRET")
    ?? throw new InvalidOperationException("API secret is required");
using var socketClient = new ToobitSocketClient(options =>
{
    options.ApiCredentials = new ToobitCredentials(key, secret);
});
var subscription = await socketClient.SpotApi.SubscribeToUserDataUpdatesAsync(
    onAccountMessage: update => Console.WriteLine("Account update"),
    onOrderMessage: update => Console.WriteLine(update.Data.Length),
    onUserTradeMessage: update => Console.WriteLine(update.Data.Length));
if (!subscription.Success)
{
    Console.WriteLine(subscription.Error);
    return;
}
try { await Task.Delay(TimeSpan.FromSeconds(30)); }
finally { await socketClient.UnsubscribeAsync(subscription.Data); }
```

The Futures overload also offers position callbacks. Explicit-key overloads use application-owned listen-key lifecycle; see [private streams](safety.md#credentials-and-private-streams).

## Shared API V2

```csharp
IGetTickerRest ticker = client.SpotApi.SharedApi;
var result = await ticker.GetTickerAsync(new GetTickerRequest(
    new SharedSymbol(TradingMode.Spot, "BTC", "USDT")));
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine(result.Data.LastPrice);
```

Load symbols before reading the catalog:

```csharp
IGetFuturesSymbolsRest symbolsClient = client.UsdtFuturesApi.SharedApi;
var result = await symbolsClient.GetFuturesSymbolsAsync(new GetSymbolsRequest());
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
foreach (var symbol in result.Data.Take(5))
    Console.WriteLine($"{symbol.Name}: trading={symbol.Trading}");
var catalog = symbolsClient.FuturesSymbolCatalog;
```

## Dependency injection and dynamic lookup

```csharp
var services = new ServiceCollection();
services.AddToobit();
using var provider = services.BuildServiceProvider();
var aggregate = provider.GetRequiredService<IToobitSharedApiClient>();
var match = aggregate.GetCapability(
    SharedCapabilities.Tickers.GetTicker.Rest, tradingMode: TradingMode.Spot);
if (match is null)
    return;
var result = await match.Capability.GetTickerAsync(new GetTickerRequest(
    new SharedSymbol(TradingMode.Spot, "BTC", "USDT")));
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine(result.Data.LastPrice);
```

Configure `options.ApiCredentials` for authenticated DI consumers, or REST/socket credentials independently. Inject `IToobitRestClient`, `IToobitSocketClient`, the aggregate or registered narrow capabilities. [API routing](api-surfaces.md#shared-api-v2) describes multiple-exchange selection and surface support.

## Complete programs (checkout-only)

[Examples](../../../../Examples/ai-friendly/README.md) contains independent Spot, Futures, socket, Shared API V2 and public-read retry programs. These links require a checkout; the snippets above work with the installed package.
