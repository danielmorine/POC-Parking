using Infrastructure.Schemas;
using Microsoft.AspNetCore.Identity;
using System;

namespace Infrastructure.Script
{
    public static class UserScript
    {
        public static ApplicationUser ApplicationUser()
        {
            var pass = "Teste@123";

            var id = Guid.NewGuid();
            var user = new ApplicationUser
            {
                Email = "usuario@teste.com.br",
                UserName = "usuario@teste.com.br",
                CreatedDate = DateTimeOffset.UtcNow,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
                ID = id,
                Id = id.ToString(),
                NormalizedUserName = "USUARIO@TESTE.COM.BR",
                NormalizedEmail = "USUARIO@TESTE.COM.BR",

            };

            var hasher = new PasswordHasher<ApplicationUser>().HashPassword(user, pass);

            user.PasswordHash = hasher;

            return user;
        }
    }
}
