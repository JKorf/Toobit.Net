using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Toobit.Net.Enums;
using Toobit.Net.Interfaces.Clients.UsdtFuturesApi;
using Toobit.Net.Objects.Models;

namespace Toobit.Net.Clients.UsdtFuturesApi
{
    internal partial class ToobitRestClientUsdtFuturesSharedApi
    {

        #region Get Futures Symbols

        async Task<ICallResult<SharedFuturesSymbol[]>> IGetFuturesSymbols.GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
            => await GetFuturesSymbolsAsync(request, ct).ConfigureAwait(false);

        public SharedSymbolCatalog? FuturesSymbolCatalog => ExchangeSymbolCache.GetSymbolCatalog(_exchangeName, _topicId, _api.EnvironmentName, null);
        public GetFuturesSymbolsOptions GetFuturesSymbolsOptions { get; } = new GetFuturesSymbolsOptions(_exchangeName, false);
        public async Task<HttpResult<SharedFuturesSymbol[]>> GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesSymbolsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesSymbol[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetExchangeInfoAsync(ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesSymbol[]>(result);

            var resultData =
                 result.Data.ContractSymbols.Select(x => ParseSymbol(x))
                .ToArray();

            ExchangeSymbolCache.UpdateSymbolInfo(_topicId, _api.EnvironmentName, null, resultData);
            return HttpResult.Ok(result, SharedUtils.ApplySymbolFilter(resultData, request));
        }

        #endregion

        private SharedFuturesSymbol ParseSymbol(ToobitFuturesSymbol s)
        {
            var result = new SharedFuturesSymbol(TradingMode.PerpetualLinear,
                s.Underlying ?? s.BaseAsset,
                s.QuoteAsset,
                s.Symbol,
                s.Status == SymbolStatus.Trading)
            {
                MinTradeQuantity = s.LotSizeFilter?.MinQuantity / s.ContractMultiplier,
                MaxTradeQuantity = s.LotSizeFilter?.MaxQuantity / s.ContractMultiplier,
                QuantityStep = s.LotSizeFilter?.StepSize / s.ContractMultiplier,
                PriceStep = s.PriceFilter?.TickSize,
                MinNotionalValue = s.MinNotionalFilter?.MinNotional,
                ContractSize = s.ContractMultiplier,
                DisplayName = s.SymbolName,
                QuoteAssetType = SharedAssetType.Crypto,
                QuoteAssetSubType = SharedAssetSubType.StableCoin
            };

            if (!s.IsRwa)
            {
                if (s.Categories.Contains("TradFi"))
                {
                    result.BaseAssetType = SharedAssetType.TradFi;
                    result.BaseAssetSubType = SharedAssetSubType.Commodity;
                }
                else
                {
                    result.BaseAssetType = SharedAssetType.Crypto;
                }
            }
            else
            {
                if (_fiatCurrencies.Contains(result.BaseAsset))
                {
                    result.BaseAssetType = SharedAssetType.Fiat;
                }
                else
                {
                    result.BaseAssetType = SharedAssetType.TradFi;
                    if (LibraryHelpers.IsCommodity(result.BaseAsset))
                        result.BaseAssetSubType = SharedAssetSubType.Commodity;
                    else
                        result.BaseAssetSubType = SharedAssetSubType.Equity;
                }
            }

            return result;
        }

        public async Task<ExchangeCallResult<SharedSymbol[]>> GetFuturesSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<SharedSymbol[]>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<SharedSymbol[]>.Ok(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicId, _api.EnvironmentName, null, baseAsset));
        }

        public async Task<ExchangeCallResult<bool>> SupportsFuturesSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode == TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Spot symbols not allowed");

            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, _api.EnvironmentName, null, symbol));
        }

        public async Task<ExchangeCallResult<bool>> SupportsFuturesSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, _api.EnvironmentName, null, symbolName));
        }
    }
}
