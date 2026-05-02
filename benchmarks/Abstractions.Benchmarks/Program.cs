using Atya.Foundation.Abstractions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Abstractions.Benchmarks;

public static class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<ResultContractBenchmarks>();
    }
}

[MemoryDiagnoser]
[ShortRunJob]
public class ResultContractBenchmarks
{
    private static readonly IResult Success = new FakeResult(true);
    private static readonly IResult Failure = new FakeResult(false);
    private static readonly IHasId<Guid> Entity = new FakeEntity(Guid.Parse("11111111-1111-1111-1111-111111111111"));
    private static readonly IPagedQuery PagedQuery = new FakePagedQuery(3, 50);
    private static readonly IAuditable Auditable = new FakeAuditableEntity
    {
        CreatedAtUtc = new DateTimeOffset(2026, 4, 22, 10, 0, 0, TimeSpan.Zero),
        CreatedBy = "system",
        LastModifiedAtUtc = new DateTimeOffset(2026, 4, 22, 10, 5, 0, TimeSpan.Zero),
        LastModifiedBy = "admin",
    };

    [Benchmark]
    public bool ReadIsSuccess() => Success.IsSuccess;

    [Benchmark]
    public bool ReadIsFailure() => Failure.IsFailure;

    [Benchmark]
    public Guid ReadEntityId() => Entity.Id;

    [Benchmark]
    public int ReadPagedQueryPageSize() => PagedQuery.PageSize;

    [Benchmark]
    public DateTimeOffset ReadAuditCreatedAtUtc() => Auditable.CreatedAtUtc;

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
