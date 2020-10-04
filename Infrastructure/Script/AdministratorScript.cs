using Infrastructure.Schemas;
using Microsoft.AspNetCore.Identity;
using System;

namespace Infrastructure.Script
{
    public static class AdministratorScript
    {
        public static ApplicationUser ApplicationUser()
        {
            var pass = "Teste@123";

            var id = Guid.NewGuid();
            var user =  new ApplicationUser
            {
                Email = "admin@teste.com.br",
                UserName = "admin@teste.com.br",
                CreatedDate = DateTimeOffset.UtcNow,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
                ID = id,
                Id = id.ToString(),                
                NormalizedUserName = "ADMIN@TESTE.COM.BR",
                NormalizedEmail = "ADMIN@TESTE.COM.BR",

            };

            var hasher = new PasswordHasher<ApplicationUser>().HashPassword(user, pass);

            user.PasswordHash = hasher;

            return user;
        }
    }
}
