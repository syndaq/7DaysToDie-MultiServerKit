# 7DaysToDie-MultiServerKit

A mod for **7 Days to Die** dedicated servers that provides RESTful APIs and a web management panel for server owners and administrators. This fork extends the original [7DaysToDie-ServerKit](https://github.com/IceCoffee1024/7DaysToDie-ServerKit) into a **multi-server** management platform.

## Vision

MultiServerKit is being developed to manage multiple dedicated game servers from a single panel. Shared systems — shop, points, VIP, CD keys, and player data — will travel with players when they move between servers in the cluster.

## Features (current)

- RESTful API for server administration
- Web management panel (frontend built separately)
- Points system, game store, VIP gifts, and CD key redemption
- Teleport (home, city, friend), colored chat, auto-backup, and task scheduling
- WebSocket telnet and live event broadcasting
- OAuth and Steam login for the web panel
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

### 4. Deploy

Copy the build output to your server's `Mods/SdtdMultiServerKit/` directory alongside `ModInfo.xml` and the `Config/` folder.

Default web panel: `http://<server-ip>:8888`

## Project structure

```
7DaysToDie-MultiServerKit/
├── 7DaysToDie-MultiServerKit.sln
├── src/SdtdMultiServerKit/       # Main mod project (C# / net48)
│   ├── WebApi/                   # OWIN REST API + OAuth
│   ├── WebSockets/               # Telnet + broadcaster
│   ├── Data/                     # SQLite repositories
│   ├── Functions/                # Game features (store, points, teleport, etc.)
│   ├── Config/                   # appsettings, SQL migrations, locales
│   └── ModInfo.xml
└── scripts/                      # Dev tooling
```

## Multi-server roadmap

The current codebase runs as **one mod instance per game server**. Planned work for MultiServerKit:

- [ ] Central orchestrator / shared database for cross-server player data
- [ ] Unified admin panel for multiple server instances
- [ ] Shared points, shop, VIP, and CD key systems across the cluster
- [ ] Player data persistence when switching servers

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md).

## License

[MIT](LICENSE)

## Links

- Repository: https://github.com/syndaq/7DaysToDie-MultiServerKit
- Original project: https://github.com/IceCoffee1024/7DaysToDie-ServerKit
