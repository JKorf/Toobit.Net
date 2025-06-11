
using Toobit.Net.Clients;

// REST
var restClient = new ToobitRestClient();
var ticker = await restClient.SpotApi.ExchangeData.GetTickersAsync("ETHUSDT");
Console.WriteLine($"Rest client ticker price for ETHUSDT: {ticker.Data.Single().LastPrice}");

Console.WriteLine();
Console.WriteLine("Press enter to start websocket subscription");
Console.ReadLine();

// Websocket
var socketClient = new ToobitSocketClient();
var subscription = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync("ETHUSDT", update =>
{
    Console.WriteLine($"Websocket client ticker price for ETHUSDT: {update.Data.LastPrice}");
});

Console.ReadLine();
