using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.OrderBook;
using Microsoft.Extensions.Logging;
using Toobit.Net.Clients;
using Toobit.Net.Interfaces.Clients;
using Toobit.Net.Objects.Models;
using Toobit.Net.Objects.Options;

namespace Toobit.Net.SymbolOrderBooks
{
    /// <summary>
    /// Implementation for a synchronized order book. After calling Start the order book will sync itself and keep up to date with new data. It will automatically try to reconnect and resync in case of a lost/interrupted connection.
    /// Make sure to check the State property to see if the order book is synced.
    /// </summary>
    public class ToobitSpotSymbolOrderBook : SymbolOrderBook
    {
        private readonly bool _clientOwner;
        private readonly IToobitRestClient _restClient;
        private readonly IToobitSocketClient _socketClient;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public ToobitSpotSymbolOrderBook(string symbol, Action<ToobitOrderBookOptions>? optionsDelegate = null)
            : this(symbol, optionsDelegate, null, null, null)
        {
            _clientOwner = true;
        }

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="logger">Logger</param>
        /// <param name="restClient">Rest client instance</param>
        /// <param name="socketClient">Socket client instance</param>
        public ToobitSpotSymbolOrderBook(
            string symbol,
            Action<ToobitOrderBookOptions>? optionsDelegate,
            ILoggerFactory? logger,
            IToobitRestClient? restClient,
            IToobitSocketClient? socketClient) : base(logger, "Toobit", "Spot", symbol)
        {
            var options = ToobitOrderBookOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            _strictLevels = false;
            _sequencesAreConsecutive = false;
                
            _clientOwner = socketClient == null;
            _socketClient = socketClient ?? new ToobitSocketClient();
            _restClient = restClient ?? new ToobitRestClient();
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync(CancellationToken ct)
        {
            var subResult = await _socketClient.SpotApi.SubscribeToOrderBookUpdatesAsync(Symbol, HandleUpdate, ct).ConfigureAwait(false);

            if (!subResult)
                return new CallResult<UpdateSubscription>(subResult.Error!);

            if (ct.IsCancellationRequested)
            {
                await subResult.Data.CloseAsync().ConfigureAwait(false);
                return subResult.AsError<UpdateSubscription>(new CancellationRequestedError());
            }

            Status = OrderBookStatus.Syncing;

            // Small delay to make sure the snapshot is from after our first stream update
            await Task.Delay(200).ConfigureAwait(false);
            var bookResult = await _restClient.SpotApi.ExchangeData.GetOrderBookAsync(Symbol, Levels ?? 5000).ConfigureAwait(false);
            if (!bookResult)
            {
                _logger.Log(Microsoft.Extensions.Logging.LogLevel.Debug, $"{Api} order book {Symbol} failed to retrieve initial order book");
                await _socketClient.UnsubscribeAsync(subResult.Data).ConfigureAwait(false);
                return new CallResult<UpdateSubscription>(bookResult.Error!);
            }

            // Filter duplicates, for some reason the server sometimes returns duplicate prices in the snapshot
            var bids = new Dictionary<decimal, ISymbolOrderBookEntry>();
            var asks = new Dictionary<decimal, ISymbolOrderBookEntry>();
            foreach(var item in bookResult.Data.Bids)
            {
                if (!bids.ContainsKey(item.Price))
                    bids.Add(item.Price, item);
            }

            foreach (var item in bookResult.Data.Asks)
            {
                if (!asks.ContainsKey(item.Price))
                    asks.Add(item.Price, item);
            }

            SetInitialOrderBook(bookResult.Data.Timestamp.Ticks, bids.Values.ToArray(), asks.Values.ToArray());
            return new CallResult<UpdateSubscription>(subResult.Data);
        }

        private void HandleUpdate(DataEvent<ToobitOrderBookUpdate> data)
        {
            if (data.Data.Asks.Length == 0 && data.Data.Bids.Length == 0)
                return;

            UpdateOrderBook(data.Data.Timestamp.Ticks, data.Data.Bids, data.Data.Asks, data.DataTime, data.DataTimeLocal);
        }

        /// <inheritdoc />
        protected override void DoReset()
        {
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync(CancellationToken ct)
        {
            var bookResult = await _restClient.SpotApi.ExchangeData.GetOrderBookAsync(Symbol, Levels ?? 5000).ConfigureAwait(false);
            if (!bookResult)
                return new CallResult<bool>(bookResult.Error!);

            SetInitialOrderBook(bookResult.Data.Timestamp.Ticks, bookResult.Data.Bids, bookResult.Data.Asks);
            return new CallResult<bool>(true);
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (_clientOwner)
            {
                _restClient?.Dispose();
                _socketClient?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
