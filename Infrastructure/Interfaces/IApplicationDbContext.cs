using Infrastructure.Schemas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ApplicationUser> ApplicationUser { get; set; }
        DbSet<Company> Company { get; set; }
        DbSet<Parking> Parking { get; set; }
        DbSet<Type> Type { get; set; }
        DbSet<UserCompany> UserCompany { get; set; }
        DbSet<Vehicle> Vehicles { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry Entry(object entity);
        DbSet<T> Set<T>() where T : class;
        void Dispose();
    }
}
