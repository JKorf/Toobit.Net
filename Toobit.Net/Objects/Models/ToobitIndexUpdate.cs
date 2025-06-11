using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Index price update
    /// </summary>
    public record ToobitIndexUpdate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Index price
        /// </summary>
        [JsonPropertyName("index")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// Average of indices over 10 minutes
        /// </summary>
        [JsonPropertyName("edp")]
        public decimal IndicesAverage { get; set; }
        /// <summary>
        /// Formula
        /// </summary>
        [JsonPropertyName("formula")]
        public string Formula { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
    }
}
