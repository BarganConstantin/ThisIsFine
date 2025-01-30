using Domain.Core.Entities.Interface;

namespace ThiIsFine.Domain.Entities.User;

public interface IThisIsFineUser : IFullAudited
{
    string? UserName { get; set; }
    string? Email { get; set; }
    string? PasswordHash { get; set; }
    int? FreeTrialAttempts { get; set; }
}
