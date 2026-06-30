#!/usr/bin/env bash
set -euo pipefail

TARGET_DIR="${1:?Usage: $0 <build-output-dir>}"
MONO_API="/usr/lib/mono/4.5-api"

if [[ ! -d "$MONO_API" ]]; then
  echo "Mono 4.5-api not found at $MONO_API" >&2
  echo "Install mono-devel on the build host (e.g. apt-get install mono-devel)." >&2
  exit 1
fi

declare -a RUNTIME_DLLS=(
  "System.ComponentModel.DataAnnotations.dll"
  "Facades/System.Reflection.Emit.dll"
  "Facades/System.Reflection.Emit.ILGeneration.dll"
  "Facades/System.Reflection.Emit.Lightweight.dll"
)

for rel in "${RUNTIME_DLLS[@]}"; do
  src="$MONO_API/$rel"
  dest="$TARGET_DIR/$(basename "$rel")"
  if [[ ! -f "$src" ]]; then
    echo "Missing Mono runtime DLL: $src" >&2
    exit 1
  fi
  cp "$src" "$dest"
  echo "Copied $(basename "$rel")"
done
