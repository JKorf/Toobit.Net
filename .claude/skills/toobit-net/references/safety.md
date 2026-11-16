# Toobit operational considerations

## Credentials and private streams

Use HMAC key/secret from secret storage. Match permissions to public/private reads, trading or funds movement. Credentials do not imply every account/product is enabled. User streams are product-specific: Spot and USDT Futures have their own account start/keepalive/stop methods. Credential-based subscription overloads manage keys internally; do not add another refresh loop. With explicit listen keys, own renewal and stop only keys no longer used by subscriptions.

## Execution and nested results

Spot `PlaceTestOrderAsync` validates parameters without placing a live order. It does not reserve balances or guarantee subsequent acceptance. Spot market-buy quantity is quote value; check the selected order type's sizing semantics. A limit order can execute immediately and cancellation can race with fills. Inspect executed quantity and user trades before concluding exposure is gone.

Batch placement/cancellation can contain per-item failures despite outer success. Check every item and record successful IDs plus failures; do not repeat an entire batch based on one failed entry. Cancel-all affects its specified symbol/side scope and may remove orders unrelated to a single example.

## USDT Futures exposure

`BuyOpen`/`SellOpen` open exposure; `SellClose` closes long and `BuyClose` closes short. Read current positions and side before deriving a close. Native orders accept decimal contract volume and use a contract multiplier. The Shared adapter converts contract quantity to `long`; reject fractional input for that adapter instead of relying on truncation. Do not send base units as contracts. Use the correct contract symbol and index symbol for the relevant endpoint.

Leverage, margin type, position margin and trading-stop changes alter live risk. Use `FuturesNewOrderType` for placement and the required `FuturesOrderType` for order query/cancel paths; normal and conditional orders have distinct behavior. Do not import another exchange's hedge-mode or reduce-only parameters. Submission success does not establish that a position opened or closed.

## Transfers and withdrawals

Transfers specify source/destination UID and account type, asset and amount. Withdrawals require the intended network, address, tag/memo, client identifier and applicable VASP/recipient information. Preserve the requested destination details and inspect histories/status to reconcile progress. These account changes are distinct from balance reads or subaccount discovery.

## Uncertain submissions and retries

A timeout or connection loss can leave an order, cancellation, transfer, withdrawal or leverage/margin change accepted but unconfirmed. `Error.IsTransient` alone does not justify repeating it. Reconcile retained order/client IDs, trades, balances, transaction history or current position settings before deciding whether to resubmit; allow for propagation delays and partial batch acceptance. Bounded backoff is appropriate for transient public reads; keep generic read-retry helpers away from account changes.
