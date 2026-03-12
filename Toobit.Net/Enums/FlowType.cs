using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Toobit.Net.Enums
{
    /// <summary>
    /// Flow type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FlowType>))]
    public enum FlowType
    {
        /// <summary>
        /// ["<c>10</c>"] Trade fee
        /// </summary>
        [Map("10")]
        Fee,
        /// <summary>
        /// ["<c>32</c>"] Funding fee
        /// </summary>
        [Map("32")]
        FundingFee,
        /// <summary>
        /// ["<c>28</c>"] Realized profit and loss
        /// </summary>
        [Map("28")]
        RealizedPnl,
        /// <summary>
        /// ["<c>51</c>"] Transfer
        /// </summary>
        [Map("51")]
        Transfer,
        /// <summary>
        /// ["<c>700</c>"] Forced liquidation
        /// </summary>
        [Map("700")]
        ForceLiquidation,
        /// <summary>
        /// ["<c>701</c>"] Auto deleverage
        /// </summary>
        [Map("701")]
        AutoDeleverage
    }
}
