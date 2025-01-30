using Microsoft.AspNetCore.Identity;
using ThiIsFine.Domain.Entities.User;

namespace ThiIsFine.Infrastructure.Identity;

public class ThisIsFineUser : IdentityUser<string>, IThisIsFineUser
{
    public DateTimeOffset? CreationTime { get; set; }
    public string? CreatorUserId { get; set; }
    public string? LastModifierUserId { get; set; }
    public DateTimeOffset? LastModificationTime { get; set; }
    public string? DeleterUserId { get; set; }
    public DateTimeOffset? DeletionTime { get; set; }
    
    public int? FreeTrialAttempts { get; set; }
}
