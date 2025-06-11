using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Funding rate info
    /// </summary>
    public record ToobitFundingRate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Next funding time
        /// </summary>
        [JsonPropertyName("nextFundingTime")]
        public DateTime NextFundingTime { get; set; }
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonPropertyName("rate")]
        public decimal FundingRate { get; set; }
    }


}
