using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Toobit.Net.Interfaces;
using Toobit.Net.Interfaces.Clients;
using Toobit.Net.Objects.Options;

namespace Toobit.Net.SymbolOrderBooks
{
    /// <summary>
    /// Toobit order book factory
    /// </summary>
    public class ToobitOrderBookFactory : IToobitOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public ToobitOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
                        
            UsdtFutures = new OrderBookFactory<ToobitOrderBookOptions>(CreateUsdtFutures, Create);
            Spot = new OrderBookFactory<ToobitOrderBookOptions>(CreateSpot, Create);
        }

        
         /// <inheritdoc />
        public IOrderBookFactory<ToobitOrderBookOptions> UsdtFutures { get; }

         /// <inheritdoc />
        public IOrderBookFactory<ToobitOrderBookOptions> Spot { get; }


        /// <inheritdoc />
        public ISymbolOrderBook Create(SharedSymbol symbol, Action<ToobitOrderBookOptions>? options = null)
        {
            var symbolName = symbol.GetSymbol(ToobitExchange.FormatSymbol);
            if (symbol.TradingMode == TradingMode.Spot)
                return CreateSpot(symbolName, options);

            return CreateUsdtFutures(symbolName, options);
        }

        
         /// <inheritdoc />
        public ISymbolOrderBook CreateUsdtFutures(string symbol, Action<ToobitOrderBookOptions>? options = null)
            => new ToobitUsdtFuturesSymbolOrderBook(symbol, options, 
                                                          _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                                          _serviceProvider.GetRequiredService<IToobitRestClient>(),
                                                          _serviceProvider.GetRequiredService<IToobitSocketClient>());

         /// <inheritdoc />
        public ISymbolOrderBook CreateSpot(string symbol, Action<ToobitOrderBookOptions>? options = null)
            => new ToobitSpotSymbolOrderBook(symbol, options, 
                                                          _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                                          _serviceProvider.GetRequiredService<IToobitRestClient>(),
                                                          _serviceProvider.GetRequiredService<IToobitSocketClient>());


    }
}
