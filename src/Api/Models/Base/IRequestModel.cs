namespace ThiIsFine.Api.Models.Base;

public interface IRequestModel<out T>
{
    T? Convert();
}
