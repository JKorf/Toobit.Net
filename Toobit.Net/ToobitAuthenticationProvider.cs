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
    internal class ToobitAuthenticationProvider : AuthenticationProvider
    {
        private static IMessageSerializer _serializer = new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(ToobitExchange._serializerContext));

        public ToobitAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            request.Headers ??= new Dictionary<string, string>();
            request.Headers.Add("X-BB-APIKEY", ApiKey);

            if (!request.Authenticated)
                return;

            var timestamp = GetMillisecondTimestampLong(apiClient) - 1000;
            var receiveWindow = ((ToobitRestOptions)apiClient.ClientOptions).ReceiveWindow.TotalMilliseconds;

            request.QueryParameters ??= new Dictionary<string, object>();
            request.QueryParameters.Add("timestamp", timestamp);
            request.QueryParameters.Add("recvWindow", receiveWindow);

            var queryString = request.GetQueryString();
            var body = request.BodyFormat == RequestBodyFormat.FormData ? (request.BodyParameters?.ToFormData() ?? string.Empty) : "";
            var signString = $"{queryString}{body}";
            var signature = SignHMACSHA256(signString).ToLowerInvariant();

            request.QueryParameters.Add("signature", signature);
            request.SetQueryString($"{queryString}{(!string.IsNullOrEmpty(queryString) ? "&" : "")}signature={signature}");
            if (request.BodyFormat == RequestBodyFormat.FormData)
                request.SetBodyContent(body);
        }
    }
}
