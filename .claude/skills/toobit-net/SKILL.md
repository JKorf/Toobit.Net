---
name: toobit-net
description: Generate and troubleshoot C#/.NET code consuming Toobit.Net for Spot and USDT Futures market data, accounts, orders, funds movement, WebSocket streams and Shared API V2 integrations.
---

# Toobit.Net

Install the consumer package:

```sh
dotnet add package Toobit.Net
```

Use `ToobitRestClient` with `SpotApi` or `UsdtFuturesApi`, each exposing `ExchangeData`, `Account` and `Trading`. `ToobitSocketClient` exposes subscriptions directly on those API roots. Reuse and dispose clients, await calls, and retain successful subscriptions for unsubscribe cleanup.

Credentials are `ToobitCredentials(key, secret)` without a passphrase. Public data needs no credentials. User-stream overloads without a listen key use credentials to acquire/renew one; explicit-key overloads require application-owned start/keepalive/stop handling.

Spot symbols use `BTCUSDT`; USDT Futures contracts use `ETH-SWAP-USDT`. Some futures index endpoints use `ETHUSDT`. Native Futures quantity is decimal contract volume; use contract metadata before conversion. `FuturesOrderSide` encodes open/close intent: `BuyOpen`, `SellOpen`, `SellClose`, `BuyClose`.

Check `Success` before `Data`: REST returns `HttpResult`, subscriptions return `WebSocketResult<UpdateSubscription>`, and some Shared helpers return `ExchangeCallResult`, with generic variants where applicable. Check each nested `CallResult` item after outer batch success.

Use `.SharedApi` and narrow V2 capabilities for portable operations. `IToobitSharedApiClient` groups `SpotRest`, `FuturesRest`, `SpotSocket`, `FuturesSocket`. Use native methods for listen keys, margin/leverage, trading stops and withdrawal details.

## Read when needed

- [API surfaces](references/api-surfaces.md): endpoints, capability support, catalogs, quantities and prerequisites.
- [Usage](references/usage.md): setup, authentication, account reads, orders, subscriptions, Shared API V2 and DI.
- [Operational considerations](references/safety.md): execution, margin, partial batches, funds movement and uncertain submissions.

For work on the library itself, use [repository instructions](../../../AGENTS.md) (checkout-only).
