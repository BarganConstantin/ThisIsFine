using ThiIsFine.Api.Models.Base;
using ThiIsFine.Application.Users.Commands.AuthenticateUser.DTOs;

namespace ThiIsFine.Api.Models.Users.Out;

public sealed record GenerateTokenModel : IResponseModel<UserTokensDto>
{
    public string? AccessToken { get; private set; }
    public DateTimeOffset AccessTokenExpiration { get; private set; }
    public DateTimeOffset CurrentTime { get; private set; }
    
    public object? Convert(UserTokensDto? dto)
    {
        if (dto == null) return default;
        
        AccessToken = dto.AccessToken;
        AccessTokenExpiration = dto.AccessTokenExpiration;
        CurrentTime = dto.CurrentTime;
        
        return this;
    }
}
