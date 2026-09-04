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
        #region Get All Assets

        async Task<ICallResult<SharedAsset[]>> IGetAllAssets.GetAllAssetsAsync(GetAssetsRequest request, CancellationToken ct)
            => await GetAllAssetsAsync(request, ct).ConfigureAwait(false);

        Task<HttpResult<SharedAsset[]>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
            => GetAllAssetsAsync(request, ct);
        GetAllAssetsOptions IAssetsRestClient.GetAssetsOptions => GetAllAssetsOptions;

        public GetAllAssetsOptions GetAllAssetsOptions { get; } = new GetAllAssetsOptions(_exchangeName, false);

        public async Task<HttpResult<SharedAsset[]>> GetAllAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = GetAllAssetsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset[]>(Exchange, validationError);

            var assets = await _api.ExchangeData.GetExchangeInfoAsync(ct: ct).ConfigureAwait(false);
            if (!assets.Success)
                return HttpResult.Fail<SharedAsset[]>(assets);

            return HttpResult.Ok(assets, assets.Data.Assets.Select(x => new SharedAsset(x.AssetName)
            {
                FullName = x.FullName,
                Networks = x.Networks.Select(x => new SharedAssetNetwork(x.NetworkType)
                {
                    DepositEnabled = x.AllowDeposit,
                    MinWithdrawQuantity = x.MinWithdrawQuantity,
                    MaxWithdrawQuantity = x.MaxWithdrawQuantity,
                    WithdrawEnabled = x.AllowWithdraw,
                    WithdrawFee = x.WithdrawFee
                }).ToArray()
            }).ToArray());
        }

        #endregion

        #region Get Asset

        async Task<ICallResult<SharedAsset>> IGetAsset.GetAssetAsync(GetAssetRequest request, CancellationToken ct)
            => await GetAssetAsync(request, ct).ConfigureAwait(false);

        public GetAssetOptions GetAssetOptions { get; } = new GetAssetOptions(_exchangeName, false);
        public async Task<HttpResult<SharedAsset>> GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            var validationError = GetAssetOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset>(Exchange, validationError);

            var assets = await _api.ExchangeData.GetExchangeInfoAsync(ct: ct).ConfigureAwait(false);
            if (!assets.Success)
                return HttpResult.Fail<SharedAsset>(assets);

            var asset = assets.Data.Assets.SingleOrDefault(x => x.AssetName.Equals(request.Asset, StringComparison.InvariantCultureIgnoreCase));
            if (asset == null)
                return HttpResult.Fail<SharedAsset>(assets, new ServerError(new ErrorInfo(ErrorType.UnknownAsset, "Asset not found")));

            return HttpResult.Ok(assets, new SharedAsset(asset.AssetName)
            {
                FullName = asset.FullName,
                Networks = asset.Networks.Select(x => new SharedAssetNetwork(x.NetworkType)
                {
                    DepositEnabled = x.AllowDeposit,
                    MinWithdrawQuantity = x.MinWithdrawQuantity,
                    MaxWithdrawQuantity = x.MaxWithdrawQuantity,
                    WithdrawEnabled = x.AllowWithdraw,
                    WithdrawFee = x.WithdrawFee
                }).ToArray()
            });
        }

        #endregion

    }
}
