# Toobit.Net examples

Each file is an independent console program. Install `Toobit.Net` in a console project and copy one program to `Program.cs`.

| Program | Workflow |
|---|---|
| [01-spot-quickstart.cs](01-spot-quickstart.cs) | Spot data, accounts and order workflow |
| [02-futures.cs](02-futures.cs) | USDT Futures leverage, orders and positions |
| [03-websocket.cs](03-websocket.cs) | Public/private streams and cleanup |
| [04-multi-exchange.cs](04-multi-exchange.cs) | Shared API V2 ticker, catalog and subscription |
| [05-error-handling.cs](05-error-handling.cs) | Results, test-order handling and public-read retries |

Replace key/secret placeholders for intended private operations. Review each program before execution: trading and leverage changes affect live account state; cancellation can race with fills. Read [operational considerations](../../.claude/skills/toobit-net/references/safety.md) and the [usage snippets](../../.claude/skills/toobit-net/references/usage.md) first.

`AiExampleCompileTests` compiles these programs without executing exchange operations.
