using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Linq;
using System.Text.Json;
using Toobit.Net;
using Toobit.Net.Objects.Models;
using Toobit.Net.Objects.Sockets;

namespace Mexc.Net.Clients.MessageHandlers
{
    internal class ToobitSocketFuturesMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(ToobitExchange._serializerContext);

        public ToobitSocketFuturesMessageHandler()
        {
            AddTopicMapping<SocketUpdate<ToobitKlineUpdate[]>>(x => x.Symbol + x.Parameters["klineType"]);
            AddTopicMapping<SocketUpdate>(x => x.Symbol);
        }

        protected override MessageEvaluator[] TypeEvaluators { get; } = [ 
            new MessageEvaluator {
                Priority = 1,
                Fields = [
                    new PropertyFieldReference("code"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("code")!
            },

             new MessageEvaluator {
                Priority = 2,
                Fields = [
                    new PropertyFieldReference("topic"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("topic")!
            },

            new MessageEvaluator {
                Priority = 3,
                Fields = [
                    new PropertyFieldReference("e") { Depth = 2 },
                ],
                IdentifyMessageCallback = x => x.FieldValue("e")!
            },

            new MessageEvaluator {
                Priority = 4,
                Fields = [
                    new PropertyFieldReference("pong"),
                ],
                StaticIdentifier = "pong"
            },

            new MessageEvaluator {
                Priority = 5,
                Fields = [
                    new PropertyFieldReference("ping"),
                ],
                StaticIdentifier = "ping"
            }
        ];
    }
}
