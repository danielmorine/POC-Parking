using Infrastructure.Schemas;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Script
{
    public static class UserRoleScript
    {
        public static IdentityUserRole<string> UserRole(IdentityRole role, ApplicationUser user)
        {
            return new IdentityUserRole<string> { UserId = user.Id, RoleId = role.Id.ToString() };
        }
    }
}
 