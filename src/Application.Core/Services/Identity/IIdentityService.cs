using Application.Core.Responses;
using Application.Core.Services.Identity.DTOs;
using ThiIsFine.Domain.Entities.User;

namespace Application.Core.Services.Identity;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<Result<IThisIsFineUser?>> CreateUserAsync(RegisterUserDto registerUserDto,
        CancellationToken cancellationToken = default);

    Task<bool> IsInRoleAsync(string userId, string role);
    Task<bool> AuthorizeAsync(string userId, string policyName);
    Task<Result> DeleteUserAsync(string userId);
    Task<string?> GetUserIdByEmail(string email);

    Task<Result<AuthenticateDto>> AuthenticateUser(string username, string password,
        CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<IThisIsFineUser>>> GetAllUsers(CancellationToken cancellationToken = default);
    Task<Result> CheckIfUserExists(string requestUserId, CancellationToken cancellationToken = default);
    Task<Result<IThisIsFineUser>> GetUserById(string? userId, CancellationToken cancellationToken = default);

    Task<Result<IThisIsFineUser>> UpdateUserAsync(IThisIsFineUser updatedUser,
        CancellationToken cancellationToken = default);
}
