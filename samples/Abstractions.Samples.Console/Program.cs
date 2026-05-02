using Atya.Foundation.Abstractions;

Order order = new(Guid.NewGuid())
{
    CreatedAtUtc = DateTimeOffset.UtcNow,
    CreatedBy = "sample",
};

IPagedQuery query = new OrdersPageQuery(1, 25);
IResult result = new SampleResult(true);

Console.WriteLine("Package: Atya.Foundation.Abstractions");
Console.WriteLine($"Order id: {order.Id}");
Console.WriteLine($"Created by: {order.CreatedBy}");
Console.WriteLine($"Page request: {query.PageNumber} / {query.PageSize}");
Console.WriteLine($"Is failure: {result.IsFailure}");

file sealed class Order(Guid id) : IEntity<Guid>, IAuditable, ISoftDeletable
{
    public Guid Id { get; } = id;

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

    public bool IsDeleted
    {
        get; set;
    }

    public DateTimeOffset? DeletedAtUtc
    {
        get; set;
    }

    public string? DeletedBy
    {
        get; set;
    }
}

file sealed class OrdersPageQuery(int pageNumber, int pageSize) : IPagedQuery
{
    public int PageNumber { get; } = pageNumber;

    public int PageSize { get; } = pageSize;
}

file sealed class SampleResult(bool isSuccess) : IResult
{
    public bool IsSuccess { get; } = isSuccess;
}
