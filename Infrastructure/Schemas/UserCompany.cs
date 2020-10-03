using Infrastructure.Schemas.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Schemas
{
    public class UserCompany : ISchema<Guid>
    {
        public Guid CompanyID { get; set; }
        public Guid UserID { get; set; }

        [NotMapped]
        public Guid ID { get; set; }

        [NotMapped]
        public DateTimeOffset CreatedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        public virtual Company Company { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
