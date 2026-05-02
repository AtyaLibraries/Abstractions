[CmdletBinding()]
param(
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Release",

    [string]$VersionSuffix
)

$ErrorActionPreference = "Stop"

function Invoke-DotNet {
    param(
        [Parameter(ValueFromRemainingArguments = $true)]
        [string[]] $Arguments
    )

    & dotnet @Arguments

    if ($LASTEXITCODE -ne 0) {
        throw "dotnet $($Arguments -join ' ') failed with exit code $LASTEXITCODE."
    }
}
$root = Resolve-Path (Join-Path $PSScriptRoot "..")
$packageProject = ".\src\Abstractions.NuGet\Abstractions.NuGet.csproj"

Push-Location $root
try {
    Invoke-DotNet restore $packageProject

    $packArgs = @(
        "pack",
        $packageProject,
        "--configuration",
        $Configuration,
        "--no-restore",
        "--output",
        ".\artifacts\packages"
    )

    if ($VersionSuffix) {
        $packArgs += "/p:VersionSuffix=$VersionSuffix"
    }

    Invoke-DotNet @packArgs
}
finally {
    Pop-Location
}
