using Application.Core.Responses;
using Application.Core.Responses.Enum;

namespace ThiIsFine.Application.Base.BaseById;

public abstract class BaseByIdHandler<T>
{
    protected static Result Validate(BaseByIdRequest<T> request)
    {
        if (string.IsNullOrWhiteSpace(request.Id))
            return new Result() { Message = "Id is required", ResultStatus = ResultStatus.BadRequest };
        
        return new Result() { ResultStatus = ResultStatus.Success };
    }
}
