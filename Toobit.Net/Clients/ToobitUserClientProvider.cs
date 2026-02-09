using Toobit.Net.Interfaces.Clients;
using Toobit.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Net.Http;

namespace Toobit.Net.Clients
{
    /// <inheritdoc />
    public class ToobitUserClientProvider : IToobitUserClientProvider
    {
        private ConcurrentDictionary<string, IToobitRestClient> _restClients = new ConcurrentDictionary<string, IToobitRestClient>();
        private ConcurrentDictionary<string, IToobitSocketClient> _socketClients = new ConcurrentDictionary<string, IToobitSocketClient>();
        
        private readonly IOptions<ToobitRestOptions> _restOptions;
        private readonly IOptions<ToobitSocketOptions> _socketOptions;
        private readonly HttpClient _httpClient;
        private readonly ILoggerFactory? _loggerFactory;

        /// <inheritdoc />
        public string ExchangeName => ToobitExchange.ExchangeName;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="optionsDelegate">Options to use for created clients</param>
        public ToobitUserClientProvider(Action<ToobitOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate).Rest), Options.Create(ApplyOptionsDelegate(optionsDelegate).Socket))
        {
        }
        
        /// <summary>
        /// ctor
        /// </summary>
        public ToobitUserClientProvider(
            HttpClient? httpClient,
            ILoggerFactory? loggerFactory,
            IOptions<ToobitRestOptions> restOptions,
            IOptions<ToobitSocketOptions> socketOptions)
        {
            _httpClient = httpClient ?? new HttpClient();
            _loggerFactory = loggerFactory;
            _restOptions = restOptions;
            _socketOptions = socketOptions;
        }

        /// <inheritdoc />
        public void InitializeUserClient(string userIdentifier, ApiCredentials credentials, ToobitEnvironment? environment = null)
        {
            CreateRestClient(userIdentifier, credentials, environment);
            CreateSocketClient(userIdentifier, credentials, environment);
        }

        /// <inheritdoc />
        public void ClearUserClients(string userIdentifier)
        {
            _restClients.TryRemove(userIdentifier, out _);
            _socketClients.TryRemove(userIdentifier, out _);
        }

        /// <inheritdoc />
        public IToobitRestClient GetRestClient(string userIdentifier, ApiCredentials? credentials = null, ToobitEnvironment? environment = null)
        {
            if (!_restClients.TryGetValue(userIdentifier, out var client) || client.Disposed)
                client = CreateRestClient(userIdentifier, credentials, environment);

            return client;
        }

        /// <inheritdoc />
        public IToobitSocketClient GetSocketClient(string userIdentifier, ApiCredentials? credentials = null, ToobitEnvironment? environment = null)
        {
            if (!_socketClients.TryGetValue(userIdentifier, out var client) || client.Disposed)
                client = CreateSocketClient(userIdentifier, credentials, environment);

            return client;
        }

        private IToobitRestClient CreateRestClient(string userIdentifier, ApiCredentials? credentials, ToobitEnvironment? environment)
        {
            var clientRestOptions = SetRestEnvironment(environment);
            var client = new ToobitRestClient(_httpClient, _loggerFactory, clientRestOptions);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _restClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IToobitSocketClient CreateSocketClient(string userIdentifier, ApiCredentials? credentials, ToobitEnvironment? environment)
        {
            var clientSocketOptions = SetSocketEnvironment(environment);
            var client = new ToobitSocketClient(clientSocketOptions!, _loggerFactory);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _socketClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IOptions<ToobitRestOptions> SetRestEnvironment(ToobitEnvironment? environment)
        {
            if (environment == null)
                return _restOptions;

            var newRestClientOptions = new ToobitRestOptions();
            var restOptions = _restOptions.Value.Set(newRestClientOptions);
            newRestClientOptions.Environment = environment;
            return Options.Create(newRestClientOptions);
        }

        private IOptions<ToobitSocketOptions> SetSocketEnvironment(ToobitEnvironment? environment)
        {
            if (environment == null)
                return _socketOptions;

            var newSocketClientOptions = new ToobitSocketOptions();
            var restOptions = _socketOptions.Value.Set(newSocketClientOptions);
            newSocketClientOptions.Environment = environment;
            return Options.Create(newSocketClientOptions);
        }

        private static T ApplyOptionsDelegate<T>(Action<T>? del) where T : new()
        {
            var opts = new T();
            del?.Invoke(opts);
            return opts;
        }
    }
}
