using Application.Core.Services.Identity.DTOs;
using ThiIsFine.Domain.Entities.User;

namespace Application.Core.Services.TokenGenerator
{
    public interface ITokenService
    {
        Task<AccessTokenDto> GenerateToken(IThisIsFineUser user, CancellationToken cancellationToken = default);
        Task<RefreshTokenDto> CreateUserRefreshToken();
        Task<string?> ValidateAndGetUsername(string accessToken);
    }
}
