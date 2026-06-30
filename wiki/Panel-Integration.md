# Panel Integration

This mod is designed to work with the [MultiServerKit Panel](https://github.com/syndaq/7DaysToDie-MultiServerKit-Panel).

## Deployment checklist

1. Build Release (`dotnet build -c Release`).
2. Copy output to `Mods/SdtdMultiServerKit/` on the game server.
3. Configure [[Configuration|appsettings.json]]: `ApiOnly`, `PanelApiKey`, `ServerId`, `PanelUrl`.
4. Bind `WebUrl` to `127.0.0.1` or a private interface.
5. Allow the panel host through the firewall on ports **8888** (REST) and **8889** (WebSocket).
6. Start the game server and confirm health in the panel dashboard.

## Register in the panel

In the panel **Servers** page, add:

| Panel field | Mod setting |
|-------------|-------------|
| Server ID | `ServerId` |
| API URL | `WebUrl` host reachable from panel (e.g. `http://10.0.0.5:8888`) |
| API key | `PanelApiKey` |

All three values must match exactly.

## What the panel controls

| Scope | Examples |
|-------|----------|
| **Cluster-wide** (PostgreSQL + sync) | Points, shop, VIP gifts, CD keys, level gifts, lottery, point log |
| **Per-server** (panel proxies to this mod) | Console, permissions, map, teleport, PVP areas, backups, global settings |

The panel never exposes mod API keys to browsers. All game-server calls go through `/api/servers/:id/mod/*` on the panel API.

## Cluster points

Set `PanelUrl` to the panel's base URL so in-game point changes sync to PostgreSQL. See [[Cluster Points]].

## Live events

The panel connects to this server's WebSocket on port **8889** and aggregates events on the dashboard. See [[WebSocket and Live Events]].
