﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Toobit.Net.Objects.Sockets
{
    internal record PingResponse
    {
        [JsonPropertyName("pong")]
        public long Pong { get; set; }
    }
}
