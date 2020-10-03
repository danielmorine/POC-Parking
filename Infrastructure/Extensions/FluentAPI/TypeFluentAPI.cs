
using Infrastructure.Schemas;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions.FluentAPI
{
    public static class TypeFluentAPI
    {
        public static ModelBuilder TypeModelBuilder(this ModelBuilder builder)
        {
            builder.Entity<Type>(entity => 
            {
                entity.ToTable(nameof(Type));

                entity.HasKey(x => x.ID);

                entity.Property(x => x.ID).HasColumnType($"{nameof(Type)}ID").IsRequired();
                entity.Property(x => x.Name).HasColumnType("VARCHAR(10)").HasMaxLength(10).IsRequired();
                
            });

            return builder;
        }
    }
}
