namespace ThiIsFine.Api.Models.Base;

public record PaginatedListModel<T>
{
    public IEnumerable<T>? PaginatedElements { get; set; }
    public PaginationInfoModel? PaginationInfo { get; set; }
}

public record PaginationInfoModel
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public long TotalCount { get; set; }
}
