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
        /// Event
        /// </summary>
        [JsonPropertyName("e")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// Event time
        /// </summary>
        [JsonPropertyName("E")]
        public DateTime EventTime { get; set; }
        /// <summary>
        /// Can trade
        /// </summary>
        [JsonPropertyName("T")]
        public bool CanTrade { get; set; }
        /// <summary>
        /// Can withdraw
        /// </summary>
        [JsonPropertyName("W")]
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// Can deposit
        /// </summary>
        [JsonPropertyName("D")]
        public bool CanDeposit { get; set; }
        /// <summary>
        /// Balances
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
        /// Asset
        /// </summary>
        [JsonPropertyName("a")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Free balance
        /// </summary>
        [JsonPropertyName("f")]
        public decimal Free { get; set; }
        /// <summary>
        /// Locked balance
        /// </summary>
        [JsonPropertyName("l")]
        public decimal Locked { get; set; }
    }


}
