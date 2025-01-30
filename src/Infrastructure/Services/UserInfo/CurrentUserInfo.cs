using System.Security.Claims;
using Application.Core.Services.UserInfo;
using Domain.Core.Constants;
using Microsoft.AspNetCore.Http;

namespace ThiIsFine.Infrastructure.Services.UserInfo;

public class CurrentUserInfo : ICurrentUserInfo
{
    private string? _id;
    private string? _username;
    private string? _role;

    private readonly IHttpContextAccessor _context;

    public CurrentUserInfo(IHttpContextAccessor context)
    {
        _context = context;
    }

    public string Id => _id ??= GetClaimFromContext(_context, UserClaim.NameIdentifier)
                                       ?? throw new InvalidOperationException();
    public string? UserName => _username ??= GetClaimFromContext(_context, UserClaim.Name);
    public string? Role => _role ??= GetClaimFromContext(_context, UserClaim.Role);

    private static string? GetClaimFromContext(IHttpContextAccessor context, string claim)
    {
        return context.HttpContext?.User.FindFirstValue(claim);
    }
}
