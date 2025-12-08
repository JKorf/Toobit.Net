using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Internal
{
    internal record ToobitDataResult<T>
    {
        /// <summary>
        /// Code
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }
        /// <summary>
        /// Result
        /// </summary>
        [JsonPropertyName("result")]
        public T Result { get; set; } = default!;
    }


}
