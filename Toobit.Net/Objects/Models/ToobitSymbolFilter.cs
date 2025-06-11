using System.Text.Json.Serialization;
using Toobit.Net.Converters;
using Toobit.Net.Enums;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// A filter for order placed on a symbol.
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<ToobitSymbolFilter>))]
    public record ToobitSymbolFilter
    {
        /// <summary>
        /// The type of this filter
        /// </summary>
        [JsonPropertyName("filterType")]
        public SymbolFilterType FilterType { get; set; }
    }

    /// <summary>
    /// Price filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<ToobitSymbolPriceFilter>))]
    public record ToobitSymbolPriceFilter : ToobitSymbolFilter
    {
        /// <summary>
        /// The minimal price the order can be for
        /// </summary>
        public decimal MinPrice { get; set; }
        /// <summary>
        /// The max price the order can be for
        /// </summary>
        public decimal MaxPrice { get; set; }
        /// <summary>
        /// The tick size of the price. The price can not have more precision as this and can only be incremented in steps of this.
        /// </summary>
        public decimal TickSize { get; set; }
    }

    /// <summary>
    /// Lot size filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<ToobitSymbolLotSizeFilter>))]
    public record ToobitSymbolLotSizeFilter : ToobitSymbolFilter
    {
        /// <summary>
        /// The minimal quantity of an order
        /// </summary>
        public decimal MinQuantity { get; set; }
        /// <summary>
        /// The maximum quantity of an order
        /// </summary>
        public decimal MaxQuantity { get; set; }
        /// <summary>
        /// The tick size of the quantity. The quantity can not have more precision as this and can only be incremented in steps of this.
        /// </summary>
        public decimal StepSize { get; set; }
    }

    /// <summary>
    /// Min notional filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<ToobitSymbolMinNotionalFilter>))]
    public record ToobitSymbolMinNotionalFilter : ToobitSymbolFilter
    {
        /// <summary>
        /// The minimal total quote quantity of an order. This is calculated by Price * Quantity.
        /// </summary>
        public decimal MinNotional { get; set; }

        /// <summary>
        /// Whether or not this filter is applied to market orders. If so the average trade price is used.
        /// </summary>
        public bool? ApplyToMarketOrders { get; set; }

        /// <summary>
        /// The amount of minutes the average price of trades is calculated over for market orders. 0 means the last price is used
        /// </summary>
        public int? AveragePriceMinutes { get; set; }
    }

    /// <summary>
    /// Trade amount filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<ToobitSymbolTradeAmountFilter>))]
    public record ToobitSymbolTradeAmountFilter : ToobitSymbolFilter
    {
        /// <summary>
        /// Min amount
        /// </summary>
        public decimal MinAmount { get; set; }
        /// <summary>
        /// Max amount
        /// </summary>
        public decimal MaxAmount { get; set; }
        /// <summary>
        /// Min buy price
        /// </summary>
        public decimal MinBuyPrice { get; set; }
    }

    /// <summary>
    /// Limit trading filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<ToobitSymbolTradeAmountFilter>))]
    public record ToobitSymbolLimitTradingFilter : ToobitSymbolFilter
    {
        /// <summary>
        /// Limit max sell price
        /// </summary>
        public decimal MaxSellPrice { get; set; }
        /// <summary>
        /// Buy price up rate
        /// </summary>
        public decimal BuyPriceUpRate { get; set; }
        /// <summary>
        /// Sell price down rate
        /// </summary>
        public decimal SellPriceDownRate { get; set; }
        /// <summary>
        /// Max open orders
        /// </summary>
        public int? MaxOpenOrders { get; set; }
        /// <summary>
        /// Max open conditional orders
        /// </summary>
        public int? MaxOpenConditionalOrders { get; set; }
    }

    /// <summary>
    /// Market trading filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<ToobitSymbolTradeAmountFilter>))]
    public record ToobitSymbolMarketTradingFilter : ToobitSymbolFilter
    {
        /// <summary>
        /// Buy price up rate
        /// </summary>
        public decimal BuyPriceUpRate { get; set; }
        /// <summary>
        /// Sell price down rate
        /// </summary>
        public decimal SellPriceDownRate { get; set; }
    }

    /// <summary>
    /// Open quote filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<ToobitSymbolTradeAmountFilter>))]
    public record ToobitSymbolOpenQuoteFilter : ToobitSymbolFilter
    {
        /// <summary>
        /// Limit max price
        /// </summary>
        public decimal LimitMaxPrice { get; set; }
        /// <summary>
        /// Limit min price
        /// </summary>
        public decimal LimitMinPrice { get; set; }
    }
}
