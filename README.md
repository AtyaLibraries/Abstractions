# Abstractions

Provider-neutral contracts and marker interfaces for Atya foundation packages.

| | |
| --- | --- |
| Repository | [https://github.com/AtyaLibraries/Abstractions](https://github.com/AtyaLibraries/Abstractions) |
| NuGet | `Atya.Foundation.Abstractions` |
| License | MIT |

> This README is the repository landing page. A minimal, consumer-facing copy
> is packed into the NuGet package from `src/Abstractions/README.md`.

## Purpose

`Atya.Foundation.Abstractions` contains the smallest shared contracts used by
Foundation-level packages and consumers that only need provider-neutral
interfaces.

## Included Contracts

- `IHasId<TId>` for strongly typed identifiers
- `IEntity<TId>` for minimal entity composition
- `IAuditable` for create and update audit metadata
- `ISoftDeletable` for logical delete metadata
- `IPagedQuery` for paging request inputs
- `IResult` for minimal success/failure contracts

## Layout

- `src/Abstractions/` for the shipped package
- `tests/Abstractions.UnitTests/` for contract-focused tests
- `samples/Abstractions.Samples.Console/` for runnable usage examples
- `benchmarks/Abstractions.Benchmarks/` for BenchmarkDotNet coverage

## Build, Test, Pack

```bash
dotnet restore ./Abstractions.sln
dotnet build ./Abstractions.sln --configuration Release --no-restore
dotnet test ./tests/Abstractions.UnitTests/Abstractions.UnitTests.csproj --configuration Release --no-build
dotnet pack ./src/Abstractions/Abstractions.csproj --configuration Release --no-build
```

Artifacts land in `artifacts/packages/`.
