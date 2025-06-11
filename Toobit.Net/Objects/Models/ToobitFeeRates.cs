using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Fee rates
    /// </summary>
    public record ToobitFeeRates
    {
        /// <summary>
        /// Fee rate for maker opening positions
        /// </summary>
        [JsonPropertyName("openMakerFee")]
        public decimal OpenMakerFee { get; set; }
        /// <summary>
        /// Fee rate for taker opening positions
        /// </summary>
        [JsonPropertyName("openTakerFee")]
        public decimal OpenTakerFee { get; set; }
        /// <summary>
        /// Fee rate for maker closing positions
        /// </summary>
        [JsonPropertyName("closeMakerFee")]
        public decimal CloseMakerFee { get; set; }
        /// <summary>
        /// Fee rate for taker closing positions
        /// </summary>
        [JsonPropertyName("closeTakerFee")]
        public decimal CloseTakerFee { get; set; }
    }


}
