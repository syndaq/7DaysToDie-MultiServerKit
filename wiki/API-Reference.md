# API Reference

REST API is served on the port configured in `WebUrl` (default **8888**).

## Authentication

All REST requests require the panel API key:

```
X-Api-Key: your-long-random-secret
```

## Health check

```bash
curl -H "X-Api-Key: YOUR_KEY" http://127.0.0.1:8888/api/Server/Stats
```

## Swagger (development)

Set `EnableSwagger: true` in `appsettings.json` to expose interactive docs at `/swagger`. **Disable in production.**

## Major API areas

| Area | Path prefix | Scope |
|------|-------------|-------|
| Server | `/api/Server` | Stats, info |
| Points | `/api/PointsInfo` | Player balances (local cache; cluster when `PanelUrl` set) |
| Game store | `/api/GameStore` | Shop products and settings |
| VIP gifts | `/api/VipGift` | VIP reward definitions |
| CD keys | `/api/CdKey` | Redemption codes |
| Level gifts | `/api/LevelGift` | Level milestone rewards |
| Lottery | `/api/Lottery` | Pools and draw settings |
| Teleport | `/api/Teleport*` | Home, city, friend |
| PVP areas | `/api/PvpArea` | Zone management |
| Permissions | `/api/Admin`, `/api/Permission`, … | Admins, bans, whitelist |
| Map | `/api/Map` | Tiles and render |
| Chat | `/api/Chat` | Records and global send |
| Console | `/api/Console` | Remote commands |
| Backup | `/api/AutoBackup` | Schedules and archives |

The panel proxies these endpoints at `/api/servers/:id/mod/*` so browsers never hold mod API keys.

## Panel → mod points sync

When the panel updates a player's balance, it calls:

```
PUT /api/PointsInfo/{platformId}
```

The mod applies this as a local cache write without re-ingesting to the panel (loop prevention).

## Panel ingest (mod → panel)

When `PanelUrl` is set, the mod calls the panel directly:

```
POST {PanelUrl}/api/points/ingest
GET  {PanelUrl}/api/points/by-platform/{platformId}
```

See [[Cluster Points]] for details.
