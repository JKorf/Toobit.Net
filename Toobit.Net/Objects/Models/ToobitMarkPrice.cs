using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Mark price
    /// </summary>
    public record ToobitMarkPrice
    {
        /// <summary>
        /// Exchange id
        /// </summary>
        [JsonPropertyName("exchangeId")]
        public long ExchangeId { get; set; }
        /// <summary>
        /// Symbol id
        /// </summary>
        [JsonPropertyName("symbolId")]
        public string SymbolId { get; set; } = string.Empty;
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
    }


}
