using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Toobit.Net.Enums
{
    /// <summary>
    /// Filter type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SymbolFilterType>))]
    public enum SymbolFilterType
    {
        /// <summary>
        /// Price filter
        /// </summary>
        [Map("PRICE_FILTER")]
        PriceFilter,
        /// <summary>
        /// Lot size filter
        /// </summary>
        [Map("LOT_SIZE")]
        LotSize,
        /// <summary>
        /// Min notional filter
        /// </summary>
        [Map("MIN_NOTIONAL")]
        MinNotional,
        /// <summary>
        /// Trade amount filter
        /// </summary>
        [Map("TRADE_AMOUNT")]
        TradeAmount,
        /// <summary>
        /// Limit trading filter
        /// </summary>
        [Map("LIMIT_TRADING")]
        LimitTrading,
        /// <summary>
        /// Market trading filter
        /// </summary>
        [Map("MARKET_TRADING")]
        MarketTrading,
        /// <summary>
        /// Open quote filter
        /// </summary>
        [Map("OPEN_QUOTE")]
        OpenQuote
    }
}
