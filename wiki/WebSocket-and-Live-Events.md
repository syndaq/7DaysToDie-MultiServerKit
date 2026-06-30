# WebSocket and Live Events

The mod broadcasts live game events over WebSocket for console telnet and panel aggregation.

## Endpoints

| | Value |
|---|-------|
| Path | `/ws` |
| Default port | **8889** (separate from REST on **8888**) |

## Authentication (ApiOnly mode)

When `ApiOnly` is `true`, connect with the panel API key — no OAuth ticket required:

```
ws://game-host:8889/ws?apiKey=YOUR_PANEL_API_KEY
```

Or send header `X-Api-Key: YOUR_PANEL_API_KEY` on the upgrade request.

## Event format

Messages are JSON with this shape:

```json
{
  "modEventType": "ChatMessage",
  "data": { }
}
```

`modEventType` is a string enum. Common types:

| Event | Description |
|-------|-------------|
| `Welcome` | Connection established |
| `ChatMessage` | In-game chat |
| `PlayerLogin` | Player joined |
| `PlayerDisconnected` | Player left |
| `LogCallback` | Server log line |
| `EntityKilled` | Entity death |
| `GameStartDone` | World loaded |
| `CommandExecutionReply` | Console command output |

## Panel aggregation

The panel opens an upstream WebSocket to each registered server and fans events into `GET /api/ws` for authenticated browser clients. The dashboard shows a live event feed tagged with server name.

Ensure the panel host can reach game servers on port **8889**.

## Telnet / console

The same WebSocket endpoint supports remote console interaction when used with the panel's console page or compatible clients.

## Security

Do not expose port **8889** to the public internet. Restrict access to the panel host only.
