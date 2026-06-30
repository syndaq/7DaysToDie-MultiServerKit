#!/usr/bin/env bash
# Move dependency DLLs out of the mod root. 7DTD auto-loads every *.dll in the
# mod folder as a mod assembly; Autofac and other deps must live under Lib/.
set -euo pipefail

BUILD_DIR="${1:?Usage: $0 <build-output-dir>}"
LIB_DIR="$BUILD_DIR/Lib"

mkdir -p "$LIB_DIR"

shopt -s nullglob
for dll in "$BUILD_DIR"/*.dll; do
  base="$(basename "$dll")"
  if [[ "$base" == "SdtdMultiServerKit.dll" ]]; then
    continue
  fi

  mv "$dll" "$LIB_DIR/"
  echo "Moved $base to Lib/"
done

if [[ -z "$(ls -A "$LIB_DIR" 2>/dev/null || true)" ]]; then
  rmdir "$LIB_DIR" 2>/dev/null || true
  echo "No dependency DLLs to move."
else
  echo "Dependency DLLs are in Lib/ (only SdtdMultiServerKit.dll remains in mod root)."
fi
