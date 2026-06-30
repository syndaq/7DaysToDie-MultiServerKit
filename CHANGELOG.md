# Changelog

All notable changes to this project are documented here.

## [Unreleased]

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

[Unreleased]: https://github.com/syndaq/7DaysToDie-MultiServerKit/compare/v1.0.0...main
[1.0.0]: https://github.com/syndaq/7DaysToDie-MultiServerKit/releases/tag/v1.0.0
