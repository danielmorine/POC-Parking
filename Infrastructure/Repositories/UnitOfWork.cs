using Infrastructure.Interfaces;
using Infrastructure.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private bool _disposed = false;

        public UnitOfWork(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task CommitAsync()
        {
            if (_disposed) throw new ObjectDisposedException(this.GetType().FullName);
            await _applicationDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dipose(true);
        }

        protected virtual void Dipose(bool disposing) 
        {
            if (_disposed) return;

            if (disposing && _applicationDbContext != null)
                _applicationDbContext.Dispose();

            _disposed = true;
        }
    }
}
