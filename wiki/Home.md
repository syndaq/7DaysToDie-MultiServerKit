Welcome to the **7DaysToDie-MultiServerKit** wiki.

This mod exposes a REST API and WebSocket stream for the central [MultiServerKit Panel](https://github.com/syndaq/7DaysToDie-MultiServerKit-Panel). It does **not** serve a web UI — admins use the separate panel repository.

## What this mod does

MultiServerKit turns a 7 Days to Die dedicated server into a managed node in a **server cluster**:

- **Per-server** features (console, map, teleport, PVP areas, backups, permissions, …) are controlled via the panel proxy.
- **Cluster-wide** features (shop, points, VIP gifts, CD keys, level gifts, lottery) sync through the panel PostgreSQL database.

## Wiki pages

| Page | Description |
|------|-------------|
| [[Installation and Build]] | Clone, binaries, build, deploy |
| [[Configuration]] | `appsettings.json` reference |
| [[Panel Integration]] | Register a server in the panel |
| [[Cluster Points]] | Panel as source of truth for player points |
| [[WebSocket and Live Events]] | Telnet, event broadcast, panel aggregation |
| [[API Reference]] | REST endpoints and authentication |

## Quick links

- [GitHub repository](https://github.com/syndaq/7DaysToDie-MultiServerKit)
- [Panel repository](https://github.com/syndaq/7DaysToDie-MultiServerKit-Panel)
- [Original ServerKit project](https://github.com/IceCoffee1024/7DaysToDie-ServerKit)

## Requirements

- .NET SDK 8+ (builds `net48`)
- 7DTD dedicated server binaries for references
- Central panel host with network access to this server's API (**8888**) and WebSocket (**8889**) ports
