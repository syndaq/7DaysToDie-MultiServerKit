# 7DaysToDie-MultiServerKit

A mod for **7 Days to Die** dedicated servers that exposes a **REST API** for the central [MultiServerKit Panel](https://github.com/syndaq/7DaysToDie-MultiServerKit-Panel). This fork extends the original [7DaysToDie-ServerKit](https://github.com/IceCoffee1024/7DaysToDie-ServerKit) into a **multi-server** management platform.

## Vision

MultiServerKit manages multiple dedicated game servers from a central panel. Shared systems — shop, points, VIP, CD keys, level gifts, lottery, and player data — travel with players when they move between servers in the cluster.

**This repo is the game-server mod only.** It does not serve a web UI. Admins use the separate panel repository.

## Features

### API & infrastructure

- RESTful API for server administration (API-only mode by default)
- Panel API key authentication (`X-Api-Key` header)
- Optional Swagger UI at `/swagger` when `EnableSwagger` is true
- WebSocket telnet and live event broadcasting
- Harmony performance tuning patches
- Multi-language localization (English-first)

### Game systems (mod-direct or panel-synced)

| Feature | Scope | Notes |
|---------|--------|--------|
| Points system | Per-server settings; cluster points via panel | Sign-in, query, currency exchange |
| Game store | Cluster-wide via panel | Products synced to all servers |
| VIP gifts | Cluster-wide via panel | |
| CD key redeem | Cluster-wide via panel | |
| Level gifts | Cluster-wide via panel | Claim tracking per player |
| Lottery | Cluster-wide via panel | Weighted pools, draw commands |
| Teleport (home / city / friend) | Per-server | Settings + location CRUD |
| PVP/PVE areas | Per-server | Kill mode, drop-on-death, land-claim rules |
| Boss kill reward | Per-server | Points on boss zombie/animal kills |
| Chunk reset | Per-server | Reset map regions by coordinates |
| Mute commands | Per-server | Block chat commands with custom tip |
| Global settings | Per-server | Protection, triggers, auto-restart |
| Game notice | Per-server | Welcome + rotating + blood moon messages |
| Auto backup | Per-server | Scheduled archives + manual trigger |
| Task schedule | Per-server | Cron jobs bound to command list |
| Item / command lists | Per-server | Shared catalogs for gifts, shop, schedules |
| Prefab tools | Per-server | Browse, place, undo |
| Map | Per-server | Tile API, full/explored render |
| Permissions | Per-server | Admins, command permissions, ban/whitelist |
| Chat records | Per-server | Searchable log + global message API |
| Console | Per-server | Remote command execution |

## Requirements

### Backend

- [.NET SDK 8+](https://dotnet.microsoft.com/download) (builds `net48`)
- Visual Studio 2022 (Windows) or VS Code / Rider (Linux)
- **7 Days to Die** dedicated server game binaries (see below)

## Getting started

### 1. Clone the repository

```bash
git clone https://github.com/syndaq/7DaysToDie-MultiServerKit.git
cd 7DaysToDie-MultiServerKit
```

### 2. Provide game binaries

Copy the required DLLs from your 7DTD dedicated server installation into:

```
src/SdtdMultiServerKit/7dtd-binaries/
```

Required files include `Assembly-CSharp.dll`, `0Harmony.dll`, `UnityEngine.CoreModule.dll`, `LogLibrary.dll`, `MapRendering.dll`, `WebServer.dll`, and others referenced in `SdtdMultiServerKit.csproj`.

Also place `websocket-sharp.dll` in `src/SdtdMultiServerKit/3rdparty-binaries/`.

### 3. Build the mod

```bash
dotnet restore src/SdtdMultiServerKit/SdtdMultiServerKit.csproj
dotnet build src/SdtdMultiServerKit/SdtdMultiServerKit.csproj -c Release
```

### 4. Configure API-only mode

Edit `Config/appsettings.json` (copied to `LSTY_Data/appsettings.json` on first run):

```json
{
  "ApiOnly": true,
  "PanelApiKey": "your-long-random-secret",
  "ServerId": "us-pve-01",
  "EnableSwagger": false,
  "WebUrl": "http://127.0.0.1:8888"
}
```

| Setting | Description |
|---------|-------------|
| `ApiOnly` | `true` = API only, no web UI or OAuth (default) |
| `PanelApiKey` | Secret the central panel sends in `X-Api-Key` |
| `ServerId` | Unique ID for this server in the panel |
| `EnableSwagger` | Expose `/swagger` docs (dev only) |
| `WebUrl` | Bind address — use `127.0.0.1` or private IP |

Set `ApiOnly` to `false` only if you need the legacy embedded web panel.

> **Note:** `dotnet restore` may report `NU1903` for the transitive `SQLitePCLRaw.lib.e_sqlite3` 2.x package pulled in by `Microsoft.Data.Sqlite`. The project suppresses this warning until dependencies move to SQLitePCLRaw 3.x (`SourceGear.sqlite3`). Track upstream updates before overriding the native SQLite package manually.

### 5. Deploy

Copy the build output to your server's `Mods/SdtdMultiServerKit/` directory alongside `ModInfo.xml` and the `Config/` folder.

**Do not expose port 8888 to the public internet.** Only the central panel should reach this API (firewall / VPN).

### SQL migrations

On first startup (and after mod updates), the mod runs SQL files from `Config/sql/` in filename order. Current migrations:

| File | Purpose |
|------|---------|
| `1-` … `8-` | Core tables (items, commands, goods, points, VIP, CD keys, etc.) |
| `9-LevelGift.sql` | Level gift rewards and claim tracking |
| `10-PvpArea.sql` | PVP/PVE custom zones |
| `11-Lottery.sql` | Lottery pools, items, and commands |

Migrations are idempotent (`CREATE TABLE IF NOT EXISTS`). The SQLite database lives under the mod data folder after first run. Back up that database before major upgrades.

### Deployment checklist

Prefer a [GitHub Release](https://github.com/syndaq/7DaysToDie-MultiServerKit/releases) zip, or build locally — see [docs/RELEASE.md](docs/RELEASE.md).

1. Download `SdtdMultiServerKit-x.y.z.zip` from Releases **or** build Release (`dotnet build -c Release`).
2. Copy/extract into `Mods/SdtdMultiServerKit/` on the game server.
3. Set `ApiOnly`, `PanelApiKey`, `ServerId`, and `PanelUrl` in `Config/appsettings.json`.
4. Bind `WebUrl` to `127.0.0.1` or a private interface; allow only the panel host through the firewall.
5. Start the server and confirm the log shows SQL migrations and `Loaded N functions`.
6. Register the server in the [panel](https://github.com/syndaq/7DaysToDie-MultiServerKit-Panel) and confirm health checks pass.

### Panel authentication

```bash
curl -H "X-Api-Key: your-long-random-secret" http://127.0.0.1:8888/api/Server/Stats
```

## Project structure

```
7DaysToDie-MultiServerKit/
├── 7DaysToDie-MultiServerKit.sln
├── src/SdtdMultiServerKit/       # Main mod project (C# / net48)
│   ├── WebApi/                   # OWIN REST API + panel API key auth
│   ├── WebSockets/               # Telnet + broadcaster
│   ├── Data/                     # SQLite repositories
│   ├── Functions/                # Game features (store, points, teleport, etc.)
│   ├── HarmonyPatchers/          # PVP area enforcement, performance
│   ├── Config/                   # appsettings, SQL migrations, locales
│   └── ModInfo.xml
└── scripts/                      # Dev tooling
```

## Multi-server roadmap

- [x] API-only mode (no embedded panel on game servers)
- [x] Central panel repository ([7DaysToDie-MultiServerKit-Panel](https://github.com/syndaq/7DaysToDie-MultiServerKit-Panel))
- [x] Panel server registry and health monitoring
- [x] Cluster-wide shop, VIP, CD keys, level gifts, lottery, point log
- [x] Per-server admin UI coverage (console, permissions, map, teleport, etc.)
- [x] Shared player points fully cluster-native (panel is source of truth)
- [x] WebSocket aggregation from game servers in the panel
- [ ] Production hardening (CI, automated tests, soak validation)

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md).

## License

[MIT](LICENSE)

## Links

- Repository: https://github.com/syndaq/7DaysToDie-MultiServerKit
- **Releases:** https://github.com/syndaq/7DaysToDie-MultiServerKit/releases
- Panel: https://github.com/syndaq/7DaysToDie-MultiServerKit-Panel
- **Wiki:** https://github.com/syndaq/7DaysToDie-MultiServerKit/wiki ([source in `wiki/`](wiki/))
- Original project: https://github.com/IceCoffee1024/7DaysToDie-ServerKit

## Disclaimer
The source code of this project is open and transparent. Any disputes arising from or related to the use of this software should be resolved through friendly negotiation. 
Any private modifications to the code of this project are the sole responsibility of the person who made these modifications. The author team of this software does not assume any responsibility for any form of loss or damage that may be caused to the user or others during the use of this software.
If the user downloads, installs and uses this software, it means that the user trusts the author team of this software and agrees to the relevant agreements and disclaimers.
