# Installation and Build

## 1. Clone

```bash
git clone https://github.com/syndaq/7DaysToDie-MultiServerKit.git
cd 7DaysToDie-MultiServerKit
```

## 2. Provide game binaries

From your **dedicated server install**, run:

```bash
bash scripts/sync-7dtd-binaries.sh /path/to/7DaysToDieServer
```

This copies reference DLLs into `src/SdtdMultiServerKit/7dtd-binaries/`.

Required from `7DaysToDieServer_Data/Managed/`: `Assembly-CSharp.dll`, `0Harmony.dll`, `UnityEngine.CoreModule.dll`, `LogLibrary.dll`, and others referenced in `SdtdMultiServerKit.csproj`.

### Game version 2.6 vs 3.0

| | v2.6 | v3.0+ |
|---|------|-------|
| MapRendering / WebServer | Separate mods: `Mods/TFP_MapRendering/`, `Mods/TFP_WebServer/` | **Merged into `Assembly-CSharp.dll`** — TFP map/web mod folders removed |
| Building the mod | Sync script may copy `MapRendering.dll` / `WebServer.dll` (optional) | Only Managed DLLs needed |
| Running the mod | One release zip works on both; map API resolved via reflection |
| Building from source on v3.0 | Only Managed DLLs needed; `GameCompatBridge` handles renamed game APIs at compile and runtime |

TFP [changeset 512](https://7dtd.illy.bz/changeset/512) (May 2026) moved WebServer, MapRendering, and HttpListener into the core game for v3.

Place `websocket-sharp.dll` in `src/SdtdMultiServerKit/3rdparty-binaries/`.

## 3. Build

```bash
dotnet restore src/SdtdMultiServerKit/SdtdMultiServerKit.csproj
dotnet build src/SdtdMultiServerKit/SdtdMultiServerKit.csproj -c Release
```

## 4. Deploy

Copy the build output to your server's `Mods/SdtdMultiServerKit/` directory alongside `ModInfo.xml` and the `Config/` folder.

On first run, `appsettings.json` is copied to `LSTY_Data/appsettings.json`.

## 5. Verify startup

Confirm the server log shows:

- SQL migrations applied
- `Loaded N functions`
- API listening on the configured `WebUrl` port (default **8888**)

## SQL migrations

On startup the mod runs SQL files from `Config/sql/` in filename order. Migrations are idempotent (`CREATE TABLE IF NOT EXISTS`). The SQLite database lives under the mod data folder.

| File range | Purpose |
|------------|---------|
| `1-` … `8-` | Core tables (items, commands, goods, points, VIP, CD keys, …) |
| `9-LevelGift.sql` | Level gift rewards |
| `10-PvpArea.sql` | PVP/PVE zones |
| `11-Lottery.sql` | Lottery pools |

Back up the SQLite database before major upgrades.

## Security

**Do not expose port 8888 or 8889 to the public internet.** Only the central panel host should reach these ports (firewall / VPN / private network).

## Next steps

- [[Configuration]] — set `ApiOnly`, `PanelApiKey`, `ServerId`, `PanelUrl`
- [[Panel Integration]] — register in the central panel
