using Infrastructure.Interfaces;
using Infrastructure.Queries.Interfaces;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Schemas.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T: class, ISchema
    {
        protected readonly IApplicationDbContext _db;
        private bool disposed = false;

        public RepositoryBase(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<T> AddAsync(T schema)
        {
            return (await _db.Set<T>().AddAsync(schema)).Entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> schemas)
        {
            await _db.Set<T>().AddRangeAsync(schemas);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _db.Set<T>().AsNoTracking().AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _db.Set<T>().AsNoTracking().CountAsync(predicate);
        }

        public void Delete(T schema)
        {
            _db.Set<T>().Remove(schema);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false, params string[] includes)
        {
            var query = _db.Set<T>().AsQueryable();

            if (asNoTracking)
            {
                query = _db.Set<T>().AsNoTracking().AsQueryable();
            }

            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                    query = query.Include(includes[i]);
            }

            if (predicate != null)
            {
                return await query.FirstOrDefaultAsync(predicate);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<U>> GetAllAsync<U>(Expression<Func<T, bool>> condiction, Expression<Func<T, U>> selection, bool asNoTracking = false, int take = 10, int skip = 1, params string[] includes) where U : IQuery
        {
            var query = _db.Set<T>().AsNoTracking().AsQueryable();

            if (asNoTracking)
            {
                query = _db.Set<T>().AsNoTracking().AsQueryable();
            }

            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                    query = query.Include(includes[i]);
            }

            var calcSkip = (take * (skip - 1));

            if (condiction == null)
            {
                return await query
                    .Select(selection)
                    .Skip(calcSkip)
                    .Take(take)
                    .ToListAsync();
            }

            return await query.Where(condiction).Select(selection).Skip(calcSkip).Take(take).ToListAsync();
        }

        public async Task SaveChangeAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Update(T schema)
        {
            _db.Set<T>().Update(schema);
        }

        protected virtual void Dispose(bool disposing) 
        {
            if (!this.disposed)
                if (disposing)
                    if (_db != null)
                        _db.Dispose();
            
            disposed = true;
        }
    }
}
