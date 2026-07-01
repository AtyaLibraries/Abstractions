<h1 align="center">Atya.Foundation.Abstractions</h1>

<p align="center"><i>Provider-neutral contracts for Atya foundation packages.</i></p>

<p align="center">
  <a href="https://www.nuget.org/packages/Atya.Foundation.Abstractions"><img src="https://img.shields.io/nuget/v/Atya.Foundation.Abstractions?style=for-the-badge&logo=nuget&logoColor=white&label=NuGet&color=512BD4" alt="NuGet Version"></a>
  <a href="https://www.nuget.org/packages/Atya.Foundation.Abstractions"><img src="https://img.shields.io/nuget/dt/Atya.Foundation.Abstractions?style=for-the-badge&logo=nuget&logoColor=white&label=Downloads&color=512BD4" alt="NuGet Downloads"></a>
  <img src="https://img.shields.io/badge/net10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="Target Framework">
  <a href="LICENSE"><img src="https://img.shields.io/github/license/AtyaLibraries/Abstractions?style=for-the-badge&color=512BD4" alt="License"></a>
  <a href="https://github.com/AtyaLibraries/Abstractions/actions"><img src="https://img.shields.io/github/actions/workflow/status/AtyaLibraries/Abstractions/ci.yml?branch=development&style=for-the-badge&logo=githubactions&logoColor=white&label=Build" alt="Build"></a>
  <a href="https://github.com/AtyaLibraries/Abstractions"><img src="https://img.shields.io/github/stars/AtyaLibraries/Abstractions?style=for-the-badge&logo=github&logoColor=white&color=512BD4" alt="Stars"></a>
</p>

---

## Overview

`Atya.Foundation.Abstractions` provides the smallest shared contracts used by Atya foundation packages and applications that need provider-neutral model, paging, audit, soft-delete, and result abstractions.

The package intentionally contains interfaces only. It does not choose an ORM, persistence provider, validation framework, or result implementation.

## Features

- Strongly typed identifier contracts through `IHasId<TId>` and `IEntity<TId>`.
- Audit metadata through `IAuditable`.
- Soft-delete metadata through `ISoftDeletable`.
- Paging request shape through `IPagedQuery`.
- A minimal result-like success contract through `IResult`.
- No runtime package dependencies.

## Installation

**.NET CLI**

```bash
dotnet add package Atya.Foundation.Abstractions
```

**Package Manager**

```powershell
Install-Package Atya.Foundation.Abstractions
```

**PackageReference**

```xml
<PackageReference Include="Atya.Foundation.Abstractions" Version="1.0.3" />
```

## Quick Start

```csharp
using System;
using Atya.Foundation.Abstractions;

Order order = new(Guid.NewGuid())
{
    CreatedAtUtc = DateTimeOffset.UtcNow,
    CreatedBy = "system",
};
IResult result = order;

Console.WriteLine(order.Id);
Console.WriteLine(result.IsFailure);

public sealed class Order(Guid id) : IEntity<Guid>, IAuditable, IResult
{
    public Guid Id { get; } = id;

    public DateTimeOffset CreatedAtUtc { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset? LastModifiedAtUtc { get; set; }

    public string? LastModifiedBy { get; set; }

    public bool IsSuccess => true;
}
```

## Usage

Use these contracts at package boundaries when code needs to depend on a stable shape without depending on an implementation package.

```csharp
using System;
using Atya.Foundation.Abstractions;

public sealed record CustomerId(Guid Value);

public sealed class Customer(CustomerId id) : IEntity<CustomerId>, ISoftDeletable
{
    public CustomerId Id { get; } = id;

    public bool IsDeleted { get; set; }

    public DateTimeOffset? DeletedAtUtc { get; set; }

    public string? DeletedBy { get; set; }
}
```

`IResult.IsFailure` is provided as a default interface member and is always the negation of `IsSuccess`.

## API Overview

| Type | Purpose |
| --- | --- |
| `IHasId<TId>` | Exposes a strongly typed `Id` value. |
| `IEntity<TId>` | Minimal entity contract that also implements `IHasId<TId>`. |
| `IAuditable` | Adds created and last-modified UTC timestamps plus actor fields. |
| `ISoftDeletable` | Adds logical deletion state, timestamp, and actor fields. |
| `IPagedQuery` | Exposes a 1-based page number and requested page size. |
| `IResult` | Exposes `IsSuccess` and the default `IsFailure` inverse. |

## Dependencies

The shipped library has no runtime package dependencies. Build, test, benchmark, and packaging tools are repository-local development dependencies and are not part of the consumer dependency graph.

## Package Boundaries

This package defines contracts only. It does not provide persistence mappings, paging validation, audit population, soft-delete policies, or concrete result types.

## Project Structure

```text
src/Abstractions/                  shipped package source and NuGet README
tests/Abstractions.UnitTests/      contract-focused unit tests
samples/Abstractions.Samples.Console/ runnable sample usage
benchmarks/Abstractions.Benchmarks/ BenchmarkDotNet harness
.github/workflows/                 CI, CodeQL, dependency review, and publish workflows
Abstractions.sln                   solution file
README.md                          GitHub README
LICENSE                            MIT license
```

## Compatibility

Targets `net10.0`.

## Testing

```bash
dotnet test ./tests/Abstractions.UnitTests/Abstractions.UnitTests.csproj --configuration Release
```

## Benchmarks

The repository includes a BenchmarkDotNet harness for contract access patterns. No benchmark result claims are published in this README.

```bash
dotnet run --project ./benchmarks/Abstractions.Benchmarks/Abstractions.Benchmarks.csproj --configuration Release
```

## Contributing

Contributions are welcome. Please open an issue or pull request on [GitHub](https://github.com/AtyaLibraries/Abstractions).

## License

Released under the **MIT** license. See [LICENSE](LICENSE) for details.

---

## About Atya Libraries

`Atya.Foundation.Abstractions` is part of **[Atya Libraries](https://github.com/AtyaLibraries)**, a family of focused, modern .NET libraries published under the reserved **`Atya.*`** prefix on NuGet. Every package shares the same principles: a small, clear public API, full test coverage, and consistent documentation.

Browse the full collection on [GitHub](https://github.com/AtyaLibraries) and [NuGet](https://www.nuget.org/profiles/ArsenAsulyan).

<p align="center"><sub>Made with .NET. Copyright 2026 Atya Libraries.</sub></p>
