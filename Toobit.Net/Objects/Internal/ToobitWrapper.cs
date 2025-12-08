using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Internal
{
    internal record ToobitWrapper<T>
    {
        /// <summary>
        /// Code
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }


}
