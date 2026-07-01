using System.Diagnostics.CodeAnalysis;
using Atya.Foundation.Abstractions;
using BenchmarkDotNet.Attributes;

namespace Abstractions.Benchmarks;

[SuppressMessage(
    "Performance",
    "CA1859:Use concrete types when possible",
    Justification = "The benchmark intentionally measures access through public abstraction interfaces.")]
public class ContractAccessBenchmarks
{
    private readonly IResult success = new FakeResult(true);
    private readonly IResult failure = new FakeResult(false);
    private readonly IHasId<Guid> entity = new FakeEntity(Guid.Parse("11111111-1111-1111-1111-111111111111"));
    private readonly IPagedQuery pagedQuery = new FakePagedQuery(3, 50);
    private readonly IAuditable auditable = new FakeAuditableEntity
    {
        CreatedAtUtc = new DateTimeOffset(2026, 4, 22, 10, 0, 0, TimeSpan.Zero),
        CreatedBy = "system",
        LastModifiedAtUtc = new DateTimeOffset(2026, 4, 22, 10, 5, 0, TimeSpan.Zero),
        LastModifiedBy = "admin",
    };

    [Benchmark]
    public bool ReadIsSuccess() => this.success.IsSuccess;

    [Benchmark]
    public bool ReadIsFailure() => this.failure.IsFailure;

    [Benchmark]
    public Guid ReadEntityId() => this.entity.Id;

    [Benchmark]
    public int ReadPagedQueryPageSize() => this.pagedQuery.PageSize;

    [Benchmark]
    public DateTimeOffset ReadAuditCreatedAtUtc() => this.auditable.CreatedAtUtc;

    private sealed class FakeResult(bool isSuccess) : IResult
    {
        public bool IsSuccess { get; } = isSuccess;
    }

    private sealed record FakeEntity(Guid Id) : IHasId<Guid>;

    private sealed class FakePagedQuery(int pageNumber, int pageSize) : IPagedQuery
    {
        public int PageNumber { get; } = pageNumber;

        public int PageSize { get; } = pageSize;
    }

    private sealed class FakeAuditableEntity : IAuditable
    {
        public DateTimeOffset CreatedAtUtc
        {
            get; set;
        }

        public string? CreatedBy
        {
            get; set;
        }

        public DateTimeOffset? LastModifiedAtUtc
        {
            get; set;
        }

        public string? LastModifiedBy
        {
            get; set;
        }
    }
}
