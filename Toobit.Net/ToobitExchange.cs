using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting.Filters;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.RateLimiting;
using System;
using CryptoExchange.Net.SharedApis;
using System.Text.Json.Serialization;
using Toobit.Net.Converters;

namespace Toobit.Net
{
    /// <summary>
    /// Toobit exchange information and configuration
    /// </summary>
    public static class ToobitExchange
    {
        /// <summary>
        /// Platform metadata
        /// </summary>
        public static PlatformInfo Metadata { get; } = new PlatformInfo(
                "Toobit",
                "Toobit",
                "https://raw.githubusercontent.com/JKorf/Toobit.Net/master/Toobit.Net/Icon/icon.png",
                "https://www.toobit.com",
                ["https://toobit-docs.github.io/apidocs/spot/v1/en/"],
                PlatformType.CryptoCurrencyExchange,
                CentralizationType.Centralized
                );

        /// <summary>
        /// Exchange name
        /// </summary>
        public static string ExchangeName => "Toobit";

        /// <summary>
        /// Display name
        /// </summary>
        public static string DisplayName => "Toobit";

        /// <summary>
        /// Url to exchange image
        /// </summary>
        public static string ImageUrl { get; } = "https://raw.githubusercontent.com/JKorf/Toobit.Net/master/Toobit.Net/Icon/icon.png";

        /// <summary>
        /// Url to the main website
        /// </summary>
        public static string Url { get; } = "https://www.toobit.com";

        /// <summary>
        /// Urls to the API documentation
        /// </summary>
        public static string[] ApiDocsUrl { get; } = new[] {
            "https://toobit-docs.github.io/apidocs/spot/v1/en/"
            };

        /// <summary>
        /// Type of exchange
        /// </summary>
        public static ExchangeType Type { get; } = ExchangeType.CEX;

        internal static JsonSerializerContext _serializerContext = new ToobitSourceGenerationContext();

        /// <summary>
        /// Aliases for Toobit assets
        /// </summary>
        public static AssetAliasConfiguration AssetAliases { get; } = new AssetAliasConfiguration
        {
            Aliases =
            [
                new AssetAlias("USDT", SharedSymbol.UsdOrStable.ToUpperInvariant(), AliasType.OnlyToExchange)
            ]
        };

        /// <summary>
        /// Format a base and quote asset to an Toobit recognized symbol 
        /// </summary>
        /// <param name="baseAsset">Base asset</param>
        /// <param name="quoteAsset">Quote asset</param>
        /// <param name="tradingMode">Trading mode</param>
        /// <param name="deliverTime">Delivery time for delivery futures</param>
        /// <returns></returns>
        public static string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
        {
            baseAsset = AssetAliases.CommonToExchangeName(baseAsset.ToUpperInvariant());
            quoteAsset = AssetAliases.CommonToExchangeName(quoteAsset.ToUpperInvariant());

            if (tradingMode == TradingMode.Spot)
                return baseAsset + quoteAsset;

            return $"{baseAsset}-SWAP-{quoteAsset}";
        }

        /// <summary>
        /// Rate limiter configuration for the Toobit API
        /// </summary>
        public static ToobitRateLimiters RateLimiter { get; } = new ToobitRateLimiters();
    }

    /// <summary>
    /// Rate limiter configuration for the Toobit API
    /// </summary>
    public class ToobitRateLimiters
    {
        /// <summary>
        /// Event for when a rate limit is triggered
        /// </summary>
        public event Action<RateLimitEvent> RateLimitTriggered;
        /// <summary>
        /// Event when the rate limit is updated. Note that it's only updated when a request is send, so there are no specific updates when the current usage is decaying.
        /// </summary>
        public event Action<RateLimitUpdateEvent> RateLimitUpdated;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        internal ToobitRateLimiters()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Initialize();
        }

        private void Initialize()
        {
            Toobit = new RateLimitGate("Toobit")
                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, [], 3000, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed)); // IP limit of 3000 request weight per minute
                
            Toobit.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            Toobit.RateLimitUpdated += (x) => RateLimitUpdated?.Invoke(x);

            ToobitSocket = new RateLimitGate("Socket")
                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerConnection, new IGuardFilter[] { new HostFilter("wss://stream.toobit.com"), new LimitItemTypeFilter(RateLimitItemType.Request) }, 4, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding)); // 5 requests per second per path (connection)
            ToobitSocket.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            ToobitSocket.RateLimitUpdated += (x) => RateLimitUpdated?.Invoke(x);
        }


        internal IRateLimitGate Toobit { get; private set; }

        internal IRateLimitGate ToobitSocket { get; private set; }
    }
}
