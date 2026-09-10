using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Toobit.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects;
using System.Linq;
using Toobit.Net.Enums;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects.Errors;
using Toobit.Net.Objects.Models;

namespace Toobit.Net.Clients.SpotApi
{
    internal partial class ToobitRestClientSpotSharedApi
    {

        #region Transfer

        async Task<ICallResult<SharedId>> ITransfer.TransferAsync(TransferRequest request, CancellationToken ct)
            => await TransferAsync(request, ct).ConfigureAwait(false);

        public TransferOptions TransferOptions { get; } = new TransferOptions(_exchangeName, [
            SharedAccountType.Spot,
            SharedAccountType.PerpetualLinearFutures,
            SharedAccountType.PerpetualInverseFutures,
            SharedAccountType.DeliveryLinearFutures,
            SharedAccountType.DeliveryInverseFutures,
            ])
        {
            ParameterRuleOverrides = [
                RequestParameterRuleOverride<TransferRequest>.NotSupported(x => x.FromSymbol),
                RequestParameterRuleOverride<TransferRequest>.NotSupported(x => x.ToSymbol),
                ],
            ExchangeParameterRules = [
                ExchangeParameterRule.Required("AccountId", "User account id", 123123L)
            ]
        };
        public async Task<HttpResult<SharedId>> TransferAsync(TransferRequest request, CancellationToken ct)
        {
            var validationError = TransferOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var fromType = GetTransferType(request.FromAccountType);
            var toType = GetTransferType(request.ToAccountType);
            if (fromType == null || toType == null)
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid("To/From AccountType", "invalid to/from account combination"));

            var userId = ExchangeParameters.GetValue<long>(request.ExchangeParameters, Exchange, "AccountId");

            // Get data
            var transfer = await _api.Account.TransferAsync(
                userId,
                userId,
                fromType.Value,
                toType.Value,
                request.Asset,
                request.Quantity,
                ct: ct).ConfigureAwait(false);
            if (!transfer.Success)
                return HttpResult.Fail<SharedId>(transfer);

            return HttpResult.Ok(transfer, new SharedId(""));
        }

        #endregion

        private AccountType? GetTransferType(SharedAccountType type)
        {
            if (type == SharedAccountType.Spot) return AccountType.Spot;
            if (type.IsFuturesAccount()) return AccountType.Futures;
            return null;
        }

    }
}
