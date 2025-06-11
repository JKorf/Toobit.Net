using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Toobit.Net.Enums;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// 
    /// </summary>
    public record ToobitExchangeInfo
    {
        /// <summary>
        /// Timezone
        /// </summary>
        [JsonPropertyName("timezone")]
        public string Timezone { get; set; } = string.Empty;
        /// <summary>
        /// Server time
        /// </summary>
        [JsonPropertyName("serverTime")]
        public DateTime ServerTime { get; set; }
        /// <summary>
        /// Spot symbols
        /// </summary>
        [JsonPropertyName("symbols")]
        public ToobitSpotSymbol[] SpotSymbols { get; set; } = [];
        /// <summary>
        /// Contract symbols
        /// </summary>
        [JsonPropertyName("contracts")]
        public ToobitFuturesSymbol[] ContractSymbols { get; set; } = [];
        /// <summary>
        /// Assets
        /// </summary>
        [JsonPropertyName("coins")]
        public ToobitAsset[] Assets { get; set; } = [];
    }

    /// <summary>
    /// Symbol info
    /// </summary>
    public record ToobitBaseSymbol
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("symbolName")]
        public string SymbolName { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus Status { get; set; }
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonPropertyName("baseAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Base asset precision
        /// </summary>
        [JsonPropertyName("baseAssetPrecision")]
        public decimal BaseAssetPrecision { get; set; }
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonPropertyName("quoteAsset")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote precision
        /// </summary>
        [JsonPropertyName("quotePrecision")]
        public decimal QuotePrecision { get; set; }
        [JsonInclude, JsonPropertyName("quoteAssetPrecision")]
        internal decimal QuoteAssetPrecision { get => QuotePrecision; set => QuotePrecision = value; }
        /// <summary>
        /// Iceberg allowed
        /// </summary>
        [JsonPropertyName("icebergAllowed")]
        public bool IcebergAllowed { get; set; }
        /// <summary>
        /// Filters
        /// </summary>
        [JsonPropertyName("filters")]
        public ToobitSymbolFilter[] Filters { get; set; } = [];
        /// <summary>
        /// Filter for max accuracy of the quantity for this symbol
        /// </summary>
        [JsonIgnore]
        public ToobitSymbolLotSizeFilter? LotSizeFilter => Filters.OfType<ToobitSymbolLotSizeFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for the minimal quote quantity of an order for this symbol
        /// </summary>
        [JsonIgnore]
        public ToobitSymbolMinNotionalFilter? MinNotionalFilter => Filters.OfType<ToobitSymbolMinNotionalFilter>().FirstOrDefault();
        /// <summary>
        /// Filter for the max accuracy of the price for this symbol
        /// </summary>
        [JsonIgnore]
        public ToobitSymbolPriceFilter? PriceFilter => Filters.OfType<ToobitSymbolPriceFilter>().FirstOrDefault();
        /// <summary>
        /// Trade amount filter
        /// </summary>
        [JsonIgnore]
        public ToobitSymbolTradeAmountFilter? TradeAmountFilter => Filters.OfType<ToobitSymbolTradeAmountFilter>().FirstOrDefault();
        /// <summary>
        /// Limit trading filter
        /// </summary>
        [JsonIgnore]
        public ToobitSymbolLimitTradingFilter? LimitTradeFilter => Filters.OfType<ToobitSymbolLimitTradingFilter>().FirstOrDefault();
        /// <summary>
        /// Market trading filter
        /// </summary>
        [JsonIgnore]
        public ToobitSymbolMarketTradingFilter? MarketTradeFilter => Filters.OfType<ToobitSymbolMarketTradingFilter>().FirstOrDefault();
        /// <summary>
        /// Open quote filter
        /// </summary>
        [JsonIgnore]
        public ToobitSymbolOpenQuoteFilter? OpenQuoteFilter => Filters.OfType<ToobitSymbolOpenQuoteFilter>().FirstOrDefault();
    }


    /// <summary>
    /// Symbol info
    /// </summary>
    public record ToobitSpotSymbol : ToobitBaseSymbol
    {
        /// <summary>
        /// Base asset name
        /// </summary>
        [JsonPropertyName("baseAssetName")]
        public string BaseAssetName { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset name
        /// </summary>
        [JsonPropertyName("quoteAssetName")]
        public string QuoteAssetName { get; set; } = string.Empty;
        /// <summary>
        /// Is aggregate
        /// </summary>
        [JsonPropertyName("isAggregate")]
        public bool IsAggregate { get; set; }
        /// <summary>
        /// Allow margin
        /// </summary>
        [JsonPropertyName("allowMargin")]
        public bool AllowMargin { get; set; }        
    }

    /// <summary>
    /// Symbol info
    /// </summary>
    public record ToobitFuturesSymbol : ToobitBaseSymbol
    {
        /// <summary>
        /// Exchange id
        /// </summary>
        [JsonPropertyName("exchangeId")]
        public string ExchangeId { get; set; } = string.Empty;        
        /// <summary>
        /// Inverse
        /// </summary>
        [JsonPropertyName("inverse")]
        public bool? Inverse { get; set; }
        /// <summary>
        /// Index
        /// </summary>
        [JsonPropertyName("index")]
        public string? Index { get; set; } = string.Empty;
        /// <summary>
        /// Index token
        /// </summary>
        [JsonPropertyName("indexToken")]
        public string? IndexToken { get; set; } = string.Empty;
        /// <summary>
        /// Margin token
        /// </summary>
        [JsonPropertyName("marginToken")]
        public string? MarginToken { get; set; } = string.Empty;
        /// <summary>
        /// Underlying
        /// </summary>
        [JsonPropertyName("underlying")]
        public string? Underlying { get; set; } = string.Empty;
        /// <summary>
        /// Contract multiplier
        /// </summary>
        [JsonPropertyName("contractMultiplier")]
        public decimal? ContractMultiplier { get; set; }
        /// <summary>
        /// Risk limits
        /// </summary>
        [JsonPropertyName("riskLimits")]
        public ToobitRiskLimit[] RiskLimits { get; set; } = [];
    }

    /// <summary>
    /// Risk limit
    /// </summary>
    public record ToobitRiskLimit
    {
        /// <summary>
        /// Risk limit id
        /// </summary>
        [JsonPropertyName("riskLimitId")]
        public string RiskLimitId { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Initial margin
        /// </summary>
        [JsonPropertyName("initialMargin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// Maintenance margin
        /// </summary>
        [JsonPropertyName("mainMargin")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// Is white
        /// </summary>
        [JsonPropertyName("isWhite")]
        public bool IsWhite { get; set; }
    }

    /// <summary>
    /// Asset info
    /// </summary>
    public record ToobitAsset
    {
        /// <summary>
        /// OriginalId
        /// </summary>
        [JsonPropertyName("orgId")]
        public string OriginalId { get; set; } = string.Empty;
        /// <summary>
        /// Asset id
        /// </summary>
        [JsonPropertyName("coinId")]
        public string AssetId { get; set; } = string.Empty;
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("coinName")]
        public string AssetName { get; set; } = string.Empty;
        /// <summary>
        /// Full asset name
        /// </summary>
        [JsonPropertyName("coinFullName (tokenFullName)")]
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// Allow withdraw
        /// </summary>
        [JsonPropertyName("allowWithdraw")]
        public bool AllowWithdraw { get; set; }
        /// <summary>
        /// Allow deposit
        /// </summary>
        [JsonPropertyName("allowDeposit")]
        public bool AllowDeposit { get; set; }
        /// <summary>
        /// Networks
        /// </summary>
        [JsonPropertyName("chainTypes")]
        public ToobitAssetNetwork[] Networks { get; set; } = [];
    }

    /// <summary>
    /// 
    /// </summary>
    public record ToobitAssetNetwork
    {
        /// <summary>
        /// Network type
        /// </summary>
        [JsonPropertyName("chainType")]
        public string NetworkType { get; set; } = string.Empty;
        /// <summary>
        /// Withdraw fee
        /// </summary>
        [JsonPropertyName("withdrawFee")]
        public decimal WithdrawFee { get; set; }
        /// <summary>
        /// Min withdraw quantity
        /// </summary>
        [JsonPropertyName("minWithdrawQuantity")]
        public decimal MinWithdrawQuantity { get; set; }
        /// <summary>
        /// Max withdraw quantity
        /// </summary>
        [JsonPropertyName("maxWithdrawQuantity")]
        public decimal MaxWithdrawQuantity { get; set; }
        /// <summary>
        /// Min deposit quantity
        /// </summary>
        [JsonPropertyName("minDepositQuantity")]
        public decimal MinDepositQuantity { get; set; }
        /// <summary>
        /// Allow deposit
        /// </summary>
        [JsonPropertyName("allowDeposit")]
        public bool AllowDeposit { get; set; }
        /// <summary>
        /// Allow withdraw
        /// </summary>
        [JsonPropertyName("allowWithdraw")]
        public bool AllowWithdraw { get; set; }
    }


}
