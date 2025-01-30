namespace Application.Core.Responses;

public static class PaginationHelper
{
    public static PaginatedList<TResult> Paginate<TSource, TResult>(
        IQueryable<TSource> data,
        Func<TSource, TResult> mapper,
        int page,
        int pageSize)
    {
        var totalCount = data.Count();
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        var paginatedData = data.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsEnumerable()
            .Select(mapper);

        return new PaginatedList<TResult>
        {
            Data = paginatedData,
            Pagination = new PaginationInfo
            {
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalCount = totalCount
            }
        };
    }
}
