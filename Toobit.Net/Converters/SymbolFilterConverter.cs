using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Toobit.Net.Enums;
using Toobit.Net.Objects.Models;

namespace Toobit.Net.Converters
{
    internal class SymbolFilterConverterImp<T> : JsonConverter<T>
    {
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var obj = JsonDocument.ParseValue(ref reader).RootElement;
            var type = obj.GetProperty("filterType").Deserialize((JsonTypeInfo<SymbolFilterType>)options.GetTypeInfo(typeof(SymbolFilterType)));
            ToobitSymbolFilter result;
            switch (type)
            {
                case SymbolFilterType.LotSize:
                    result = new ToobitSymbolLotSizeFilter
                    {
                        MaxQuantity = decimal.Parse(obj.GetProperty("maxQty").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        MinQuantity = decimal.Parse(obj.GetProperty("minQty").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        StepSize = decimal.Parse(obj.GetProperty("stepSize").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture)
                    };
                    break;
                case SymbolFilterType.PriceFilter:
                    result = new ToobitSymbolPriceFilter
                    {
                        MaxPrice = decimal.Parse(obj.GetProperty("maxPrice").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        MinPrice = decimal.Parse(obj.GetProperty("minPrice").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        TickSize = decimal.Parse(obj.GetProperty("tickSize").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture)
                    };
                    break;
                case SymbolFilterType.MinNotional:
                    result = new ToobitSymbolMinNotionalFilter
                    {
                        MinNotional = decimal.Parse(obj.TryGetProperty("minNotional", out var minNotional) ? minNotional.GetString()! : obj.GetProperty("notional").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        ApplyToMarketOrders = obj.TryGetProperty("applyToMarket", out var applyToMarket) ? applyToMarket.GetBoolean() : null,
                        AveragePriceMinutes = obj.TryGetProperty("avgPriceMins", out var avgPrice) ? avgPrice.GetInt32() : null
                    };
                    break;
                case SymbolFilterType.TradeAmount:
                    result = new ToobitSymbolTradeAmountFilter
                    {
                        MinAmount = decimal.Parse(obj.GetProperty("minAmount").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        MaxAmount = decimal.Parse(obj.GetProperty("maxAmount").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        MinBuyPrice = decimal.Parse(obj.GetProperty("minBuyPrice").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture)
                    };
                    break;
                case SymbolFilterType.LimitTrading:
                    result = new ToobitSymbolLimitTradingFilter
                    {
                        MaxSellPrice = decimal.Parse(obj.GetProperty("maxSellPrice").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        BuyPriceUpRate = decimal.Parse(obj.GetProperty("buyPriceUpRate").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        SellPriceDownRate = decimal.Parse(obj.GetProperty("sellPriceDownRate").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        MaxOpenOrders = obj.TryGetProperty("maxEntrustNum", out var maxEnt) ? (maxEnt.ValueKind == JsonValueKind.String ? int.Parse(maxEnt.GetString()!) : maxEnt.GetInt32()) : null,
                        MaxOpenConditionalOrders = obj.TryGetProperty("maxConditionNum", out var maxCon) ? (maxCon.ValueKind == JsonValueKind.String ? int.Parse(maxCon.GetString()!) : maxCon.GetInt32()) : null,
                    };
                    break;
                case SymbolFilterType.MarketTrading:
                    result = new ToobitSymbolMarketTradingFilter
                    {
                        BuyPriceUpRate = decimal.Parse(obj.GetProperty("buyPriceUpRate").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        SellPriceDownRate = decimal.Parse(obj.GetProperty("sellPriceDownRate").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture)
                    };
                    break;

                case SymbolFilterType.OpenQuote:
                    result = new ToobitSymbolOpenQuoteFilter
                    {
                        LimitMinPrice = decimal.Parse(obj.GetProperty("limitMinPrice").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture),
                        LimitMaxPrice = decimal.Parse(obj.GetProperty("limitMaxPrice").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture)
                    };
                    break;
                default:
                    Trace.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss:fff} | Warning | Can't parse symbol filter of type: " + obj.GetProperty("filterType").GetString());
                    result = new ToobitSymbolFilter();
                    break;
            }
#pragma warning restore 8604
            result.FilterType = type;
            return (T)(object)result;
        }

#if NET5_0_OR_GREATER
        [UnconditionalSuppressMessage("AssemblyLoadTrimming", "IL3050:RequiresUnreferencedCode", Justification = "JsonSerializerOptions provided here has TypeInfoResolver set")]
        [UnconditionalSuppressMessage("AssemblyLoadTrimming", "IL2026:RequiresUnreferencedCode", Justification = "JsonSerializerOptions provided here has TypeInfoResolver set")]
#endif
        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize<T>(writer, value, SerializerOptions.WithConverters(ToobitExchange._serializerContext));
        }
    }
}