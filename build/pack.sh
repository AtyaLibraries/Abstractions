#!/usr/bin/env bash
set -euo pipefail

CONFIGURATION="${CONFIGURATION:-Release}"
VERSION_SUFFIX="${VERSION_SUFFIX:-}"
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(cd "${SCRIPT_DIR}/.." && pwd)"
SOLUTION_FILE="$(find "$ROOT_DIR" -maxdepth 1 -name '*.sln' -type f | head -n 1)"

cd "$ROOT_DIR"

if [ -z "$SOLUTION_FILE" ]; then
  echo "Unable to locate a solution file under '$ROOT_DIR'." >&2
  exit 1
fi

dotnet restore "$SOLUTION_FILE"
dotnet test "$SOLUTION_FILE" --configuration "$CONFIGURATION" --no-restore

pack_args=(
  pack
  "./src/Abstractions.NuGet/Abstractions.NuGet.csproj"
  --configuration "$CONFIGURATION"
  --no-build
  --output "./artifacts/packages"
)

if [ -n "$VERSION_SUFFIX" ]; then
  pack_args+=("/p:VersionSuffix=$VERSION_SUFFIX")
fi

dotnet "${pack_args[@]}"
