using System;
using System.Collections.Generic;

namespace Infrastructure.Script
{
    public static class TypeScript
    {
        public static List<Schemas.Type>  GetTypes()
        {
            return new List<Schemas.Type>
            {
                new Schemas.Type { CreatedDate = DateTimeOffset.UtcNow, ID = 1, Name = "Carro" },
                new Schemas.Type { CreatedDate = DateTimeOffset.UtcNow, ID = 2, Name = "Moto" }
            };
        }
    }
}
