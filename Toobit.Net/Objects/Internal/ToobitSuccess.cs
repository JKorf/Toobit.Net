using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Internal
{
    /// <summary>
    /// 
    /// </summary>
    internal record ToobitSuccess
    {
        /// <summary>
        /// Success
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }


}
