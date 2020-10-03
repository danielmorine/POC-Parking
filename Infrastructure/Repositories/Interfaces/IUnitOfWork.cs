using System;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
    }
}
