# Cluster Points

When `PanelUrl` is set in `appsettings.json`, the **panel PostgreSQL database** is the source of truth for player points. The mod keeps a local SQLite cache for fast in-game reads.

## How it works

```
In-game action (sign-in, shop, lottery, …)
    → mod ChangePointsAsync
    → POST panel /api/points/ingest
    → PostgreSQL updated
    → mod local SQLite cache updated

Player login
    → GET panel /api/points/by-platform/:platformId
    → local cache refreshed

Panel admin edits points
    → PUT mod /api/PointsInfo/:id (existing cluster sync)
    → local cache updated (no loop back to panel)
```

## Mod configuration

```json
{
  "PanelUrl": "http://panel-host:80",
  "PanelApiKey": "same-key-as-panel-server-registry",
  "ServerId": "server-01"
}
```

The mod sends these headers on ingest requests:

- `X-Api-Key` — `PanelApiKey`
- `X-Server-Id` — `ServerId`

## Panel configuration

1. Register the server in the panel with matching `ServerId` and `PanelApiKey`.
2. Enable point log in **Point log** settings if you want audit entries for ingest events.

## Supported in-game paths

Point changes that go through `ChangePointsAsync` are cluster-aware, including:

- Daily sign-in
- Shop purchases (when using the points-aware store flow)
- Lottery draws
- Panel-initiated balance sync

## Known limitations

Direct point mutations that bypass `ChangePointsAsync` (e.g. some admin commands or raw repository writes) may not sync to the panel. Prefer panel admin UI or the standard in-game commands for balance changes.

## Without PanelUrl

If `PanelUrl` is empty, points behave as before: stored only in the mod's local SQLite database on this server.
