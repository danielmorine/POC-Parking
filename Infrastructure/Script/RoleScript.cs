using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Script
{
    public static class RoleScript
    {
        public static IEnumerable<IdentityRole> Roles()
        {
            return new List<IdentityRole>
            {
                new IdentityRole { ConcurrencyStamp = Guid.NewGuid().ToString(), Id = Guid.NewGuid().ToString(), Name = "ADMINISTRATOR", NormalizedName = "ADMINISTRATOR" },
                new IdentityRole { ConcurrencyStamp = Guid.NewGuid().ToString(), Id = Guid.NewGuid().ToString(), Name = "USER", NormalizedName = "USER" }
            };
        }
    }
}
