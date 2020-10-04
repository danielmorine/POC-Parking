using Infrastructure.Schemas;
using System;

namespace Infrastructure.Script
{
    public static class CompanyScript
    {
        public static Company GetCompany()
        {
            return new Company
            {
                Name = "park-estacionamentos",
                CNPJ = "cnpj",
                Phone = "988027555",
                QtdCars = 10,
                ID = Guid.Parse("862dfd76-5dda-42e1-8c07-2234e11e9724"),
                CreatedDate = DateTimeOffset.UtcNow,
                Address = "Rua A 343"                
            };
        }
    }
}
