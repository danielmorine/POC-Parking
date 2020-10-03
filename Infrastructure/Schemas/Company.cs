using Infrastructure.Schemas.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Infrastructure.Schemas
{
    public class Company : ISchema<Guid>
    {
        public Guid ID { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public string Phone { get; set; }
        public short QtdCars { get; set; }
        public short QtdMotorcycles { get; set; }

        public virtual ICollection<Parking> Parkings { get; set; } = new HashSet<Parking>();

        public virtual Vehicle Vehicle { get; set; }

        public virtual ICollection<UserCompany> UserCompanies { get; set; } = new HashSet<UserCompany>();
    }
}
