using Infrastructure.Schemas.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Schemas
{
    public class Parking : ISchema<Guid>
    {
        [NotMapped]
        public Guid ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DateTimeOffset CreatedDate { get; set; }

        public Guid CompanyID { get; set; }
        public Guid VehicleID { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }

        public virtual Company Company { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
