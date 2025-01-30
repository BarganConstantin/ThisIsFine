using Application.Core.Responses;
using Application.Core.Responses.Enum;

namespace ThiIsFine.Application.Base.BasePaginated;

public abstract class BasePaginatedHandler
{
    protected static Result<PaginatedList<T>> Validate<T>(BasePaginatedQuery<T> request)
    {
        if (request.Page < 1)
        {
            return new Result<PaginatedList<T>>()
            {
                ResultStatus = ResultStatus.BadRequest,
                Message = "Page must be greater than 0"
            };
        }

        if (request.PageSize < 1)
        {
            return new Result<PaginatedList<T>>()
            {
                ResultStatus = ResultStatus.BadRequest,
                Message = "PageSize must be greater than 0"
            };
        }

        return new Result<PaginatedList<T>>()
        {
            ResultStatus = ResultStatus.Success
        };
    }
}
