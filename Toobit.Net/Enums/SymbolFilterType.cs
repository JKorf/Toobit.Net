using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Toobit.Net.Enums
{
    /// <summary>
    /// Filter type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SymbolFilterType>))]
    public enum SymbolFilterType
    {
        /// <summary>
        /// ["<c>PRICE_FILTER</c>"] Price filter
        /// </summary>
        [Map("PRICE_FILTER")]
        PriceFilter,
        /// <summary>
        /// ["<c>LOT_SIZE</c>"] Lot size filter
        /// </summary>
        [Map("LOT_SIZE")]
        LotSize,
        /// <summary>
        /// ["<c>MIN_NOTIONAL</c>"] Min notional filter
        /// </summary>
        [Map("MIN_NOTIONAL")]
        MinNotional,
        /// <summary>
        /// ["<c>TRADE_AMOUNT</c>"] Trade amount filter
        /// </summary>
        [Map("TRADE_AMOUNT")]
        TradeAmount,
        /// <summary>
        /// ["<c>LIMIT_TRADING</c>"] Limit trading filter
        /// </summary>
        [Map("LIMIT_TRADING")]
        LimitTrading,
        /// <summary>
        /// ["<c>MARKET_TRADING</c>"] Market trading filter
        /// </summary>
        [Map("MARKET_TRADING")]
        MarketTrading,
        /// <summary>
        /// ["<c>OPEN_QUOTE</c>"] Open quote filter
        /// </summary>
        [Map("OPEN_QUOTE")]
        OpenQuote
    }
}
