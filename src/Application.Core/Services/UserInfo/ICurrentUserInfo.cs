namespace Application.Core.Services.UserInfo;

public interface ICurrentUserInfo
{
    string Id { get; }
    string? UserName { get; }
    string? Role { get; }
}
