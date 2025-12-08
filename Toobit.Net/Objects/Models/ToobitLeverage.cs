using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Leverage info
    /// </summary>
    public record ToobitLeverage
    {
        /// <summary>
        /// Code
        /// </summary>
        [JsonPropertyName("code")]
        public decimal Code { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public int Leverage { get; set; }
    }


}
