using Application.Core.Responses;

namespace ThiIsFine.Application.Base.BaseById;

public record BaseByIdRequest<T> : IRequest<Result<T>>
{
    public string? Id { get; init; }

    public BaseByIdRequest(string? id)
    {
        Id = id;
    }
}
