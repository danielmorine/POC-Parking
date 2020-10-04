using Infrastructure.Schemas;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions.FluentAPI
{
    public static class CompanyFluentAPI
    {
        public static ModelBuilder CompanyModelBuilder(this ModelBuilder builder)
        {
            builder.Entity<Company>(entity =>
            {
                entity.ToTable(nameof(Company));

                entity.HasKey(x => x.ID);

                entity.Property(x => x.ID).HasColumnName($"{nameof(Company)}ID").IsRequired();

                entity.Property(x => x.CNPJ).HasColumnType("VARCHAR(18)").HasMaxLength(18).IsRequired();
                entity.Property(x => x.CreatedDate).IsRequired();
                entity.Property(x => x.Name).HasColumnType("VARCHAR(120)").HasMaxLength(120).IsRequired();
                entity.Property(x => x.Address).HasColumnType("VARCHAR(200)").HasMaxLength(200).IsRequired();
                entity.Property(x => x.Phone).HasColumnType("VARCHAR(11)").HasMaxLength(11).IsRequired();
                entity.Property(x => x.QtdCars).IsRequired();
                entity.Property(x => x.QtdMotorcycles).IsRequired();

            });

            return builder;
        }
    }
}
