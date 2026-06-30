# 7DaysToDie-MultiServerKit

A mod for **7 Days to Die** dedicated servers that exposes a **REST API** for the central [MultiServerKit Panel](https://github.com/syndaq/7DaysToDie-MultiServerKit-Panel). This fork extends the original [7DaysToDie-ServerKit](https://github.com/IceCoffee1024/7DaysToDie-ServerKit) into a **multi-server** management platform.

## Vision

MultiServerKit manages multiple dedicated game servers from a central panel. Shared systems — shop, points, VIP, CD keys, and player data — travel with players when they move between servers in the cluster.

**This repo is the game-server mod only.** It does not serve a web UI. Admins use the separate panel repository.

## Features (current)

- RESTful API for server administration (API-only mode by default)
- Panel API key authentication (`X-Api-Key` header)
- Points system, game store, VIP gifts, and CD key redemption (moving to panel)
- Teleport (home, city, friend), colored chat, auto-backup, and task scheduling
- WebSocket telnet and live event broadcasting
- Harmony performance tuning patches
- Multi-language localization (English-first)

## Requirements

### Backend

- [.NET SDK 8+](https://dotnet.microsoft.com/download) (builds `net48`)
- Visual Studio 2022 (Windows) or VS Code / Rider (Linux)
- **7 Days to Die** dedicated server game binaries (see below)

### Frontend

- Node.js 18+
- Git 2.20+

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

### 5. Deploy

Copy the build output to your server's `Mods/SdtdMultiServerKit/` directory alongside `ModInfo.xml` and the `Config/` folder.

**Do not expose port 8888 to the public internet.** Only the central panel should reach this API (firewall / VPN).

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
│   ├── Config/                   # appsettings, SQL migrations, locales
│   └── ModInfo.xml
└── scripts/                      # Dev tooling
```

## Multi-server roadmap

- [x] API-only mode (no embedded panel on game servers)
- [ ] Central panel repository ([7DaysToDie-MultiServerKit-Panel](https://github.com/syndaq/7DaysToDie-MultiServerKit-Panel))
- [ ] Shared database for cross-server player data (points, shop, VIP, CD keys)
- [ ] Panel server registry and health monitoring
- [ ] Player data persistence when switching servers

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md).

## License

[MIT](LICENSE)

## Links

- Repository: https://github.com/syndaq/7DaysToDie-MultiServerKit
- Panel: https://github.com/syndaq/7DaysToDie-MultiServerKit-Panel
- Original project: https://github.com/IceCoffee1024/7DaysToDie-ServerKit
