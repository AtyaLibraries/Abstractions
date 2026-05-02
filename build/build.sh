#!/usr/bin/env bash
set -euo pipefail

CONFIGURATION="${CONFIGURATION:-Release}"
SKIP_TESTS="${SKIP_TESTS:-false}"
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(cd "${SCRIPT_DIR}/.." && pwd)"
SOLUTION_FILE="$(find "$ROOT_DIR" -maxdepth 1 -name '*.sln' -type f | head -n 1)"

cd "$ROOT_DIR"

if [ -z "$SOLUTION_FILE" ]; then
  echo "Unable to locate a solution file under '$ROOT_DIR'." >&2
  exit 1
fi

dotnet restore "$SOLUTION_FILE"
dotnet build "$SOLUTION_FILE" --configuration "$CONFIGURATION" --no-restore

if [ "$SKIP_TESTS" != "true" ]; then
  dotnet test "$SOLUTION_FILE" --configuration "$CONFIGURATION" --no-build --logger "trx;LogFileName=test-results.trx"
fi
