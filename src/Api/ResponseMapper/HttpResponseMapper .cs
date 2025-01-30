using Application.Core.Responses;
using Application.Core.Responses.Enum;
using ThiIsFine.Api.Models.Base;

namespace ThiIsFine.Api.ResponseMapper
{
    public class HttpResponseMapper : IResponseMapper
    {
        private static readonly Dictionary<ResultStatus, int> StatusMapping = new Dictionary<ResultStatus, int>()
        {
            { ResultStatus.Success,  StatusCodes.Status200OK },
            { ResultStatus.BadRequest, StatusCodes.Status400BadRequest },
            { ResultStatus.Conflict, StatusCodes.Status409Conflict },
            { ResultStatus.NoContent, StatusCodes.Status204NoContent },
            { ResultStatus.NotFound, StatusCodes.Status404NotFound },
            { ResultStatus.Accepted, StatusCodes.Status202Accepted },
            { ResultStatus.PartialContent, StatusCodes.Status206PartialContent },
            { ResultStatus.Forbidden, StatusCodes.Status403Forbidden },
            { ResultStatus.Created, StatusCodes.Status201Created },
            { ResultStatus.TooManyRequests, StatusCodes.Status429TooManyRequests },
        };

        public IResult ExecuteAndMapStatus(Result result)
        {
            int statusCode = GetStatusCode(result.ResultStatus);

            return Results.Json(new { Message = result.Message }, statusCode: statusCode);
        }

        public IResult ExecuteAndMapStatus<TResultType, TInputType>(Result<TInputType> result)
            where TResultType : IResponseModel<TInputType>, new()
        {
            var statusCode = GetStatusCode(result.ResultStatus);
            var actionResult = (result.Data == null)
                ? Results.Json(new { Message = result.Message }, statusCode: statusCode)
                : Results.Json(new { Data = new TResultType().Convert(result.Data) }, statusCode: statusCode);

            return actionResult;
        }

        private static int GetStatusCode(ResultStatus statusCode)
        {
            return StatusMapping.ContainsKey(statusCode) ? StatusMapping[statusCode] : StatusCodes.Status500InternalServerError;
        }
    }
}
