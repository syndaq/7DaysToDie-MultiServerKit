# Configuration

Edit `Config/appsettings.json` before first deploy (or `LSTY_Data/appsettings.json` after first run).

## Example (API-only + panel cluster)

```json
{
  "ApiOnly": true,
  "PanelApiKey": "your-long-random-secret",
  "ServerId": "us-pve-01",
  "PanelUrl": "http://panel-host:80",
  "EnableSwagger": false,
  "WebUrl": "http://127.0.0.1:8888",
  "WebSocketPort": 8889
}
```

## Settings reference

| Setting | Description |
|---------|-------------|
| `ApiOnly` | `true` = REST API only, no embedded web UI or OAuth (recommended) |
| `PanelApiKey` | Secret the panel sends in `X-Api-Key` on REST and WebSocket |
| `ServerId` | Unique cluster identifier — must match the panel server registry |
| `PanelUrl` | Base URL of the panel API (e.g. `http://panel-host:80`). When set, player points are stored in PostgreSQL and cached locally in SQLite |
| `EnableSwagger` | Expose `/swagger` docs — **dev only** |
| `WebUrl` | REST API bind address — use `127.0.0.1` or a private IP |
| `WebSocketPort` | WebSocket port (default **8889**) |
| `DatabasePath` | SQLite path relative to mod data folder |

Set `ApiOnly` to `false` only if you need the legacy embedded web panel.

## Test REST authentication

```bash
curl -H "X-Api-Key: your-long-random-secret" http://127.0.0.1:8888/api/Server/Stats
```

## Cluster points

When `PanelUrl` is set, see [[Cluster Points]] for how balances flow between the mod and panel.

## WebSocket

See [[WebSocket and Live Events]] for port **8889** authentication and event types.
