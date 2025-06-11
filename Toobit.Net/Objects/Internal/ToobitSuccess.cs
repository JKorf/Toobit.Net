using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
