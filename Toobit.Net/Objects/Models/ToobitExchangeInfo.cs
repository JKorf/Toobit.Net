using System;
using System.Linq;
using System.Text.Json.Serialization;
using Toobit.Net.Enums;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// 
    /// </summary>
    public record ToobitExchangeInfo
    {
        /// <summary>
        /// ["<c>timezone</c>"] Timezone
        /// </summary>
        [JsonPropertyName("timezone")]
        public string Timezone { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>serverTime</c>"] Server time
        /// </summary>
        [JsonPropertyName("serverTime")]
        public DateTime ServerTime { get; set; }
        /// <summary>
        /// ["<c>symbols</c>"] Spot symbols
        /// </summary>
        [JsonPropertyName("symbols")]
        public ToobitSpotSymbol[] SpotSymbols { get; set; } = [];
        /// <summary>
        /// ["<c>contracts</c>"] Contract symbols
        /// </summary>
        [JsonPropertyName("contracts")]
        public ToobitFuturesSymbol[] ContractSymbols { get; set; } = [];
        /// <summary>
        /// ["<c>coins</c>"] Assets
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
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbolName</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("symbolName")]
        public string SymbolName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus Status { get; set; }
        /// <summary>
        /// ["<c>baseAsset</c>"] Base asset
        /// </summary>
        [JsonPropertyName("baseAsset")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>baseAssetPrecision</c>"] Base asset precision
        /// </summary>
        [JsonPropertyName("baseAssetPrecision")]
        public decimal BaseAssetPrecision { get; set; }
        /// <summary>
        /// ["<c>quoteAsset</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("quoteAsset")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quotePrecision</c>"] Quote precision
        /// </summary>
        [JsonPropertyName("quotePrecision")]
        public decimal QuotePrecision { get; set; }
        [JsonInclude, JsonPropertyName("quoteAssetPrecision")]
        internal decimal QuoteAssetPrecision { get => QuotePrecision; set => QuotePrecision = value; }
        /// <summary>
        /// ["<c>icebergAllowed</c>"] Iceberg allowed
        /// </summary>
        [JsonPropertyName("icebergAllowed")]
        public bool IcebergAllowed { get; set; }
        /// <summary>
        /// ["<c>filters</c>"] Filters
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
        /// ["<c>baseAssetName</c>"] Base asset name
        /// </summary>
        [JsonPropertyName("baseAssetName")]
        public string BaseAssetName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quoteAssetName</c>"] Quote asset name
        /// </summary>
        [JsonPropertyName("quoteAssetName")]
        public string QuoteAssetName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>isAggregate</c>"] Is aggregate
        /// </summary>
        [JsonPropertyName("isAggregate")]
        public bool IsAggregate { get; set; }
        /// <summary>
        /// ["<c>allowMargin</c>"] Allow margin
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
        /// ["<c>exchangeId</c>"] Exchange id
        /// </summary>
        [JsonPropertyName("exchangeId")]
        public string ExchangeId { get; set; } = string.Empty;        
        /// <summary>
        /// ["<c>inverse</c>"] Inverse
        /// </summary>
        [JsonPropertyName("inverse")]
        public bool? Inverse { get; set; }
        /// <summary>
        /// ["<c>index</c>"] Index
        /// </summary>
        [JsonPropertyName("index")]
        public string? Index { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>indexToken</c>"] Index token
        /// </summary>
        [JsonPropertyName("indexToken")]
        public string? IndexToken { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>marginToken</c>"] Margin token
        /// </summary>
        [JsonPropertyName("marginToken")]
        public string? MarginToken { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>underlying</c>"] Underlying
        /// </summary>
        [JsonPropertyName("underlying")]
        public string? Underlying { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contractMultiplier</c>"] Contract multiplier
        /// </summary>
        [JsonPropertyName("contractMultiplier")]
        public decimal? ContractMultiplier { get; set; }
        /// <summary>
        /// ["<c>riskLimits</c>"] Risk limits
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
        /// ["<c>riskLimitId</c>"] Risk limit id
        /// </summary>
        [JsonPropertyName("riskLimitId")]
        public string RiskLimitId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quantity</c>"] Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>initialMargin</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("initialMargin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// ["<c>mainMargin</c>"] Maintenance margin
        /// </summary>
        [JsonPropertyName("mainMargin")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>isWhite</c>"] Is white
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
        /// ["<c>orgId</c>"] OriginalId
        /// </summary>
        [JsonPropertyName("orgId")]
        public string OriginalId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>coinId</c>"] Asset id
        /// </summary>
        [JsonPropertyName("coinId")]
        public string AssetId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>coinName</c>"] Asset name
        /// </summary>
        [JsonPropertyName("coinName")]
        public string AssetName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>coinFullName (tokenFullName)</c>"] Full asset name
        /// </summary>
        [JsonPropertyName("coinFullName (tokenFullName)")]
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>allowWithdraw</c>"] Allow withdraw
        /// </summary>
        [JsonPropertyName("allowWithdraw")]
        public bool AllowWithdraw { get; set; }
        /// <summary>
        /// ["<c>allowDeposit</c>"] Allow deposit
        /// </summary>
        [JsonPropertyName("allowDeposit")]
        public bool AllowDeposit { get; set; }
        /// <summary>
        /// ["<c>chainTypes</c>"] Networks
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
        /// ["<c>chainType</c>"] Network type
        /// </summary>
        [JsonPropertyName("chainType")]
        public string NetworkType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>withdrawFee</c>"] Withdraw fee
        /// </summary>
        [JsonPropertyName("withdrawFee")]
        public decimal WithdrawFee { get; set; }
        /// <summary>
        /// ["<c>minWithdrawQuantity</c>"] Min withdraw quantity
        /// </summary>
        [JsonPropertyName("minWithdrawQuantity")]
        public decimal MinWithdrawQuantity { get; set; }
        /// <summary>
        /// ["<c>maxWithdrawQuantity</c>"] Max withdraw quantity
        /// </summary>
        [JsonPropertyName("maxWithdrawQuantity")]
        public decimal MaxWithdrawQuantity { get; set; }
        /// <summary>
        /// ["<c>minDepositQuantity</c>"] Min deposit quantity
        /// </summary>
        [JsonPropertyName("minDepositQuantity")]
        public decimal MinDepositQuantity { get; set; }
        /// <summary>
        /// ["<c>allowDeposit</c>"] Allow deposit
        /// </summary>
        [JsonPropertyName("allowDeposit")]
        public bool AllowDeposit { get; set; }
        /// <summary>
        /// ["<c>allowWithdraw</c>"] Allow withdraw
        /// </summary>
        [JsonPropertyName("allowWithdraw")]
        public bool AllowWithdraw { get; set; }
    }


}
