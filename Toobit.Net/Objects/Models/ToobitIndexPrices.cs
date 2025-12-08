using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Index price info
    /// </summary>
    public record ToobitIndexPrices
    {
        /// <summary>
        /// Index prices
        /// </summary>
        [JsonPropertyName("index")]
        public Dictionary<string, decimal> IndexPrices { get; set; } = null!;
        /// <summary>
        /// Indices average over 10 minutes
        /// </summary>
        [JsonPropertyName("edp")]
        public Dictionary<string, decimal> IndicesAverage { get; set; } = null!;
    }
}
