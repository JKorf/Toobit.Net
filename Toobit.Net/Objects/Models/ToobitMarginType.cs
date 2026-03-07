using System.Text.Json.Serialization;
using Toobit.Net.Enums;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Margin type
    /// </summary>
    public record ToobitMarginType
    {
        /// <summary>
        /// ["<c>code</c>"] Code
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }
        /// <summary>
        /// ["<c>symbolId</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbolId")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>marginType</c>"] Margin type
        /// </summary>
        [JsonPropertyName("marginType")]
        public MarginType MarginType { get; set; }
    }


}
