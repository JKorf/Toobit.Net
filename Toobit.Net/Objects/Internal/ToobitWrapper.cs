using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
