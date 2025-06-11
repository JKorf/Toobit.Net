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
    /// Flow type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FlowType>))]
    public enum FlowType
    {
        /// <summary>
        /// Trade fee
        /// </summary>
        [Map("10")]
        Fee,
        /// <summary>
        /// Funding fee
        /// </summary>
        [Map("32")]
        FundingFee,
        /// <summary>
        /// Realized profit and loss
        /// </summary>
        [Map("28")]
        RealizedPnl,
        /// <summary>
        /// Transfer
        /// </summary>
        [Map("51")]
        Transfer,
        /// <summary>
        /// Forced liquidation
        /// </summary>
        [Map("700")]
        ForceLiquidation,
        /// <summary>
        /// Auto deleverage
        /// </summary>
        [Map("701")]
        AutoDeleverage
    }
}
