// 03-websocket.cs
//
// Demonstrates: ToobitSocketClient subscriptions, success checks, authenticated
// listen-key user streams, and unsubscribe on shutdown.
//
// Setup: dotnet add package Toobit.Net

using Toobit.Net;
using Toobit.Net.Clients;
using Toobit.Net.Enums;

var socketClient = new ToobitSocketClient();

var tickerSub = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync(
    "BTCUSDT",
    update => Console.WriteLine($"BTCUSDT: {update.Data.LastPrice}"));

if (!tickerSub.Success)
{
    Console.WriteLine($"Ticker subscription failed: {tickerSub.Error}");
    return;
}

var klineSub = await socketClient.SpotApi.SubscribeToKlineUpdatesAsync(
    "BTCUSDT",
    KlineInterval.OneMinute,
    update => Console.WriteLine($"Kline close: {update.Data.ClosePrice}"));

if (!klineSub.Success)
{
    Console.WriteLine($"Kline subscription failed: {klineSub.Error}");
    await socketClient.UnsubscribeAsync(tickerSub.Data);
    return;
}

var restClient = new ToobitRestClient(options =>
{
    options.ApiCredentials = new ToobitCredentials("API_KEY", "API_SECRET");
});

var listenKeyResult = await restClient.SpotApi.Account.StartUserStreamAsync();
if (listenKeyResult.Success)
{
    var userSub = await socketClient.SpotApi.SubscribeToUserDataUpdatesAsync(
        listenKeyResult.Data,
        onAccountMessage: update => Console.WriteLine("Account update"),
        onOrderMessage: update => Console.WriteLine($"Order updates: {update.Data.Length}"),
        onUserTradeMessage: update => Console.WriteLine($"Trade updates: {update.Data.Length}"));

    if (userSub.Success)
        await socketClient.UnsubscribeAsync(userSub.Data);

    await restClient.SpotApi.Account.StopUserStreamAsync(listenKeyResult.Data);
}

await socketClient.UnsubscribeAsync(klineSub.Data);
await socketClient.UnsubscribeAsync(tickerSub.Data);
