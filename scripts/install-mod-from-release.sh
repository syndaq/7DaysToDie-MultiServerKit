#!/usr/bin/env bash
# Clean-install SdtdMultiServerKit from a GitHub release zip on a dedicated server.
# Usage: install-mod-from-release.sh <server-root> [version]
# Example: install-mod-from-release.sh /srv/games/17/server 1.0.20
set -euo pipefail

SERVER_ROOT="${1:?Usage: $0 <server-root> [version]}"
VERSION="${2:-1.0.20}"
REPO="syndaq/7DaysToDie-MultiServerKit"
MODS_DIR="$SERVER_ROOT/Mods"
MOD_DIR="$MODS_DIR/SdtdMultiServerKit"
TMP_DIR="$(mktemp -d)"
SETTINGS_BACKUP=""

cleanup() {
  rm -rf "$TMP_DIR"
}
trap cleanup EXIT

if [[ -f "$MOD_DIR/LSTY_Data/appsettings.json" ]]; then
  SETTINGS_BACKUP="$TMP_DIR/appsettings.json.bak"
  cp "$MOD_DIR/LSTY_Data/appsettings.json" "$SETTINGS_BACKUP"
  echo "Backed up LSTY_Data/appsettings.json"
fi

echo "Removing old mod folders..."
rm -rf "$MOD_DIR"
find "$MODS_DIR" -maxdepth 1 -type d -name 'syndaq-mod-extract-*' -exec rm -rf {} + 2>/dev/null || true

ZIP_URL="https://github.com/${REPO}/releases/download/v${VERSION}/SdtdMultiServerKit-${VERSION}.zip"
ZIP_FILE="$TMP_DIR/SdtdMultiServerKit-${VERSION}.zip"

echo "Downloading v${VERSION} from GitHub..."
curl -fsSL "$ZIP_URL" -o "$ZIP_FILE"

mkdir -p "$MOD_DIR"
unzip -q "$ZIP_FILE" -d "$MOD_DIR"

if [[ -n "$SETTINGS_BACKUP" && -f "$SETTINGS_BACKUP" ]]; then
  mkdir -p "$MOD_DIR/LSTY_Data"
  cp "$SETTINGS_BACKUP" "$MOD_DIR/LSTY_Data/appsettings.json"
  echo "Restored LSTY_Data/appsettings.json"
fi

INSTALLED_VERSION="$(grep -oP '(?<=<Version value=\")[^\"]+' "$MOD_DIR/ModInfo.xml" || true)"
ROOT_DLL_COUNT="$(find "$MOD_DIR" -maxdepth 1 -name '*.dll' | wc -l)"
LIB_DLL_COUNT="$(find "$MOD_DIR/Lib" -maxdepth 1 -name '*.dll' 2>/dev/null | wc -l || echo 0)"

echo ""
echo "Installed ModInfo version: ${INSTALLED_VERSION:-unknown}"
echo "DLLs in mod root: $ROOT_DLL_COUNT (expected 1: SdtdMultiServerKit.dll)"
echo "DLLs in Lib/: $LIB_DLL_COUNT"
echo "Mod path: $MOD_DIR"
echo ""
echo "Restart the dedicated server and confirm the log shows:"
echo "  Loaded Mod: SdtdMultiServerKit (${VERSION})"
echo "  [LSTY] ... listening on ...:8888"
