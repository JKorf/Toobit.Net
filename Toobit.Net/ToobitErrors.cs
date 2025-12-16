using CryptoExchange.Net.Objects.Errors;

namespace Toobit.Net
{
    internal static class ToobitErrors
    {
        public static ErrorMapping Errors { get; } = new ErrorMapping([

            new ErrorInfo(ErrorType.SystemError, true, "Internal error", "-1001"),

            new ErrorInfo(ErrorType.Unauthorized, false, "Unauthorized", "-1002"),
            new ErrorInfo(ErrorType.Unauthorized, false, "API key invalid", "-2014"),
            new ErrorInfo(ErrorType.Unauthorized, false, "API key unauthorized", "-2015"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Invalid signature", "-1022"),

            new ErrorInfo(ErrorType.InvalidTimestamp, false, "Invalid timestamp", "-1021"),

            new ErrorInfo(ErrorType.Timeout, false, "Internal timeout", "-1007"),
            new ErrorInfo(ErrorType.Timeout, false, "Order creation timeout", "-1146"),
            new ErrorInfo(ErrorType.Timeout, false, "Order cancellation timeout", "-1147"),

            new ErrorInfo(ErrorType.RateLimitRequest, false, "Too many requests", "-1003"),

            new ErrorInfo(ErrorType.RateLimitOrder, false, "Too many open orders", "-1015", "-1193"),

            new ErrorInfo(ErrorType.UnknownSymbol, false, "Unknown symbol", "-100010", "-100011", "-1121"),

            new ErrorInfo(ErrorType.UnknownOrder, false, "Order not found in order book", "-1143"),
            new ErrorInfo(ErrorType.UnknownOrder, false, "Order does not exist", "-2013"),

            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid parameter value", "-1100", "-1130"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Too many parameters", "-1101"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Unknown parameter", "-1103"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Not all parameters read", "-1104"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter sent but not needed", "-1106"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid decimal precision", "-1111"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "TimeInForce error", "-1114", "-1115"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid order type", "-1116"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid side", "-1117"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid time range", "-1127"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid parameter combination", "-1128"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid listen key", "-1125"),

            new ErrorInfo(ErrorType.MissingParameter, false, "Parameter missing or invalid", "-1102"),
            new ErrorInfo(ErrorType.MissingParameter, false, "Parameter empty", "-1105"),

            new ErrorInfo(ErrorType.InvalidPrice, false, "Order price too high", "-1132", "-1196", "-1197"),
            new ErrorInfo(ErrorType.InvalidPrice, false, "Order price too low", "-1133", "-1195", "-1198"),
            new ErrorInfo(ErrorType.InvalidPrice, false, "Order price outside allowed range", "-1138"),
            new ErrorInfo(ErrorType.InvalidPrice, false, "Order price decimal precision invalid", "-1134"),

            new ErrorInfo(ErrorType.InvalidQuantity, false, "Order quantity too large", "-1135", "-1200", "-1201", "-1203"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Order quantity too small", "-1136", "-1199", "-1202"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Order quantity decimal precision invalid", "-1137"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Order value too low", "-1140", "-1206"),

            new ErrorInfo(ErrorType.DuplicateClientOrderId, false, "Duplicate client order id", "-1141"),

            new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient balance", "-1131"),
            new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient margin available", "-3130"),

            new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "Invalid order", "-1014"),
            new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "Market order not allowed", "-1194"),

            new ErrorInfo(ErrorType.IncorrectState, false, "Order has filled", "-1139"),
            new ErrorInfo(ErrorType.IncorrectState, false, "Order has been canceled", "-1142"),
            new ErrorInfo(ErrorType.IncorrectState, false, "Order has been locked", "-1144"),
            new ErrorInfo(ErrorType.IncorrectState, false, "Order does not allow cancellation", "-1145"),

            ]);
    }
}
