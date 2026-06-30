#!/usr/bin/env bash
set -euo pipefail

TARGET_DIR="${1:?Usage: $0 <build-output-dir>}"
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
VENDORED_DIR="$SCRIPT_DIR/../src/SdtdMultiServerKit/3rdparty-binaries/linux-mono"
MONO_API="/usr/lib/mono/4.5-api"

declare -a RUNTIME_DLLS=(
  "System.ComponentModel.DataAnnotations.dll"
  "System.Reflection.Emit.dll"
  "System.Reflection.Emit.ILGeneration.dll"
  "System.Reflection.Emit.Lightweight.dll"
)

declare -a MONO_REL_PATHS=(
  "System.ComponentModel.DataAnnotations.dll"
  "Facades/System.Reflection.Emit.dll"
  "Facades/System.Reflection.Emit.ILGeneration.dll"
  "Facades/System.Reflection.Emit.Lightweight.dll"
)

for i in "${!RUNTIME_DLLS[@]}"; do
  name="${RUNTIME_DLLS[$i]}"
  dest="$TARGET_DIR/$name"

  if [[ -f "$VENDORED_DIR/$name" ]]; then
    cp "$VENDORED_DIR/$name" "$dest"
    echo "Copied $name (vendored)"
    continue
  fi

  if [[ ! -d "$MONO_API" ]]; then
    echo "Mono 4.5-api not found at $MONO_API and vendored $name missing" >&2
    echo "Install mono-devel or ensure 3rdparty-binaries/linux-mono is populated." >&2
    exit 1
  fi

  src="$MONO_API/${MONO_REL_PATHS[$i]}"
  if [[ ! -f "$src" ]]; then
    echo "Missing Mono runtime DLL: $src" >&2
    exit 1
  fi
  cp "$src" "$dest"
  echo "Copied $name (mono-devel)"
done
