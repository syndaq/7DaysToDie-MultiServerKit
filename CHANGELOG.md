# Changelog

All notable changes to this project are documented here.

## [Unreleased]

## [1.0.7] - 2026-06-30

### Fixed

- **Linux SQLite init** — Stop bundling NuGet `System.Runtime.InteropServices.RuntimeInformation` (it pulls `System.Resources.ResourceManager`, which 7DTD Linux Managed does not ship). Patch `SQLitePCL.Batteries_V2.Init` to use the static `e_sqlite3` provider instead of the .NET Framework dynamic loader, and install `libe_sqlite3.so` under `MonoBleedingEdge/x86_64/`.
- **OS detection** — Use `Environment.OSVersion.Platform` instead of `RuntimeInformation.IsOSPlatform` for Linux/Windows checks.

### Note

- `Framework/` now only needs `System.ComponentModel.DataAnnotations.dll`. Remove `System.Runtime.InteropServices.RuntimeInformation.dll` if present from a v1.0.6 deploy.

## [1.0.6] - 2026-06-30

### Fixed

- **Linux SQLite init** — Bundle `System.Runtime.InteropServices.RuntimeInformation.dll` under `Framework/` (required by `Microsoft.Data.Sqlite` / SQLitePCL on Linux dedicated servers where Managed does not include it).
- **Release packaging** — Copy all `Framework/*.dll` during `dotnet build` so deploys always include the `Framework/` subfolder, not only when the CI copy script runs.

### Note

- After upgrading, confirm `Mods/SdtdMultiServerKit/Framework/` contains both `System.ComponentModel.DataAnnotations.dll` and `System.Runtime.InteropServices.RuntimeInformation.dll`.

## [1.0.5] - 2026-06-30

### Fixed

- **Linux dedicated servers** — Register framework assembly resolution when `SdtdMultiServerKit.dll` loads (before `System.Web.Http`), search the game's `7DaysToDieServer_Data/Managed` folder (Linux dedicated servers do not ship `MonoBleedingEdge/lib/mono`), and bundle the game's own `System.ComponentModel.DataAnnotations.dll` under `Framework/` (not the mod root). Ubuntu `mono-devel` copies are a different build and fail with `Invalid Image`.

### Note

- Fix invalid JSON in `LSTY_Data/appsettings.json` if deploy fails at config load — every property except the last needs a trailing comma.

## [1.0.4] - 2026-06-30

### Fixed

- **Linux dedicated servers** — Stop shipping Mono framework DLLs in the mod root. 7DTD auto-loads every `*.dll` in that folder; Ubuntu/debian `mono-devel` copies are not valid under Unity MonoBleedingEdge (`Invalid Image` on `System.ComponentModel.DataAnnotations.dll`). The mod now resolves those assemblies from the game's `7DaysToDieServer_Data/MonoBleedingEdge` tree at runtime.
- **Upgrade note** — After deploying v1.0.4, delete any leftover `System.ComponentModel.DataAnnotations.dll` and `System.Reflection.Emit*.dll` files from `Mods/SdtdMultiServerKit/` (not in a subfolder), then restart the server.

## [1.0.3] - 2026-06-30

### Fixed

- **Linux dedicated servers** — Removed Mapster (requires `System.Reflection.Emit` at mod init). Vendored Mono runtime DLLs are copied into every build output; mod preloads them on startup so `System.Web.Http` metadata works under Unity MonoBleedingEdge.

## [1.0.2] - 2026-06-30

### Fixed

- **Linux dedicated servers** — v1.0.1 shipped .NET *reference* assemblies (invalid at runtime on Mono). Release builds now copy real Mono runtime DLLs from `mono-devel` during packaging.

## [1.0.1] - 2026-06-30

### Fixed

- **Linux dedicated servers** — Bundle `System.ComponentModel.DataAnnotations` and `System.Reflection.Emit` (plus ILGeneration/Lightweight facades) in the release zip so the mod loads under Mono without missing-assembly errors.

## [1.0.0] - 2026-06-30

First production-ready MultiServerKit release with central panel integration.

### Added

- **API-only mode** — REST API on port 8888; no embedded web UI on game servers
- **Panel API key auth** — `X-Api-Key` for all REST requests; WebSocket auth via `?apiKey=` in ApiOnly mode
- **Cluster-native points** — `PanelUrl` routes balances through panel PostgreSQL with local SQLite cache
- **Login points sync** — Pull cluster balance on player login
- **PVP/PVE areas** — Custom zones with kill mode, drop-on-death, land-claim rules
- **Lottery** — Weighted pools with item/command bindings
- **Level gifts** — Milestone rewards with claim tracking
- **Boss kill reward** — Configurable points per entity type
- **Chunk reset** — Reset map regions by coordinates
- **Mute commands** — Block specific chat commands
- **SQL migrations** — `9-LevelGift.sql`, `10-PvpArea.sql`, `11-Lottery.sql`
- Wiki documentation and publish workflow
- GitHub Release workflow with pre-built mod zip

### Changed

- README and wiki expanded for multi-server deployment
- Build warnings reduced (XML docs, nullable annotations); CS8618 DTO warnings remain harmless

## Prior history

See git history for upstream ServerKit features and earlier fork development.

[Unreleased]: https://github.com/syndaq/7DaysToDie-MultiServerKit/compare/v1.0.2...main
[1.0.2]: https://github.com/syndaq/7DaysToDie-MultiServerKit/releases/tag/v1.0.2
[1.0.1]: https://github.com/syndaq/7DaysToDie-MultiServerKit/releases/tag/v1.0.1
[1.0.0]: https://github.com/syndaq/7DaysToDie-MultiServerKit/releases/tag/v1.0.0
