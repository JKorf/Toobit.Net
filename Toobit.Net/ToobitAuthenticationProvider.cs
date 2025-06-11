using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Toobit.Net.Objects.Options;

namespace Toobit.Net
{
    internal class ToobitAuthenticationProvider : AuthenticationProvider
    {
        private static IMessageSerializer _serializer = new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(ToobitExchange._serializerContext));

        public ToobitAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }

        public override void AuthenticateRequest(
            RestApiClient apiClient,
            Uri uri,
            HttpMethod method,
            ref IDictionary<string, object>? uriParameters,
            ref IDictionary<string, object>? bodyParameters,
            ref Dictionary<string, string>? headers,
            bool auth,
            ArrayParametersSerialization arraySerialization,
            HttpMethodParameterPosition parameterPosition,
            RequestBodyFormat requestBodyFormat)
        {
            headers ??= new Dictionary<string, string>();
            headers.Add("X-BB-APIKEY", ApiKey);

            if (!auth)
                return;

            IDictionary<string, object> parameters;
            if (parameterPosition == HttpMethodParameterPosition.InUri)
            {
                uriParameters ??= new Dictionary<string, object>();
                parameters = uriParameters;
            }
            else
            {
                if (requestBodyFormat != RequestBodyFormat.Json)
                {
                    // When the body is json (batch endpoint) use uri
                    parameters = uriParameters ??= new Dictionary<string, object>();
                }
                else
                {
                    bodyParameters ??= new Dictionary<string, object>();
                    parameters = bodyParameters;
                }
            }

            var timestamp = long.Parse(GetMillisecondTimestamp(apiClient)) - 1000;
            parameters.Add("timestamp", timestamp);
            parameters.Add("recvWindow", ((ToobitRestOptions)apiClient.ClientOptions).ReceiveWindow.TotalMilliseconds);

            if (uriParameters != null)
                uri = uri.SetParameters(uriParameters, arraySerialization);

            var parameterData = uri.Query.Replace("?", "");
            if (requestBodyFormat != RequestBodyFormat.Json)
                parameterData += requestBodyFormat == RequestBodyFormat.FormData ? bodyParameters?.ToFormData() : GetSerializedBody(_serializer, bodyParameters ?? new Dictionary<string, object>());
            
            parameters.Add("signature", SignHMACSHA256(parameterData).ToLowerInvariant());            
        }
    }
}
