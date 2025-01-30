namespace Application.Core.Responses;

public class PaginatedList<T>
{
    public required IEnumerable<T> Data { get; set; }
    public required PaginationInfo Pagination { get; set; }
}

public class PaginationInfo
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public long TotalCount { get; set; }
}
