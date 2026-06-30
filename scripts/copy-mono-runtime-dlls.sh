#!/usr/bin/env bash
# Deprecated in v1.0.4 — framework DLLs are resolved from the game's MonoBleedingEdge at runtime.
# Do not copy mono-devel or vendored DLLs into the mod root (7DTD auto-loads them and fails).
set -euo pipefail

echo "copy-mono-runtime-dlls.sh: no-op (v1.0.4+ resolves Mono assemblies from 7DaysToDieServer_Data/MonoBleedingEdge)"
