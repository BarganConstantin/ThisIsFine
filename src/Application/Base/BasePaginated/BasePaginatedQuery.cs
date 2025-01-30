using Application.Core.Responses;

namespace ThiIsFine.Application.Base.BasePaginated;

public record BasePaginatedQuery<T> : IRequest<Result<PaginatedList<T>>>
{
    public int Page { get; }
    public int PageSize { get; }

    public BasePaginatedQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}
