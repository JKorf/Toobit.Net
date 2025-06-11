using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
