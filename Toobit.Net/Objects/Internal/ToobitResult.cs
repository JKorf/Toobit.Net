using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Internal
{
    /// <summary>
    /// 
    /// </summary>
    internal record ToobitResult
    {
        /// <summary>
        /// Code
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
    }


}
