using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
