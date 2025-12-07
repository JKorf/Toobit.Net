using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Linq;
using System.Text.Json;
using Toobit.Net;
using Toobit.Net.Objects.Models;
using Toobit.Net.Objects.Sockets;

namespace Toobit.Net.Clients.MessageHandlers
{
    internal class ToobitSocketFuturesMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(ToobitExchange._serializerContext);

        public ToobitSocketFuturesMessageHandler()
        {
            AddTopicMapping<SocketUpdate<ToobitKlineUpdate[]>>(x => x.Symbol + x.Parameters["klineType"]);
            AddTopicMapping<SocketUpdate>(x => x.Symbol);
        }

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [ 
            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("code"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("code")!
            },

             new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("topic"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("topic")!
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("e") { Depth = 2 },
                ],
                TypeIdentifierCallback = x => x.FieldValue("e")!
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("pong"),
                ],
                StaticIdentifier = "pong"
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("ping"),
                ],
                StaticIdentifier = "ping"
            }
        ];
    }
}
