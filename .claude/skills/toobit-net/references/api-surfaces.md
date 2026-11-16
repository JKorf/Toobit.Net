# Toobit.Net API surfaces

## Client roots

`ToobitRestClient` / `IToobitRestClient` expose `SpotApi` and `UsdtFuturesApi`, each with `ExchangeData`, `Account` and `Trading`. `ToobitSocketClient` / `IToobitSocketClient` exposes subscriptions directly on `SpotApi` and `UsdtFuturesApi`.

## Spot REST Endpoint Groups

`client.SpotApi.ExchangeData`:

- `GetServerTimeAsync()`
- `GetExchangeInfoAsync()`
- `GetOrderBookAsync(symbol, limit)`
- `GetRecentTradesAsync(symbol, limit)`
- `GetKlinesAsync(symbol, interval, startTime, endTime, limit)`
- `GetTickersAsync(symbol)`
- `GetPricesAsync(symbol)`
- `GetBookTickersAsync(symbol)`

`client.SpotApi.Account`:

- `GetBalancesAsync()`
- `WithdrawAsync(asset, address, quantity, network, tag, vaspCode, targetPersonFirstName, targetPersonLastName, clientOrderId)`
- `GetWithdrawalsAsync(asset, fromId, withdrawOrderId, startTime, endTime, limit)`
- `GetDepositAddressAsync(asset, network)`
- `GetDepositsAsync(asset, fromId, startTime, endTime, limit)`
- `TransferAsync(fromUid, toUid, fromAccountType, toAccountType, asset, quantity)`
- `GetTransactionHistoryAsync(accountType, asset, flowType, fromId, startTime, endTime, limit)`
- `GetSubAccountsAsync()`
- `StartUserStreamAsync()`
- `KeepAliveUserStreamAsync(listenKey)`
- `StopUserStreamAsync(listenKey)`

`client.SpotApi.Trading`:

- `PlaceTestOrderAsync(symbol, orderSide, orderType, quantity, timeInForce, price, clientOrderId)`
- `PlaceOrderAsync(symbol, orderSide, orderType, quantity, timeInForce, price, clientOrderId)`
- `PlaceMultipleOrdersAsync(orders)`
- `CancelOrderAsync(orderId, clientOrderId)`
- `CancelAllOrdersAsync(symbol, side)`
- `CancelMultipleOrdersAsync(orderIds)`
- `GetOrderAsync(orderId, clientOrderId)`
- `GetOpenOrdersAsync(orderId, symbol, limit)`
- `GetOrdersAsync(orderId, symbol, startTime, endTime, limit)`
- `GetUserTradesAsync(symbol, fromId, toId, startTime, endTime, limit)`

## USDT Futures REST Endpoint Groups

`client.UsdtFuturesApi.ExchangeData`:

- `GetServerTimeAsync()`
- `GetExchangeInfoAsync()`
- `GetOrderBookAsync(symbol, limit)`
- `GetRecentTradesAsync(symbol, limit)`
- `GetKlinesAsync(symbol, interval, startTime, endTime, limit)`
- `GetMarkPriceKlinesAsync(symbol, interval, startTime, endTime, limit)`
- `GetIndexPriceKlinesAsync(symbol, interval, startTime, endTime, limit)`
- `GetTickersAsync(symbol, interval)`
- `GetPricesAsync(symbol)`
- `GetBookTickersAsync(symbol)`
- `GetIndexPricesAsync(symbol)`
- `GetMarkPriceAsync(symbol)`
- `GetFundingRateAsync(symbol)`
- `GetFundingRateHistoryAsync(symbol, fromId, endId, limit)`

`client.UsdtFuturesApi.Account`:

- `SetMarginTypeAsync(symbol, marginType)`
- `SetLeverageAsync(symbol, leverage)`
- `GetLeverageInfoAsync(symbol)`
- `GetBalancesAsync()`
- `SetPositionMarginAsync(symbol, positionSide, quantity)`
- `GetTransactionHistoryAsync(asset, flowType, fromId, endId, startTime, endTime, limit)`
- `GetFeesAsync(symbol)`
- `StartUserStreamAsync()`
- `KeepAliveUserStreamAsync(listenKey)`
- `StopUserStreamAsync(listenKey)`

`client.UsdtFuturesApi.Trading`:

- `PlaceOrderAsync(symbol, orderSide, orderType, quantity, price, priceType, stopPrice, timeInForce, clientOrderId, takeProfit, takeProfitTriggerType, takeProfitLimitPrice, takeProfitOrderType, stopLoss, stopLossTriggerType, stopLossLimitPrice, stopLossOrderType)`
- `PlaceMultipleOrdersAsync(orders)`
- `GetOrderAsync(orderId, clientOrderId, orderType)`
- `CancelOrderAsync(orderId, clientOrderId, orderType)`
- `CancelAllOrdersAsync(symbol, side)`
- `CancelMultipleOrdersAsync(orderIds)`
- `GetOpenOrdersAsync(symbol, orderId, orderType, limit)`
- `GetPositionsAsync(symbol, positionSide)`
- `SetTradingStopAsync(symbol, positionSide, takeProfitPrice, stopLossPrice, takeProfitTriggerType, StopLossTriggerType)`
- `GetOrderHistoryAsync(symbol, toId, orderType, startTime, endTime, limit)`
- `GetUserTradesAsync(symbol, fromId, toId, startTime, endTime, limit)`

## Socket Streams

`socket.SpotApi` public streams:

- `SubscribeToTradeUpdatesAsync(symbols, handler)`
- `SubscribeToTickerUpdatesAsync(symbols, handler)`
- `SubscribeToKlineUpdatesAsync(symbols, interval, handler)`
- `SubscribeToPartialOrderBookUpdatesAsync(symbols, handler)`
- `SubscribeToOrderBookUpdatesAsync(symbols, handler)`

`socket.SpotApi` private stream:

- `SubscribeToUserDataUpdatesAsync(onAccountMessage, onOrderMessage, onUserTradeMessage)`
- `SubscribeToUserDataUpdatesAsync(listenKey, onAccountMessage, onOrderMessage, onUserTradeMessage)`

`socket.UsdtFuturesApi` public streams:

- `SubscribeToTradeUpdatesAsync(symbols, handler)`
- `SubscribeToKlineUpdatesAsync(symbols, interval, handler)`
- `SubscribeToPartialOrderBookUpdatesAsync(symbols, handler)`
- `SubscribeToOrderBookUpdatesAsync(symbols, handler)`
- `SubscribeToTickerUpdatesAsync(symbols, handler)`
- `SubscribeToIndexPriceUpdatesAsync(symbols, handler)`

`socket.UsdtFuturesApi` private stream:

- `SubscribeToUserDataUpdatesAsync(onAccountMessage, onOrderMessage, onPositionMessage, onUserTradeMessage)`
- `SubscribeToUserDataUpdatesAsync(listenKey, onAccountMessage, onOrderMessage, onPositionMessage, onUserTradeMessage)`

User stream overloads without a listen key require API credentials and manage listen key acquisition/renewal. Listen-key overloads require `StartUserStreamAsync`, keepalive, and stop calls.

## Shared API V2

`IToobitSharedApiClient` exposes these properties. Corresponding native REST/socket surfaces expose `.SharedApi`.

- `SpotRest` (`IToobitRestClientSpotSharedApi`): `IGetKlinesRest`, `IGetSpotSymbolsRest`, `IGetTickerRest`, `IGetAllTickersRest`, `IGetBookTickerRest`, `IGetRecentTradesRest`, `IGetOrderBookRest`, `IGetBalancesRest`, `IPlaceSpotOrderRest`, `IGetSpotOrderRest`, `IGetOpenSpotOrdersRest`, `IGetClosedSpotOrdersRest`, `IGetSpotOrderTradesRest`, `IGetSpotUserTradeHistoryRest`, `ICancelSpotOrderRest`, `IGetSpotOrderByClientOrderIdRest`, `ICancelSpotOrderByClientOrderIdRest`, `IGetAssetRest`, `IGetAllAssetsRest`, `IGetDepositAddressesRest`, `IGetDepositHistoryRest`, `IGetWithdrawalHistoryRest`, `IWithdrawRest`, `ITransferRest`.
- `SpotSocket` (`IToobitSocketClientSpotSharedApi`): `ISubscribeTickerSocket`, `ISubscribeTradesSocket`, `ISubscribeKlinesSocket`, `ISubscribeOrderBookSocket`, `ISubscribeBalancesSocket`, `ISubscribeSpotOrdersSocket`, `ISubscribeUserTradesSocket`.
- `FuturesRest` (`IToobitRestClientUsdtFuturesSharedApi`): `IGetKlinesRest`, `IGetMarkPriceKlinesRest`, `IGetIndexPriceKlinesRest`, `IGetFuturesSymbolsRest`, `IGetTickerRest`, `IGetAllTickersRest`, `IGetBookTickerRest`, `IGetRecentTradesRest`, `IPlaceFuturesOrderRest`, `IGetFuturesOrderRest`, `IGetOpenFuturesOrdersRest`, `IGetClosedFuturesOrdersRest`, `IGetFuturesOrderTradesRest`, `IGetFuturesUserTradeHistoryRest`, `ICancelFuturesOrderRest`, `IGetPositionsRest`, `IGetFuturesOrderByClientOrderIdRest`, `ICancelFuturesOrderByClientOrderIdRest`, `IGetLeverageRest`, `ISetLeverageRest`, `IGetOrderBookRest`, `IGetFundingRateHistoryRest`, `IGetBalancesRest`, `IGetFeesRest`, `IPlaceFuturesTriggerOrderRest`, `IGetFuturesTriggerOrderRest`, `ICancelFuturesTriggerOrderRest`.
- `FuturesSocket` (`IToobitSocketClientUsdtFuturesSharedApi`): `ISubscribeTickerSocket`, `ISubscribeTradesSocket`, `ISubscribeBalancesSocket`, `ISubscribeKlinesSocket`, `ISubscribeOrderBookSocket`, `ISubscribeFuturesOrdersSocket`, `ISubscribePositionsSocket`, `ISubscribeUserTradesSocket`.

For known workflows use aggregate properties or inject narrow capabilities. For dynamic selection call `aggregate.GetCapability(SharedCapabilities.Tickers.GetTicker.Rest, tradingMode: TradingMode.Spot)` and check for null before using `Capability` and `Options`. Descriptors identify operations, not guarantees of support. `GetCapabilities` enumerates matches and `Discover` summarizes metadata. With several exchanges registered, inject `IEnumerable<TCapability>` and select by exchange, supported mode and options.

## Symbols, catalogs and quantities

Spot symbols use `BTCUSDT`; USDT Futures contracts use `ETH-SWAP-USDT`, while some index-price endpoints use `ETHUSDT`. Use metadata or `ToobitExchange.FormatSymbol` rather than assuming a single symbol format for every endpoint. Shared workflows use `SharedSymbol` with the relevant trading mode.

Call `IGetSpotSymbolsRest.GetSpotSymbolsAsync(new GetSymbolsRequest())` or `IGetFuturesSymbolsRest.GetFuturesSymbolsAsync(new GetSymbolsRequest())`, check success, then inspect `SpotSymbolCatalog` or `FuturesSymbolCatalog`. Requests honor asset type/subtype filters; returned symbols include `Trading`, display names and crypto/stablecoin/fiat/TradFi classifications where populated. Inactive entries can appear; do not treat discovery as permission to trade them.

Native Futures order quantity is decimal contract volume; Shared Futures sizing uses `SharedOrderQuantity.QuantityInContracts`. The adapter converts to integer quantity, so reject fractional input instead of relying on truncation. Use contract multiplier and lot constraints for conversion. Futures books use contract quantities; Spot books use base quantities. Native Spot market buys use quote-asset quantity in the `quantity` parameter; other Spot orders use their documented base sizing.

## Environments, factories and checkout-only sources

Only `ToobitEnvironment.Live` is built in. `CreateCustom(...)` configures explicit addresses, not a built-in testnet. `IToobitOrderBookFactory`, `IToobitTrackerFactory` and `IToobitUserClientProvider` supply local books, trackers and cached per-user clients. Own start/stop lifecycles and clear stale clients after credential changes.

- [Public interfaces](../../../../Toobit.Net/Interfaces/Clients/): exact signatures, symbols, filters and cancellation tokens (checkout-only).
- [Implementations](../../../../Toobit.Net/Clients/): Shared options, conversions and stream management (checkout-only).
- [Complete examples](../../../../Examples/ai-friendly/): independent consumer programs (checkout-only).
