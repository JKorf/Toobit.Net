using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using System.Collections.Generic;
using Toobit.Net.Objects.Options;

namespace Toobit.Net
{
    internal class ToobitAuthenticationProvider : AuthenticationProvider<ToobitCredentials, ToobitCredentials>
    {
        public ToobitAuthenticationProvider(ToobitCredentials credentials) : base(credentials, credentials)
        {
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            request.Headers ??= new Dictionary<string, string>();
            request.Headers.Add("X-BB-APIKEY", Credential.Key);

            if (!request.RequestDefinition.Authenticated)
                return;

            var timestamp = GetMillisecondTimestampLong(apiClient);
            var receiveWindow = ((ToobitRestOptions)apiClient.ClientOptions).ReceiveWindow.TotalMilliseconds;

            request.QueryParameters ??= new Parameters(ToobitExchange._parameterSerializationSettings);
            request.QueryParameters["timestamp"] = timestamp;
            request.QueryParameters["recvWindow"] = receiveWindow;

            var queryString = request.GetQueryString();
            var body = request.BodyFormat == RequestBodyFormat.FormData ? (request.BodyParameters?.ToFormData() ?? string.Empty) : "";
            var signString = $"{queryString}{body}";
            var signature = SignHMACSHA256(signString).ToLowerInvariant();

            request.QueryParameters["signature"] = signature;
            request.SetQueryString($"{queryString}{(!string.IsNullOrEmpty(queryString) ? "&" : "")}signature={signature}");
            if (request.BodyFormat == RequestBodyFormat.FormData)
                request.SetBodyContent(body);
        }
    }
}
