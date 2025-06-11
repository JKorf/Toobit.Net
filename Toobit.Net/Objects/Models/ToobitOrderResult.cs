using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Order result
    /// </summary>
    internal record ToobitOrderResult
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("msg")]
        public string? Message { get; set; }

        [JsonPropertyName("order")]
        public ToobitOrder? Order { get; set; }
    }
}
