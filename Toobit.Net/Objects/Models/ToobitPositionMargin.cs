using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Position isolated margin
    /// </summary>
    public record ToobitPositionMargin
    {
        /// <summary>
        /// Code
        /// </summary>
        [JsonPropertyName("code")]
        public decimal Code { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Margin
        /// </summary>
        [JsonPropertyName("margin")]
        public decimal Margin { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }


}
