# AI-Friendly Examples

These examples are optimized for AI coding assistants and quick onboarding. Each file is:

- **Compilable**: drop into a console project with `dotnet add package Toobit.Net` and it builds.
- **Self-contained**: single file, no external setup, no shared helpers.
- **Commented**: explains why the Toobit.Net pattern matters.
- **Idiomatic**: follows the current Toobit.Net client shape.

## Files

| File | What it shows |
|---|---|
| `01-spot-quickstart.cs` | Client setup, public ticker, authenticated balances, place limit order, query order status |
| `02-futures.cs` | USDT futures: set leverage, place limit order, get position, close order pattern |
| `03-websocket.cs` | Subscribe to ticker updates, klines, user data stream, with proper teardown |
| `04-multi-exchange.cs` | `CryptoExchange.Net.SharedApis` pattern for exchange-agnostic code |
| `05-error-handling.cs` | `WebCallResult` patterns, retry, common error scenarios |

## Running

```bash
dotnet new console -n MyToobitApp
cd MyToobitApp
dotnet add package Toobit.Net
# Copy the example .cs file content into Program.cs
# Replace API_KEY / API_SECRET placeholders for private endpoints
dotnet run
```
