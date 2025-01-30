using Application.Core.Responses.Enum;

namespace Application.Core.Responses;

public class Result
{
    public string? Message { get; set; }
    public ResultStatus ResultStatus { get; set; }

    public bool Succeeded => ResultStatus == ResultStatus.Success 
                             || ResultStatus == ResultStatus.Created;
        
    public static Result<T> NotFound<T>(string message)
    {
        return new Result<T>
        {
            Message = message,
            ResultStatus = ResultStatus.NotFound
        };
    }

    public static Result<T> Success<T>(T data)
    {
        return new Result<T>
        {
            Data = data,
            ResultStatus = ResultStatus.Success
        };
    }
    
    public static Result BadRequest(string message)
    {
        return new Result
        {
            Message = message,
            ResultStatus = ResultStatus.BadRequest
        };
    }
    
    public static Result<T> BadRequest<T>(string message)
    {
        return new Result<T>
        {
            Message = message,
            ResultStatus = ResultStatus.BadRequest
        };
    }
    
    public Result<T> ConvertTo<T>()
    {
        return new Result<T>
        {
            Message = Message,
            ResultStatus = ResultStatus
        };
    }
}

public class Result<T> : Result
{
    public T? Data { get; set; }
}
