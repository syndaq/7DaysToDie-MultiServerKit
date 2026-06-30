#!/usr/bin/env bash
# Copy Unity-compatible framework DLLs into Mods/.../Framework/ (not mod root).
set -euo pipefail

TARGET_DIR="${1:?Usage: $0 <build-output-dir>}"
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
UNITY_DIR="$SCRIPT_DIR/../src/SdtdMultiServerKit/3rdparty-binaries/linux-mono-unity"
FRAMEWORK_DIR="$TARGET_DIR/Framework"

mkdir -p "$FRAMEWORK_DIR"

shopt -s nullglob
dlls=("$UNITY_DIR"/*.dll)
if [[ ${#dlls[@]} -eq 0 ]]; then
  echo "No framework DLLs in $UNITY_DIR" >&2
  echo "Expected at least System.ComponentModel.DataAnnotations.dll from a Linux 7DTD dedicated server." >&2
  exit 1
fi

for dll in "${dlls[@]}"; do
  cp "$dll" "$FRAMEWORK_DIR/"
  echo "Copied $(basename "$dll") to Framework/"
done
