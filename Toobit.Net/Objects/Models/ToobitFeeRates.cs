using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Fee rates
    /// </summary>
    public record ToobitFeeRates
    {
        /// <summary>
        /// ["<c>openMakerFee</c>"] Fee rate for maker opening positions
        /// </summary>
        [JsonPropertyName("openMakerFee")]
        public decimal OpenMakerFee { get; set; }
        /// <summary>
        /// ["<c>openTakerFee</c>"] Fee rate for taker opening positions
        /// </summary>
        [JsonPropertyName("openTakerFee")]
        public decimal OpenTakerFee { get; set; }
        /// <summary>
        /// ["<c>closeMakerFee</c>"] Fee rate for maker closing positions
        /// </summary>
        [JsonPropertyName("closeMakerFee")]
        public decimal CloseMakerFee { get; set; }
        /// <summary>
        /// ["<c>closeTakerFee</c>"] Fee rate for taker closing positions
        /// </summary>
        [JsonPropertyName("closeTakerFee")]
        public decimal CloseTakerFee { get; set; }
    }


}
