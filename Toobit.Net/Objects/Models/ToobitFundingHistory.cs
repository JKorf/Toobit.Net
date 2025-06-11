using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Funding rate history
    /// </summary>
    public record ToobitFundingHistory
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Funding time
        /// </summary>
        [JsonPropertyName("settleTime")]
        public DateTime FundingTime { get; set; }
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonPropertyName("settleRate")]
        public decimal FundingRate { get; set; }
    }


}
