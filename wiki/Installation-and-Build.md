# Installation and Build

## 1. Clone

```bash
git clone https://github.com/syndaq/7DaysToDie-MultiServerKit.git
cd 7DaysToDie-MultiServerKit
```

## 2. Provide game binaries

Copy required DLLs from your 7DTD dedicated server into:

```
src/SdtdMultiServerKit/7dtd-binaries/
```

Required files include `Assembly-CSharp.dll`, `0Harmony.dll`, `UnityEngine.CoreModule.dll`, `LogLibrary.dll`, `MapRendering.dll`, `WebServer.dll`, and others referenced in `SdtdMultiServerKit.csproj`.

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
