using Infrastructure.Schemas;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions.FluentAPI
{
    public static class ParkingFluentAPI
    {
        public static ModelBuilder ParkingModelBuilder(this ModelBuilder builder)
        {

            builder.Entity<Parking>(entity =>
            {
                entity.ToTable(nameof(Parking));
                entity.HasKey(x => x.ID);

                entity.Property(x => x.ID).HasColumnName($"{nameof(Parking)}ID").IsRequired();

                entity.Property(x => x.StartDate).IsRequired();
                entity.Property(x => x.CompanyID).IsRequired();
                entity.Property(x => x.CreatedDate).IsRequired();

                entity.HasOne(x => x.Vehicle).WithMany(x => x.Parkings).HasForeignKey(x => x.VehicleID).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(x => x.Company).WithMany(x => x.Parkings).HasForeignKey(x => x.CompanyID).OnDelete(DeleteBehavior.Restrict);

            });

            return builder;
        }
    }
}
