# Copilot Instructions for Toobit.Net

This repository is **Toobit.Net**, a strongly typed C#/.NET client library for the Toobit cryptocurrency exchange API. It is part of the CryptoExchange.Net ecosystem.

When generating code that consumes Toobit.Net, follow these conventions.

## Use Toobit.Net, Not Raw HTTP

Never generate raw `HttpClient` calls to Toobit API endpoints. Always use `ToobitRestClient` or `ToobitSocketClient` so signing, request formatting, result handling, and WebSocket management stay correct.

## Client Setup

```csharp
using Toobit.Net;
using Toobit.Net.Clients;

var restClient = new ToobitRestClient(options =>
{
    options.ApiCredentials = new ToobitCredentials("API_KEY", "API_SECRET");
});
```

For public market data, use `new ToobitRestClient()` without credentials.

## Result Handling

REST methods return `WebCallResult<T>` and WebSocket subscriptions return `CallResult<UpdateSubscription>`. Always check `.Success` before reading `.Data`.

## API Structure

- `restClient.SpotApi.ExchangeData`: public spot market data
- `restClient.SpotApi.Account`: spot balances, deposits, withdrawals, transfers, listen keys
- `restClient.SpotApi.Trading`: spot orders and trades
- `restClient.UsdtFuturesApi.ExchangeData`: USDT futures market data, mark/index prices, funding
- `restClient.UsdtFuturesApi.Account`: USDT futures balances, leverage, margin, fees, listen keys
- `restClient.UsdtFuturesApi.Trading`: USDT futures orders, positions, trading stops, user trades
- `socketClient.SpotApi`: spot WebSocket subscriptions
- `socketClient.UsdtFuturesApi`: USDT futures WebSocket subscriptions

## Order Placement

Spot:

```csharp
var order = await restClient.SpotApi.Trading.PlaceOrderAsync(
    "BTCUSDT",
    OrderSide.Buy,
    OrderType.Limit,
    quantity: 0.001m,
    timeInForce: TimeInForce.GoodTillCanceled,
    price: 50000m);
```

USDT futures symbols use the Toobit contract format, for example `ETH-SWAP-USDT`:

```csharp
var order = await restClient.UsdtFuturesApi.Trading.PlaceOrderAsync(
    "ETH-SWAP-USDT",
    FuturesOrderSide.BuyOpen,
    FuturesNewOrderType.Limit,
    quantity: 10,
    price: 2000m,
    timeInForce: TimeInForce.GoodTillCanceled);
```

## WebSocket Pattern

Store the returned `UpdateSubscription` and unsubscribe on shutdown:

```csharp
var socketClient = new ToobitSocketClient();
var sub = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync(
    "BTCUSDT",
    update => { Console.WriteLine(update.Data.LastPrice); });

if (!sub.Success) { Console.WriteLine(sub.Error); return; }

await socketClient.UnsubscribeAsync(sub.Data);
```

Authenticated user streams require a listen key from `StartUserStreamAsync`.

## Cross-Exchange

For exchange-agnostic code, use `CryptoExchange.Net.SharedApis` from `.SharedClient`:

```csharp
using CryptoExchange.Net.SharedApis;

var shared = new ToobitRestClient().SpotApi.SharedClient;
var ticker = await shared.GetSpotTickerAsync(
    new GetTickerRequest(new SharedSymbol(TradingMode.Spot, "BTC", "USDT")));
```

Shared Spot and futures symbol clients expose `SpotSymbolCatalog` / `FuturesSymbolCatalog` after the corresponding symbol call. `GetSymbolsRequest` supports base/quote asset type and subtype filters; results include inactive entries through `Active`, display names, and asset classification metadata.

## Avoid

- Raw `HttpClient` Toobit calls
- Generic `ApiCredentials` in examples; use `ToobitCredentials`
- Binance futures symbols for Toobit futures; use `ETH-SWAP-USDT` style
- Nonexistent APIs such as `GeneralApi`, `CoinFuturesApi`, `UsdFuturesApi`, margin, or options
- `.Result` or `.Wait()`
- Reading `.Data` without checking `.Success`
- Creating clients per request

## Reference

For detailed patterns and pitfalls see `AGENTS.md`, `llms.txt`, `llms-full.txt`, and `docs/ai-api-map.md` in the repository root. Compilable examples are in `Examples/ai-friendly/`.
