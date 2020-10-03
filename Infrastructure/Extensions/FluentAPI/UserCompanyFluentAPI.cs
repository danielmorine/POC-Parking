using Infrastructure.Schemas;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions.FluentAPI
{
    public static class UserCompanyFluentAPI
    {
        public static ModelBuilder UserCompanyModelBuilder(this ModelBuilder builder)
        {
            builder.Entity<UserCompany>(entity => 
            {
                entity.ToTable(nameof(UserCompany));

                entity.HasKey(x => new { x.CompanyID, x.UserID });

                entity.Property(x => x.UserID).IsRequired();
                entity.Property(x => x.CompanyID).IsRequired();


                entity.HasOne(x => x.Company).WithMany(x => x.UserCompanies).HasForeignKey(x => x.CompanyID).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(x => x.ApplicationUser).WithMany(x => x.UserCompanies).HasForeignKey(x => x.UserID).OnDelete(DeleteBehavior.Restrict);

            });

            return builder;
        }
    }
}
