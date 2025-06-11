﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Toobit.Net.Enums;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Margin type
    /// </summary>
    public record ToobitMarginType
    {
        /// <summary>
        /// Code
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbolId")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Margin type
        /// </summary>
        [JsonPropertyName("marginType")]
        public MarginType MarginType { get; set; }
    }


}
