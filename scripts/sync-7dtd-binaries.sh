#!/usr/bin/env bash
# Copy reference DLLs from a 7DTD dedicated server install into 7dtd-binaries/ for building the mod.
# Supports both v2.6 (separate TFP map/web mods) and v3.0+ (MapRendering/WebServer merged into Assembly-CSharp).
set -euo pipefail

ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
DEST="$ROOT/src/SdtdMultiServerKit/7dtd-binaries"
SERVER_ROOT="${1:-}"

if [[ -z "$SERVER_ROOT" ]]; then
  echo "Usage: $0 /path/to/7DaysToDieServer" >&2
  echo "" >&2
  echo "Example:" >&2
  echo "  $0 /srv/games/17/server" >&2
  exit 1
fi

MANAGED="$SERVER_ROOT/7DaysToDieServer_Data/Managed"
MODS="$SERVER_ROOT/Mods"

if [[ ! -d "$MANAGED" ]]; then
  echo "Managed folder not found: $MANAGED" >&2
  exit 1
fi

mkdir -p "$DEST"

copy_managed() {
  local name="$1"
  if [[ -f "$MANAGED/$name" ]]; then
    cp "$MANAGED/$name" "$DEST/"
    echo "Copied $name from Managed/"
  else
    echo "Missing in Managed/: $name" >&2
    return 1
  fi
}

copy_mod_dll() {
  local dll_name="$1"
  local found
  found="$(find "$MODS" -name "$dll_name" -print -quit 2>/dev/null || true)"
  if [[ -n "$found" && -f "$found" ]]; then
    cp "$found" "$DEST/$dll_name"
    echo "Copied $dll_name from $found"
    return 0
  fi
  return 1
}

echo "Syncing build references from: $SERVER_ROOT"
echo "Destination: $DEST"
echo

# Always required from Managed/
for dll in \
  Assembly-CSharp.dll \
  Assembly-CSharp-firstpass.dll \
  UnityEngine.CoreModule.dll \
  UnityEngine.AnimationModule.dll \
  LogLibrary.dll \
  0Harmony.dll \
  Noemax.GZip.dll \
  com.rlabrecque.steamworks.net.dll
do
  copy_managed "$dll"
done

# Optional on v3.0+ (merged into Assembly-CSharp). Required on v2.6 for local builds only — runtime uses reflection.
OPTIONAL_MISSING=()
for dll in MapRendering.dll WebServer.dll; do
  if ! copy_mod_dll "$dll"; then
    OPTIONAL_MISSING+=("$dll")
  fi
done

if [[ ${#OPTIONAL_MISSING[@]} -gt 0 ]]; then
  echo
  echo "Note: ${OPTIONAL_MISSING[*]} not found under Mods/."
  if [[ -f "$MODS/TFP_CommandExtensions/CommandExtensions.dll" || -d "$MODS/0_TFP_Harmony" ]]; then
    echo "This looks like a v3.0+ server — MapRendering/WebServer were merged into Assembly-CSharp."
    echo "That is OK; SdtdMultiServerKit no longer requires those DLLs at build or load time."
  else
    echo "If this is a v2.6 server, run Steam validate and ensure TFP_MapRendering / TFP_WebServer exist under Mods/."
  fi
fi

echo
echo "Done. Build with:"
echo "  dotnet build src/SdtdMultiServerKit/SdtdMultiServerKit.csproj -c Release"
