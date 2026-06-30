# Releasing

This project uses [Semantic Versioning](https://semver.org/) tags (`v1.0.0`) and GitHub Releases with a pre-built mod zip.

## Version files (keep in sync)

| File | Field |
|------|--------|
| `src/SdtdMultiServerKit/ModInfo.xml` | `<Version value="…"/>` |
| `src/SdtdMultiServerKit/SdtdMultiServerKit.csproj` | `<Version>` |
| `CHANGELOG.md` | Release section |
| Git tag | `v1.0.0` |

## Maintainer release checklist

1. Update `CHANGELOG.md` (move `[Unreleased]` into a versioned section).
2. Bump `ModInfo.xml` and `SdtdMultiServerKit.csproj` version.
3. Commit on `main`, push.
4. Create and push an annotated tag:

   ```bash
   git tag -a v1.0.0 -m "Release 1.0.0"
   git push origin v1.0.0
   ```

5. The **Release** GitHub Action builds `Release`, zips `bin/Release/net48`, and publishes the asset to the GitHub Release.

## Local packaging (without GitHub)

```bash
chmod +x scripts/package-release.sh
./scripts/package-release.sh 1.0.0
# Output: dist/SdtdMultiServerKit-1.0.0.zip
```

Upload manually if needed:

```bash
gh release create v1.0.0 dist/SdtdMultiServerKit-1.0.0.zip \
  --title "1.0.0" \
  --notes-file CHANGELOG.md
```

## Installing on a game server

1. Stop the dedicated server.
2. Back up `Mods/SdtdMultiServerKit/` and `Mods/SdtdMultiServerKit/LSTY_Data/`.
3. Download `SdtdMultiServerKit-x.y.z.zip` from [Releases](https://github.com/syndaq/7DaysToDie-MultiServerKit/releases).
4. Extract into `Mods/SdtdMultiServerKit/` (preserve `LSTY_Data/appsettings.json` if customized).
5. Start the server; confirm SQL migrations and API startup in the log.

## Panel pairing

Use the matching panel release from [7DaysToDie-MultiServerKit-Panel](https://github.com/syndaq/7DaysToDie-MultiServerKit-Panel/releases). Set `PanelUrl`, `PanelApiKey`, and `ServerId` in mod `appsettings.json`.
