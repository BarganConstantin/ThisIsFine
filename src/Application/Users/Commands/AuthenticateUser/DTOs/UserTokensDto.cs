using Application.Core.Services.Identity.DTOs;

namespace ThiIsFine.Application.Users.Commands.AuthenticateUser.DTOs;

public class UserTokensDto
{
    public required string AccessToken { get; init; }
    public required DateTimeOffset AccessTokenExpiration { get; init; }
    public required DateTimeOffset CurrentTime { get; init; }
    
    public static UserTokensDto? Create(AuthenticateDto? authenticateDto)
    {
        if (authenticateDto == null) return default;
        
        return new UserTokensDto
        {
            AccessToken = authenticateDto.AccessToken,
            AccessTokenExpiration = authenticateDto.AccessTokenExpiration,
            CurrentTime = authenticateDto.CurrentTime
        };
    }
}
