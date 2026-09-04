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

        #region Get Withdrawal History

        async Task<ICallResult<SharedWithdrawal[]>> IGetWithdrawalHistory.GetWithdrawalHistoryAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetWithdrawalHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        Task<HttpResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetWithdrawalHistoryAsync(request, pageRequest, ct);
        GetWithdrawalHistoryOptions IWithdrawalRestClient.GetWithdrawalsOptions => GetWithdrawalHistoryOptions;

        public GetWithdrawalHistoryOptions GetWithdrawalHistoryOptions { get; } = new GetWithdrawalHistoryOptions(_exchangeName, false, true, true, 1000);
        public async Task<HttpResult<SharedWithdrawal[]>> GetWithdrawalHistoryAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetWithdrawalHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedWithdrawal[]>(Exchange, validationError);

            // Determine page token
            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await _api.Account.GetWithdrawalsAsync(
                request.Asset,
                fromId: pageParams.FromId,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedWithdrawal[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                          () => Pagination.NextPageFromId(result.Data.Min(x => long.Parse(x.Id))),
                          result.Data.Length,
                          result.Data.Select(x => x.Timestamp),
                          request.StartTime,
                          request.EndTime ?? DateTime.UtcNow,
                          pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                .Select(x =>
                    new SharedWithdrawal(
                        x.AssetId, 
                        x.Address,
                        x.Quantity, 
                        true, 
                        x.Timestamp,
                        SharedTransferStatus.Unknown
                        )
                    {
                        Confirmations = x.ConfirmTimes,
                        Tag = x.Tag,
                        TransactionId = x.TransactionId,
                        Fee = x.Fee,
                        Id = x.Id
                    })
                .ToArray(), nextPageRequest);
        }

        #endregion

        #region Withdraw

        async Task<ICallResult<SharedId>> IWithdraw.WithdrawAsync(WithdrawRequest request, CancellationToken ct)
            => await WithdrawAsync(request, ct).ConfigureAwait(false);

        public WithdrawOptions WithdrawOptions { get; } = new WithdrawOptions(_exchangeName);
        public async Task<HttpResult<SharedId>> WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = WithdrawOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            // Get data
            var withdrawal = await _api.Account.WithdrawAsync(
                request.Asset,
                request.Address,
                request.Quantity,
                network: request.Network,
                tag: request.AddressTag,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal.Success)
                return HttpResult.Fail<SharedId>(withdrawal);

            return HttpResult.Ok(withdrawal, new SharedId(withdrawal.Data.Id));
        }

        #endregion

    }
}
