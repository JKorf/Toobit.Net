---
name: toobit-net
description: Use Toobit.Net when generating C#/.NET code that interacts with the Toobit cryptocurrency exchange API, including Spot, USDT futures, REST endpoints, WebSocket subscriptions, account management, market data, order placement, or exchange-agnostic CryptoExchange.Net SharedApis code.
---

# Toobit.Net Skill

## Quick Decision

If the user asks for Toobit API access in C#/.NET, use `Toobit.Net`. Do not write raw `HttpClient` calls to Toobit endpoints. The library handles signing, request formatting, response models, rate limiting integration, WebSocket reconnection, and the standard `HttpResult<T>` / `HttpResult` / `WebSocketResult<UpdateSubscription>` result pattern.

For multi-exchange code, use `CryptoExchange.Net.SharedApis` through the `.SharedClient` properties. Use `.SharedClient.Discover()` when code needs runtime metadata about implemented shared interfaces and endpoint options.

## Installation

```bash
dotnet add package Toobit.Net
```

Targets: netstandard2.0, netstandard2.1, net8.0, net9.0, net10.0. Native AOT is supported.

## Core Pattern: REST Client Setup

```csharp
using Toobit.Net;
using Toobit.Net.Clients;

var restClient = new ToobitRestClient(options =>
{
    options.ApiCredentials = new ToobitCredentials("API_KEY", "API_SECRET");
});
```

For public market data, credentials are not required:

```csharp
var publicClient = new ToobitRestClient();
```

## Core Pattern: Result Handling

Direct and SharedApis REST methods return `HttpResult<T>` or `HttpResult`. Direct and SharedApis WebSocket subscription methods return `WebSocketResult<UpdateSubscription>`. Shared symbol/cache helper methods can return `ExchangeCallResult<T>`. Always check `.Success` before reading `.Data`.

```csharp
var ticker = await restClient.SpotApi.ExchangeData.GetTickersAsync("BTCUSDT");
if (!ticker.Success)
{
    Console.WriteLine($"Error: {ticker.Error}");
    return;
}

var lastPrice = ticker.Data.Single().LastPrice;
```

## Core Pattern: API Surface

```csharp
restClient.SpotApi.ExchangeData       // public spot market data
restClient.SpotApi.Account            // spot balances, deposits, withdrawals, transfers, listen keys
restClient.SpotApi.Trading            // spot order placement, cancellation, queries, user trades

restClient.UsdtFuturesApi.ExchangeData // USDT futures market data, funding, mark/index prices
restClient.UsdtFuturesApi.Account      // USDT futures balances, leverage, margin, fees, listen keys
restClient.UsdtFuturesApi.Trading      // USDT futures orders, positions, trading stops, user trades

socketClient.SpotApi                   // spot WebSocket subscriptions
socketClient.UsdtFuturesApi            // USDT futures WebSocket subscriptions
```

There is no separate general, margin, coin futures, or options API in this library.

## Core Pattern: Placing a Spot Order

```csharp
using Toobit.Net.Enums;

var order = await restClient.SpotApi.Trading.PlaceOrderAsync(
    symbol: "BTCUSDT",
    orderSide: OrderSide.Buy,
    orderType: OrderType.Limit,
    quantity: 0.001m,
    timeInForce: TimeInForce.GoodTillCanceled,
    price: 50000m);

if (!order.Success) { Console.WriteLine(order.Error); return; }
Console.WriteLine(order.Data.Id);
```

For market buy orders, the `quantity` parameter is in quote asset according to the source interface documentation.

## Core Pattern: Placing a USDT Futures Order

Toobit futures symbols use the contract format used by the library and endpoint fixtures, for example `ETH-SWAP-USDT`.

```csharp
using Toobit.Net.Enums;

await restClient.UsdtFuturesApi.Account.SetLeverageAsync("ETH-SWAP-USDT", 10);

var order = await restClient.UsdtFuturesApi.Trading.PlaceOrderAsync(
    symbol: "ETH-SWAP-USDT",
    orderSide: FuturesOrderSide.BuyOpen,
    orderType: FuturesNewOrderType.Limit,
    quantity: 10,
    price: 2000m,
    timeInForce: TimeInForce.GoodTillCanceled);

if (!order.Success) { Console.WriteLine(order.Error); return; }
```

Use `BuyOpen`, `SellOpen`, `BuyClose`, and `SellClose` for futures direction and position intent.

## Core Pattern: WebSocket Subscriptions

Use `ToobitSocketClient`. Store the returned `UpdateSubscription` and unsubscribe when done.

```csharp
using Toobit.Net.Clients;
using Toobit.Net.Enums;

var socketClient = new ToobitSocketClient();

var subscription = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync(
    "BTCUSDT",
    update => Console.WriteLine($"BTCUSDT: {update.Data.LastPrice}"));

if (!subscription.Success) { Console.WriteLine(subscription.Error); return; }

await socketClient.UnsubscribeAsync(subscription.Data);
```

Authenticated user streams require a listen key from the REST account API. The listen key is shared between Spot and USDT futures APIs.

```csharp
var listenKeyResult = await restClient.SpotApi.Account.StartUserStreamAsync();
if (!listenKeyResult.Success) { Console.WriteLine(listenKeyResult.Error); return; }

var userSub = await socketClient.SpotApi.SubscribeToUserDataUpdatesAsync(
    listenKeyResult.Data,
    onAccountMessage: update => { },
    onOrderMessage: update => { },
    onUserTradeMessage: update => { });
```

## Multi-Exchange via CryptoExchange.Net.SharedApis

Use shared interfaces for exchange-agnostic code:

```csharp
using CryptoExchange.Net.SharedApis;
using Toobit.Net.Clients;

var shared = new ToobitRestClient().SpotApi.SharedClient;
var symbol = new SharedSymbol(TradingMode.Spot, "BTC", "USDT");

var ticker = await shared.GetSpotTickerAsync(new GetTickerRequest(symbol));
if (!ticker.Success) { Console.WriteLine(ticker.Error); return; }
```

Toobit exposes shared REST clients for Spot and USDT futures and shared socket clients for Spot and USDT futures. Call `shared.Discover()` to inspect supported shared interfaces, request options, and subscription options at runtime.

## Dependency Injection

```csharp
using Toobit.Net;

services.AddToobit(options =>
{
    options.Rest.ApiCredentials = new ToobitCredentials("API_KEY", "API_SECRET");
    options.Socket.ApiCredentials = new ToobitCredentials("API_KEY", "API_SECRET");
});

// Inject IToobitRestClient and IToobitSocketClient.
```

## Environments

```csharp
using Toobit.Net;

var live = new ToobitRestClient(options =>
{
    options.Environment = ToobitEnvironment.Live;
});

var custom = new ToobitRestClient(options =>
{
    options.Environment = ToobitEnvironment.CreateCustom(
        "custom",
        "https://example-rest",
        "wss://example-socket");
});
```

Only `ToobitEnvironment.Live` is built in. Use `CreateCustom` if you need non-default addresses.

## Common Pitfalls

- Do not write raw `HttpClient` calls to Toobit endpoints.
- Do not use `BinanceRestClient`, `OKXRestClient`, or generic exchange clients for Toobit code.
- Do not use generic `ApiCredentials` in examples. Use `ToobitCredentials`.
- Do not assume a `GeneralApi`, `CoinFuturesApi`, `UsdFuturesApi`, margin API, or options API exists.
- Do not use Binance futures symbol format (`ETHUSDT`) for Toobit USDT futures examples; use `ETH-SWAP-USDT`.
- Do not read `.Data` before checking `.Success`.
- Do not block async calls with `.Result` or `.Wait()`.
- Do not create clients per request. Reuse clients or use DI.
- Do not skip WebSocket teardown; call `UnsubscribeAsync(subscription.Data)`.
- Do not invent methods. Check `Toobit.Net/Interfaces/Clients/**` first.

## Reference

- Full client reference: https://cryptoexchange.jkorf.dev/Toobit.Net/
- AI API quick map: `docs/ai-api-map.md`
- LLM context: `llms.txt` and `llms-full.txt`
- Examples: `Examples/ai-friendly/`
- Source: https://github.com/JKorf/Toobit.Net
