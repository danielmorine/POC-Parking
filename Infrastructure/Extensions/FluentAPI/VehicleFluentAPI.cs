using Infrastructure.Schemas;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions.FluentAPI
{
    public static class VehicleFluentAPI
    {
        public static ModelBuilder VehicleModelBuilder(this ModelBuilder builder)
        {
            builder.Entity<Vehicle>(entity => 
            {
                entity.ToTable(nameof(Vehicle));

                entity.HasKey(x => x.ID);

                entity.Property(x => x.ID).HasColumnName($"{nameof(Vehicle)}ID").IsRequired();

                entity.Property(x => x.Make).HasColumnType("VARCHAR(40)").HasMaxLength(40).IsRequired();
                entity.Property(x => x.Color).HasColumnType("VARCHAR(40)").HasMaxLength(40).IsRequired();
                entity.Property(x => x.CreatedDate).IsRequired();
                entity.Property(x => x.Plate).HasColumnType("VARCHAR(7)").HasMaxLength(7).IsRequired();
                entity.Property(x => x.Model).HasColumnType("VARCHAR(100)").HasMaxLength(100).IsRequired();
                entity.Property(x => x.TypeID).IsRequired();
                entity.Property(x => x.CompanyID).IsRequired();

                entity.HasOne(x => x.Company).WithMany(x => x.Vehicles).HasForeignKey(x => x.CompanyID).OnDelete(DeleteBehavior.Restrict);

            });
            return builder;
        }
    }
}
