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
    /// Leverage info
    /// </summary>
    public record ToobitLeverageInfo
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbolId")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Margin type
        /// </summary>
        [JsonPropertyName("marginType")]
        public MarginType MarginType { get; set; }
    }


}
