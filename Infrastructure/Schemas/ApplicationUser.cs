using Infrastructure.Schemas.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Schemas
{
    public class ApplicationUser : IdentityUser, ISchema<Guid>
    {
        [NotMapped]
        public Guid ID { get { return Guid.Parse(this.Id); } set { this.Id = value.ToString(); } }
        public DateTimeOffset CreatedDate { get; set; }


        [NotMapped]
        public string PasswordComparer { get; set; }


        public virtual ICollection<UserCompany> UserCompanies { get; set; } = new HashSet<UserCompany>();

    }
}
