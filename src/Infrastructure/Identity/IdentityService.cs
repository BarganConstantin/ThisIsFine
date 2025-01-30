using Application.Core.Responses;
using Application.Core.Responses.Enum;
using Application.Core.Services.Identity;
using Application.Core.Services.Identity.DTOs;
using Application.Core.Services.TokenGenerator;
using Domain.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThiIsFine.Domain.Entities.User;

namespace ThiIsFine.Infrastructure.Identity;

public class IdentityService(
    UserManager<ThisIsFineUser> userManager, 
    IUserClaimsPrincipalFactory<ThisIsFineUser> userClaimsPrincipalFactory, 
    IAuthorizationService authorizationService,
    ITokenService tokenService) 
    : IIdentityService
{
    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<Result<IThisIsFineUser?>> CreateUserAsync(RegisterUserDto registerUserDto, 
        CancellationToken cancellationToken = default)
    {
        var user = new ThisIsFineUser
        {
            UserName = registerUserDto.UserName,
            Email = registerUserDto.Email,
            CreationTime = DateTimeOffset.Now,
            FreeTrialAttempts = 3,
        };
        
        var result = await userManager.CreateAsync(user, registerUserDto.Password);
        if (!result.Succeeded) return new Result<IThisIsFineUser?>()
            { Message = result.Errors.First().Description, ResultStatus = ResultStatus.BadRequest, };
                  
        await userManager.AddToRoleAsync(user, Roles.User);
        return new Result<IThisIsFineUser?>() { Data = user, ResultStatus = ResultStatus.Created, };
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        var principal = await userClaimsPrincipalFactory.CreateAsync(user);

        var result = await authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null) return new Result() { Message = "User not found", ResultStatus = ResultStatus.NotFound, };
        
        var result = await userManager.DeleteAsync(user);
        return !result.Succeeded 
            ? new Result() { Message = "User could not be deleted", ResultStatus = ResultStatus.InternalServerError, } 
            : new Result() { Message = "User deleted successfully", ResultStatus = ResultStatus.Success, };
    }
    
    public async Task<string?> GetUserIdByEmail(string email)
    {
        var result = await userManager.Users.SingleOrDefaultAsync(u => u.Email == email);
        return result?.Id;
    }

    public async Task<IThisIsFineUser?> GetUserByEmail(string email)
    {
        return await userManager.Users.SingleOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Result<AuthenticateDto>> AuthenticateUser(string username, string password, 
        CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user == null) return new Result<AuthenticateDto>() { Message = "User not found", ResultStatus = ResultStatus.NotFound };
        
        var validPassword = await userManager.CheckPasswordAsync(user, password);
        if (!validPassword) return new Result<AuthenticateDto>() { Message = "Incorrect username or password", ResultStatus = ResultStatus.Forbidden };
        
        
        var token = await tokenService.GenerateToken(user, cancellationToken);
        
        await userManager.UpdateAsync(user);
        
        var result = new AuthenticateDto()
        {
            AccessToken = token.AccessToken,
            AccessTokenExpiration = token.Expiration,
            CurrentTime = DateTimeOffset.Now
        };
        
        return new Result<AuthenticateDto>() { Data = result, ResultStatus = ResultStatus.Success };
    }
    
    public async Task<Result<IEnumerable<IThisIsFineUser>>> GetAllUsers(CancellationToken cancellationToken)
    {
        var users = await userManager.Users.ToListAsync(cancellationToken);
        return new Result<IEnumerable<IThisIsFineUser>>() { Data = users, ResultStatus = ResultStatus.Success };
    }

    public Task<Result> CheckIfUserExists(string requestUserId, CancellationToken cancellationToken = default)
    {
        var user = userManager.Users.FirstOrDefault(u => u.Id == requestUserId);
        return user == null 
            ? Task.FromResult(new Result() { Message = "User not found", ResultStatus = ResultStatus.NotFound }) 
            : Task.FromResult(new Result() { ResultStatus = ResultStatus.Success });
    }

    public async Task<Result<IThisIsFineUser>> GetUserById(string? userId, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        return user == null 
            ? Result.NotFound<IThisIsFineUser>("User not found")
            : Result.Success<IThisIsFineUser>(user);
    }

    public Task<Result<IThisIsFineUser>> UpdateUserAsync(IThisIsFineUser updatedUser, CancellationToken cancellationToken = default)
    {
        var user = userManager.Users.FirstOrDefault(u => u.Id == updatedUser.Id);
        if (user == null) return Task.FromResult(Result.BadRequest<IThisIsFineUser>("User not found"));
        
        user.UserName = updatedUser.UserName;
        user.Email = updatedUser.Email;
        user.FreeTrialAttempts = updatedUser.FreeTrialAttempts;
        
        var result = userManager.UpdateAsync(user);
        
        return result.Result.Succeeded 
            ? Task.FromResult(Result.Success<IThisIsFineUser>(user))
            : Task.FromResult(Result.BadRequest<IThisIsFineUser>("Error updating user"));
    }
}
