namespace ThiIsFine.Api.Models.Base;

public interface IResponseModel<T>
{
    object? Convert(T? dto);
}
