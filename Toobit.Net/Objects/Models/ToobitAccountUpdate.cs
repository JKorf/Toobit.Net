using System;
using System.Text.Json.Serialization;

namespace Toobit.Net.Objects.Models
{
    /// <summary>
    /// Account update
    /// </summary>
    public record ToobitAccountUpdate
    {
        /// <summary>
        /// ["<c>e</c>"] Event
        /// </summary>
        [JsonPropertyName("e")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>E</c>"] Event time
        /// </summary>
        [JsonPropertyName("E")]
        public DateTime EventTime { get; set; }
        /// <summary>
        /// ["<c>T</c>"] Can trade
        /// </summary>
        [JsonPropertyName("T")]
        public bool CanTrade { get; set; }
        /// <summary>
        /// ["<c>W</c>"] Can withdraw
        /// </summary>
        [JsonPropertyName("W")]
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// ["<c>D</c>"] Can deposit
        /// </summary>
        [JsonPropertyName("D")]
        public bool CanDeposit { get; set; }
        /// <summary>
        /// ["<c>B</c>"] Balances
        /// </summary>
        [JsonPropertyName("B")]
        public ToobitAccountUpdateBalance[] Balances { get; set; } = [];
    }

    /// <summary>
    /// Account balance
    /// </summary>
    public record ToobitAccountUpdateBalance
    {
        /// <summary>
        /// ["<c>a</c>"] Asset
        /// </summary>
        [JsonPropertyName("a")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>f</c>"] Free balance
        /// </summary>
        [JsonPropertyName("f")]
        public decimal Free { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Locked balance
        /// </summary>
        [JsonPropertyName("l")]
        public decimal Locked { get; set; }
    }


}
