# Changelog

All notable changes to this project are documented here.

## [Unreleased]

### Added

- **PVP/PVE areas** — Custom zones with kill mode, drop-on-death, land-claim bonus, invulnerable claims; Harmony enforcement hooks
- **Lottery** — Weighted pools with item/command bindings; settings and draw commands
- **Level gifts** — Milestone rewards with claim tracking
- **Boss kill reward** — Configurable points per entity type with fallback reward and kill tip
- **Chunk reset** — `POST /api/ChunkReset` to reset map regions by coordinates
- **Mute commands** — Block specific chat commands with a custom player message
- **SQL migrations** — `9-LevelGift.sql`, `10-PvpArea.sql`, `11-Lottery.sql`
- Settings API controllers for PVP areas, lottery, level gifts, boss kill reward, mute commands

### Changed

- README expanded with feature matrix, deployment checklist, and updated roadmap
- `NU1903` suppressed for SQLitePCLRaw 2.x with documented upgrade path

## Prior releases

See git history for API-only mode, English-first rename, and earlier ServerKit features.
