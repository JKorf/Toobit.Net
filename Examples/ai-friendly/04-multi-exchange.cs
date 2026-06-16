// 04-multi-exchange.cs
//
// Demonstrates: writing exchange-agnostic code using CryptoExchange.Net.SharedApis.
// The same pattern works against other CryptoExchange.Net exchange libraries.
//
// Setup: dotnet add package Toobit.Net

using CryptoExchange.Net.SharedApis;
using Toobit.Net.Clients;

ISpotTickerRestClient toobitShared = new ToobitRestClient().SpotApi.SharedClient;

var btcusdt = new SharedSymbol(TradingMode.Spot, "BTC", "USDT");

// Use Discover() when you need runtime capability and option metadata.
var capabilities = toobitShared.Discover();
Console.WriteLine($"Shared features: {capabilities.Features.Count(x => x.Supported)}");

await PrintTicker(toobitShared, btcusdt);

async Task PrintTicker(ISpotTickerRestClient client, SharedSymbol symbol)
{
    var result = await client.GetSpotTickerAsync(new GetTickerRequest(symbol));
    if (!result.Success)
    {
        Console.WriteLine($"[{client.Exchange}] Failed: {result.Error}");
        return;
    }

    Console.WriteLine($"[{client.Exchange}] {result.Data.Symbol}: {result.Data.LastPrice}");
}

var socketClient = new ToobitSocketClient();
ITickerSocketClient tickerSocket = socketClient.SpotApi.SharedClient;

// Shared socket subscriptions return WebSocketResult<UpdateSubscription>.
var sub = await tickerSocket.SubscribeToTickerUpdatesAsync(
    new SubscribeTickerRequest(btcusdt),
    update => Console.WriteLine($"[{tickerSocket.Exchange}] {update.Data.Symbol}: {update.Data.LastPrice}"));

if (!sub.Success)
{
    Console.WriteLine($"Subscribe failed: {sub.Error}");
    return;
}

await socketClient.UnsubscribeAsync(sub.Data);
