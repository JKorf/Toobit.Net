# Toobit.Net AI API Quick Map

Use this file to route common user intents to the correct Toobit.Net client member. If a method name or parameter is not listed here, inspect `Toobit.Net/Interfaces/Clients/**` before generating code.

## Client Roots

| Intent | Use |
|---|---|
| REST calls | `new ToobitRestClient()` |
| WebSocket streams | `new ToobitSocketClient()` |
| API key authentication | `options.ApiCredentials = new ToobitCredentials("key", "secret")` |
| Live environment | `ToobitEnvironment.Live` |
| Custom environment | `ToobitEnvironment.CreateCustom(name, restAddress, socketAddress)` |
| Dependency injection | `services.AddToobit(options => { ... })` |

## Spot REST

| User intent | Toobit.Net member |
|---|---|
| Get spot server time | `client.SpotApi.ExchangeData.GetServerTimeAsync()` |
| Get spot exchange info | `client.SpotApi.ExchangeData.GetExchangeInfoAsync()` |
| Get spot ticker for one symbol | `client.SpotApi.ExchangeData.GetTickersAsync("BTCUSDT")` |
| Get all spot tickers | `client.SpotApi.ExchangeData.GetTickersAsync()` |
| Get spot price for one symbol | `client.SpotApi.ExchangeData.GetPricesAsync("BTCUSDT")` |
| Get all spot prices | `client.SpotApi.ExchangeData.GetPricesAsync()` |
| Get spot order book | `client.SpotApi.ExchangeData.GetOrderBookAsync("BTCUSDT")` |
| Get recent spot trades | `client.SpotApi.ExchangeData.GetRecentTradesAsync("BTCUSDT")` |
| Get spot klines/candles | `client.SpotApi.ExchangeData.GetKlinesAsync("BTCUSDT", KlineInterval.OneMinute)` |
| Get spot book ticker | `client.SpotApi.ExchangeData.GetBookTickersAsync("BTCUSDT")` |
| Get spot balances | `client.SpotApi.Account.GetBalancesAsync()` |
| Get deposit address | `client.SpotApi.Account.GetDepositAddressAsync(asset, network)` |
| Get deposits | `client.SpotApi.Account.GetDepositsAsync(...)` |
| Get withdrawals | `client.SpotApi.Account.GetWithdrawalsAsync(...)` |
| Withdraw asset | `client.SpotApi.Account.WithdrawAsync(asset, address, quantity, ...)` |
| Transfer assets | `client.SpotApi.Account.TransferAsync(fromUid, toUid, fromAccountType, toAccountType, asset, quantity)` |
| Get spot transaction history | `client.SpotApi.Account.GetTransactionHistoryAsync(...)` |
| Get sub accounts | `client.SpotApi.Account.GetSubAccountsAsync()` |
| Start spot/user listen key | `client.SpotApi.Account.StartUserStreamAsync()` |
| Keep alive listen key | `client.SpotApi.Account.KeepAliveUserStreamAsync(listenKey)` |
| Stop listen key | `client.SpotApi.Account.StopUserStreamAsync(listenKey)` |
| Place spot order | `client.SpotApi.Trading.PlaceOrderAsync(...)` |
| Place spot test order | `client.SpotApi.Trading.PlaceTestOrderAsync(...)` |
| Place multiple spot orders | `client.SpotApi.Trading.PlaceMultipleOrdersAsync(orders)` |
| Query spot order | `client.SpotApi.Trading.GetOrderAsync(orderId: orderId)` |
| Query spot order by client id | `client.SpotApi.Trading.GetOrderAsync(clientOrderId: clientOrderId)` |
| Get open spot orders | `client.SpotApi.Trading.GetOpenOrdersAsync(symbol: "BTCUSDT")` |
| Get historical spot orders | `client.SpotApi.Trading.GetOrdersAsync(symbol: "BTCUSDT")` |
| Cancel spot order | `client.SpotApi.Trading.CancelOrderAsync(orderId: orderId)` |
| Cancel spot order by client id | `client.SpotApi.Trading.CancelOrderAsync(clientOrderId: clientOrderId)` |
| Cancel all spot orders | `client.SpotApi.Trading.CancelAllOrdersAsync(symbol: "BTCUSDT")` |
| Cancel multiple spot orders | `client.SpotApi.Trading.CancelMultipleOrdersAsync(orderIds)` |
| Get spot user trades | `client.SpotApi.Trading.GetUserTradesAsync("BTCUSDT")` |

## USDT Futures REST

| User intent | Toobit.Net member |
|---|---|
| Get futures server time | `client.UsdtFuturesApi.ExchangeData.GetServerTimeAsync()` |
| Get futures exchange info | `client.UsdtFuturesApi.ExchangeData.GetExchangeInfoAsync()` |
| Get futures ticker | `client.UsdtFuturesApi.ExchangeData.GetTickersAsync("ETH-SWAP-USDT")` |
| Get all futures tickers | `client.UsdtFuturesApi.ExchangeData.GetTickersAsync()` |
| Get futures price | `client.UsdtFuturesApi.ExchangeData.GetPricesAsync("ETH-SWAP-USDT")` |
| Get futures order book | `client.UsdtFuturesApi.ExchangeData.GetOrderBookAsync("ETH-SWAP-USDT")` |
| Get recent futures trades | `client.UsdtFuturesApi.ExchangeData.GetRecentTradesAsync("ETH-SWAP-USDT")` |
| Get futures klines/candles | `client.UsdtFuturesApi.ExchangeData.GetKlinesAsync("ETH-SWAP-USDT", KlineInterval.OneMinute)` |
| Get mark price klines | `client.UsdtFuturesApi.ExchangeData.GetMarkPriceKlinesAsync("ETH-SWAP-USDT", KlineInterval.OneMinute)` |
| Get index price klines | `client.UsdtFuturesApi.ExchangeData.GetIndexPriceKlinesAsync("ETHUSDT", KlineInterval.OneMinute)` |
| Get futures book ticker | `client.UsdtFuturesApi.ExchangeData.GetBookTickersAsync("ETH-SWAP-USDT")` |
| Get index prices | `client.UsdtFuturesApi.ExchangeData.GetIndexPricesAsync("ETHUSDT")` |
| Get mark price | `client.UsdtFuturesApi.ExchangeData.GetMarkPriceAsync("ETH-SWAP-USDT")` |
| Get funding rates | `client.UsdtFuturesApi.ExchangeData.GetFundingRateAsync("ETH-SWAP-USDT")` |
| Get funding rate history | `client.UsdtFuturesApi.ExchangeData.GetFundingRateHistoryAsync("ETH-SWAP-USDT")` |
| Set margin type | `client.UsdtFuturesApi.Account.SetMarginTypeAsync(symbol, MarginType.Isolated)` |
| Set leverage | `client.UsdtFuturesApi.Account.SetLeverageAsync(symbol, leverage)` |
| Get leverage info | `client.UsdtFuturesApi.Account.GetLeverageInfoAsync(symbol)` |
| Get futures balances | `client.UsdtFuturesApi.Account.GetBalancesAsync()` |
| Set isolated position margin | `client.UsdtFuturesApi.Account.SetPositionMarginAsync(symbol, positionSide, quantity)` |
| Get futures transaction history | `client.UsdtFuturesApi.Account.GetTransactionHistoryAsync(...)` |
| Get futures fee rates | `client.UsdtFuturesApi.Account.GetFeesAsync(symbol)` |
| Start futures/user listen key | `client.UsdtFuturesApi.Account.StartUserStreamAsync()` |
| Keep alive listen key | `client.UsdtFuturesApi.Account.KeepAliveUserStreamAsync(listenKey)` |
| Stop listen key | `client.UsdtFuturesApi.Account.StopUserStreamAsync(listenKey)` |
| Place futures order | `client.UsdtFuturesApi.Trading.PlaceOrderAsync(...)` |
| Place futures order with TP/SL | `client.UsdtFuturesApi.Trading.PlaceOrderAsync(..., takeProfit: ..., stopLoss: ...)` |
| Place multiple futures orders | `client.UsdtFuturesApi.Trading.PlaceMultipleOrdersAsync(orders)` |
| Query futures order | `client.UsdtFuturesApi.Trading.GetOrderAsync(orderId: orderId)` |
| Query futures order by client id | `client.UsdtFuturesApi.Trading.GetOrderAsync(clientOrderId: clientOrderId)` |
| Cancel futures order | `client.UsdtFuturesApi.Trading.CancelOrderAsync(orderId: orderId)` |
| Cancel all futures orders | `client.UsdtFuturesApi.Trading.CancelAllOrdersAsync(symbol, side)` |
| Cancel multiple futures orders | `client.UsdtFuturesApi.Trading.CancelMultipleOrdersAsync(orderIds)` |
| Get open futures orders | `client.UsdtFuturesApi.Trading.GetOpenOrdersAsync(symbol: "ETH-SWAP-USDT")` |
| Get futures positions | `client.UsdtFuturesApi.Trading.GetPositionsAsync(symbol: "ETH-SWAP-USDT")` |
| Set futures trading stop | `client.UsdtFuturesApi.Trading.SetTradingStopAsync(symbol, positionSide, takeProfitPrice, stopLossPrice)` |
| Get futures order history | `client.UsdtFuturesApi.Trading.GetOrderHistoryAsync(symbol: "ETH-SWAP-USDT")` |
| Get futures user trades | `client.UsdtFuturesApi.Trading.GetUserTradesAsync("ETH-SWAP-USDT")` |

## Spot WebSocket

| User intent | Toobit.Net member |
|---|---|
| Subscribe spot trades | `socketClient.SpotApi.SubscribeToTradeUpdatesAsync(symbol, handler)` |
| Subscribe many spot trade streams | `socketClient.SpotApi.SubscribeToTradeUpdatesAsync(symbols, handler)` |
| Subscribe spot ticker | `socketClient.SpotApi.SubscribeToTickerUpdatesAsync(symbol, handler)` |
| Subscribe many spot ticker streams | `socketClient.SpotApi.SubscribeToTickerUpdatesAsync(symbols, handler)` |
| Subscribe spot klines | `socketClient.SpotApi.SubscribeToKlineUpdatesAsync(symbol, interval, handler)` |
| Subscribe many spot klines | `socketClient.SpotApi.SubscribeToKlineUpdatesAsync(symbols, interval, handler)` |
| Subscribe spot partial order book | `socketClient.SpotApi.SubscribeToPartialOrderBookUpdatesAsync(symbol, handler)` |
| Subscribe spot diff order book | `socketClient.SpotApi.SubscribeToOrderBookUpdatesAsync(symbol, handler)` |
| Subscribe spot user data | `socketClient.SpotApi.SubscribeToUserDataUpdatesAsync(listenKey, ...)` |

## USDT Futures WebSocket

| User intent | Toobit.Net member |
|---|---|
| Subscribe futures trades | `socketClient.UsdtFuturesApi.SubscribeToTradeUpdatesAsync(symbol, handler)` |
| Subscribe many futures trade streams | `socketClient.UsdtFuturesApi.SubscribeToTradeUpdatesAsync(symbols, handler)` |
| Subscribe futures ticker | `socketClient.UsdtFuturesApi.SubscribeToTickerUpdatesAsync(symbol, handler)` |
| Subscribe many futures ticker streams | `socketClient.UsdtFuturesApi.SubscribeToTickerUpdatesAsync(symbols, handler)` |
| Subscribe futures klines | `socketClient.UsdtFuturesApi.SubscribeToKlineUpdatesAsync(symbol, interval, handler)` |
| Subscribe many futures klines | `socketClient.UsdtFuturesApi.SubscribeToKlineUpdatesAsync(symbols, interval, handler)` |
| Subscribe futures partial order book | `socketClient.UsdtFuturesApi.SubscribeToPartialOrderBookUpdatesAsync(symbol, handler)` |
| Subscribe futures diff order book | `socketClient.UsdtFuturesApi.SubscribeToOrderBookUpdatesAsync(symbol, handler)` |
| Subscribe futures index price | `socketClient.UsdtFuturesApi.SubscribeToIndexPriceUpdatesAsync(symbol, handler)` |
| Subscribe futures user data | `socketClient.UsdtFuturesApi.SubscribeToUserDataUpdatesAsync(listenKey, ...)` |

## SharedApis

Use SharedApis for exchange-agnostic code across Toobit and other CryptoExchange.Net libraries.

| User intent | Toobit.Net member or interface |
|---|---|
| Shared spot REST client | `new ToobitRestClient().SpotApi.SharedClient` |
| Shared USDT futures REST client | `new ToobitRestClient().UsdtFuturesApi.SharedClient` |
| Shared spot socket client | `new ToobitSocketClient().SpotApi.SharedClient` |
| Shared USDT futures socket client | `new ToobitSocketClient().UsdtFuturesApi.SharedClient` |
| Shared spot ticker REST | `ISpotTickerRestClient.GetSpotTickerAsync(new GetTickerRequest(symbol))` |
| Shared spot order REST | `ISpotOrderRestClient.PlaceSpotOrderAsync(...)` |
| Shared futures order REST | `IFuturesOrderRestClient.PlaceFuturesOrderAsync(...)` |
| Shared balance REST | `IBalanceRestClient.GetBalancesAsync(...)` |
| Shared ticker socket | `ITickerSocketClient.SubscribeToTickerUpdatesAsync(...)` |
| Shared order book socket | `IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(...)` |

For shared socket subscriptions, keep the concrete socket client and unsubscribe with `await socketClient.UnsubscribeAsync(subscription.Data)`.

## Result Handling

| Situation | Pattern |
|---|---|
| REST success check | `if (!result.Success) { Console.WriteLine(result.Error); return; }` |
| Socket subscription success check | `if (!sub.Success) { Console.WriteLine(sub.Error); return; }` |
| Read REST data | Read `result.Data` only after `result.Success` |
| Retry decision | Retry only when `result.Error?.IsTransient == true` |
| Cancellation | Pass `ct: cancellationToken` |

## Common Routing Pitfalls

| Do not use | Use instead |
|---|---|
| `ToobitClient` | `ToobitRestClient` |
| `ApiCredentials` | `ToobitCredentials` |
| `UsdFuturesApi` | `UsdtFuturesApi` |
| `CoinFuturesApi` | No COIN futures API exists in Toobit.Net |
| `GeneralApi` | Spot account methods for deposits, withdrawals, transfers, sub accounts |
| `SpotApi.ExchangeData.GetTickerAsync(...)` | `SpotApi.ExchangeData.GetTickersAsync(...)` |
| `SpotApi.ExchangeData.GetPriceAsync(...)` | `SpotApi.ExchangeData.GetPricesAsync(...)` |
| Binance futures symbol `ETHUSDT` | Toobit futures symbol `ETH-SWAP-USDT` |
| `.Data` without `.Success` check | Check `.Success` first |
| `ITickerSocketClient.UnsubscribeAsync(...)` | Keep the concrete socket client and call `socketClient.UnsubscribeAsync(subscription.Data)` |
| Testnet environment | `ToobitEnvironment.Live` or `ToobitEnvironment.CreateCustom(...)` |
