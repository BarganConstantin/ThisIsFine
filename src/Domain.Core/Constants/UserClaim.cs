using System.Security.Claims;

namespace Domain.Core.Constants
{
    public static class UserClaim
    {
        public const string NameIdentifier = ClaimTypes.NameIdentifier;
        public const string Name = ClaimTypes.Name;
        public const string Role = ClaimTypes.Role;
        public const string LibraryIdentifier = "lib";
    }
}
