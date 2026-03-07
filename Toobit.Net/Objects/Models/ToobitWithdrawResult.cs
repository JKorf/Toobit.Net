using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Withdraw result
    /// </summary>
    public record ToobitWithdrawResult
    {
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; }
        /// <summary>
        /// ["<c>success</c>"] Success
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        /// <summary>
        /// ["<c>needBrokerAudit</c>"] Needs broker audit
        /// </summary>
        [JsonPropertyName("needBrokerAudit")]
        public bool NeedBrokerAudit { get; set; }
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>refuseReason</c>"] Refuse reason
        /// </summary>
        [JsonPropertyName("refuseReason")]
        public string? RefuseReason { get; set; }
    }


}
