#!/usr/bin/env bash
# Copy Unity-compatible framework DLLs into Mods/.../Framework/ (not mod root).
set -euo pipefail

TARGET_DIR="${1:?Usage: $0 <build-output-dir>}"
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
UNITY_DIR="$SCRIPT_DIR/../src/SdtdMultiServerKit/3rdparty-binaries/linux-mono-unity"
FRAMEWORK_DIR="$TARGET_DIR/Framework"

mkdir -p "$FRAMEWORK_DIR"

if [[ ! -f "$UNITY_DIR/System.ComponentModel.DataAnnotations.dll" ]]; then
  echo "Missing Unity-compatible System.ComponentModel.DataAnnotations.dll in $UNITY_DIR" >&2
  echo "Extract it from a Linux 7DTD dedicated server:" >&2
  echo "  7DaysToDieServer_Data/Managed/System.ComponentModel.DataAnnotations.dll" >&2
  exit 1
fi

cp "$UNITY_DIR/System.ComponentModel.DataAnnotations.dll" "$FRAMEWORK_DIR/"
echo "Copied System.ComponentModel.DataAnnotations.dll to Framework/ (7DTD Managed)"
