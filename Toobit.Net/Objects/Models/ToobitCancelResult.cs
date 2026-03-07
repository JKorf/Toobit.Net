using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Cancel result
    /// </summary>
    public record ToobitCancelResult
    {
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>code</c>"] Error code
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }
    }
}
