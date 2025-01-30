using Application.Core.Responses;
using ThiIsFine.Api.Models.Base;

namespace ThiIsFine.Api.ResponseMapper
{
    public interface IResponseMapper
    {
        IResult ExecuteAndMapStatus(Result result);
        IResult ExecuteAndMapStatus<TResultType, TInputType>(Result<TInputType> result)
            where TResultType : IResponseModel<TInputType>, new();
    }
}
