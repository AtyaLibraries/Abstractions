[CmdletBinding()]
param(
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Release",

    [switch]$SkipTests
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
$testProject = ".\tests\Abstractions.UnitTests\Abstractions.UnitTests.csproj"
$sampleProject = ".\samples\Abstractions.Samples.Console\Abstractions.Samples.Console.csproj"
$benchmarkProject = ".\benchmarks\Abstractions.Benchmarks\Abstractions.Benchmarks.csproj"

Push-Location $root
try {
    Invoke-DotNet restore $packageProject
    Invoke-DotNet restore $testProject
    Invoke-DotNet restore $sampleProject
    Invoke-DotNet restore $benchmarkProject

    Invoke-DotNet build $packageProject --configuration $Configuration --no-restore
    Invoke-DotNet build $testProject --configuration $Configuration --no-restore
    Invoke-DotNet build $sampleProject --configuration $Configuration --no-restore /p:UseSharedCompilation=false
    Invoke-DotNet build $benchmarkProject --configuration $Configuration --no-restore /p:UseSharedCompilation=false

    if (-not $SkipTests) {
        Invoke-DotNet test $testProject --configuration $Configuration --no-build --logger "trx;LogFileName=test-results.trx"
    }
}
finally {
    Pop-Location
}
