using Infrastructure.Schemas;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Helpers
{
    public class PasswordHasherHelper
    {
        public string PasswordHash(ApplicationUser user, string password)
        {
            return new PasswordHasher<ApplicationUser>().HashPassword(user, password);
        }
    }
}
