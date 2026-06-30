#!/usr/bin/env bash
set -euo pipefail

ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
VERSION="${1:-}"

if [[ -z "$VERSION" ]]; then
  echo "Usage: $0 <version>" >&2
  echo "Example: $0 1.0.0" >&2
  exit 1
fi

PROJECT="$ROOT/src/SdtdMultiServerKit/SdtdMultiServerKit.csproj"
OUTPUT_DIR="$ROOT/dist"
ARCHIVE="$OUTPUT_DIR/SdtdMultiServerKit-${VERSION}.zip"

echo "Building Release..."
dotnet build "$PROJECT" -c Release --no-incremental

BUILD_DIR="$ROOT/src/SdtdMultiServerKit/bin/Release/net48"
if [[ ! -d "$BUILD_DIR" ]]; then
  echo "Build output not found at $BUILD_DIR" >&2
  exit 1
fi

mkdir -p "$OUTPUT_DIR"
rm -f "$ARCHIVE"

echo "Packaging $ARCHIVE..."
(
  cd "$BUILD_DIR"
  zip -r "$ARCHIVE" . -x "*.pdb"
)

echo "Done: $ARCHIVE"
echo "Deploy by extracting into your server's Mods/SdtdMultiServerKit/ folder."
