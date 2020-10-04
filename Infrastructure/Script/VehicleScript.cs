using Infrastructure.Schemas;
using System;
using System.Collections.Generic;

namespace Infrastructure.Script
{
    public static class VehicleScript
    {
        public static IEnumerable<Vehicle> GetVehicles()
        {
            return new List<Vehicle>()
            {
                new Vehicle { Color = "Azul", CompanyID = Guid.Parse("862dfd76-5dda-42e1-8c07-2234e11e9724"), CreatedDate = DateTimeOffset.UtcNow, ID = Guid.NewGuid(), Make = "Fiat", Model = "Palio", Plate = "AHC1D23", TypeID = 1},
                new Vehicle { Color = "Preto", CompanyID = Guid.Parse("862dfd76-5dda-42e1-8c07-2234e11e9724"), CreatedDate = DateTimeOffset.UtcNow, ID = Guid.NewGuid(), Make = "Fiat", Model = "Uno", Plate = "JBC1D13", TypeID = 1},
                new Vehicle { Color = "Vermelho", CompanyID = Guid.Parse("862dfd76-5dda-42e1-8c07-2234e11e9724"), CreatedDate = DateTimeOffset.UtcNow, ID = Guid.NewGuid(), Make = "Ford", Model = "Focus", Plate = "MIQ1D66", TypeID = 1},
                new Vehicle { Color = "Prata", CompanyID = Guid.Parse("862dfd76-5dda-42e1-8c07-2234e11e9724"), CreatedDate = DateTimeOffset.UtcNow, ID = Guid.NewGuid(), Make = "Citroen", Model = "C3", Plate = "PGC3E99", TypeID = 1},
                new Vehicle { Color = "Prata", CompanyID = Guid.Parse("862dfd76-5dda-42e1-8c07-2234e11e9724"), CreatedDate = DateTimeOffset.UtcNow, ID = Guid.NewGuid(), Make = "Citroen", Model = "C4", Plate = "HQW-5678", TypeID = 1},
                new Vehicle { Color = "Prata", CompanyID = Guid.Parse("862dfd76-5dda-42e1-8c07-2234e11e9724"), CreatedDate = DateTimeOffset.UtcNow, ID = Guid.NewGuid(), Make = "Citroen", Model = "C6", Plate = "AAA-4444", TypeID = 1},
                new Vehicle { Color = "Prata", CompanyID = Guid.Parse("862dfd76-5dda-42e1-8c07-2234e11e9724"), CreatedDate = DateTimeOffset.UtcNow, ID = Guid.NewGuid(), Make = "Ford", Model = "Fusion", Plate = "BEE4R22", TypeID = 1},
                new Vehicle { Color = "Prata", CompanyID = Guid.Parse("862dfd76-5dda-42e1-8c07-2234e11e9724"), CreatedDate = DateTimeOffset.UtcNow, ID = Guid.NewGuid(), Make = "Ford", Model = "Ka", Plate = "JJD9K10", TypeID = 1},
            };
        }
    }
}
