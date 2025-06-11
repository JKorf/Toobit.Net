using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.SharedApis;
using System;
using Toobit.Net.Objects.Options;

namespace Toobit.Net.Interfaces
{
    /// <summary>
    /// Toobit local order book factory
    /// </summary>
    public interface IToobitOrderBookFactory
    {
        
        /// <summary>
        /// UsdtFutures order book factory methods
        /// </summary>
        IOrderBookFactory<ToobitOrderBookOptions> UsdtFutures { get; }

        /// <summary>
        /// Spot order book factory methods
        /// </summary>
        IOrderBookFactory<ToobitOrderBookOptions> Spot { get; }


        /// <summary>
        /// Create a SymbolOrderBook for the symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        ISymbolOrderBook Create(SharedSymbol symbol, Action<ToobitOrderBookOptions>? options = null);

        
        /// <summary>
        /// Create a new UsdtFutures local order book instance
        /// </summary>
        ISymbolOrderBook CreateUsdtFutures(string symbol, Action<ToobitOrderBookOptions>? options = null);

        /// <summary>
        /// Create a new Spot local order book instance
        /// </summary>
        ISymbolOrderBook CreateSpot(string symbol, Action<ToobitOrderBookOptions>? options = null);

    }
}