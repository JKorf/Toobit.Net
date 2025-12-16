using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Withdraw result
    /// </summary>
    public record ToobitWithdrawResult
    {
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; }
        /// <summary>
        /// Success
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        /// <summary>
        /// Needs broker audit
        /// </summary>
        [JsonPropertyName("needBrokerAudit")]
        public bool NeedBrokerAudit { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Refuse reason
        /// </summary>
        [JsonPropertyName("refuseReason")]
        public string? RefuseReason { get; set; }
    }


}
