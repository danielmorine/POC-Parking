using Infrastructure.Schemas.Interfaces;
using System;
using System.Collections.Generic;

namespace Infrastructure.Schemas
{
    public class Vehicle : ISchema<Guid>
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Plate { get; set; }
        public byte TypeID { get; set; }
        public Guid ID { get; set; }
        public Guid CompanyID { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public virtual Type Type { get; set; }

        public virtual ICollection<Parking> Parkings { get; set; } = new HashSet<Parking>();

        public virtual Company Company { get; set; }
    }
}
