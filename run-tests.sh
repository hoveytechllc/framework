#!/bin/bash
set -euo pipefail

find /code/tests -name "*.csproj" -exec dotnet restore {} \;
find /code/tests -name "*.csproj" -exec dotnet test {} \;