using Infrastructure.Queries.Interfaces;
using Infrastructure.Schemas.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T: class, ISchema
    {
        Task<IEnumerable<U>> GetAllAsync<U>(Expression<Func<T, bool>> condiction,
            Expression<Func<T, U>> selection,
            bool asNoTracking = false,
            int take = 10, 
            int skip = 1, 
            params string[] includes) where U : IQuery;

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, 
            bool asNoTracking = false,
            params string[] inclues);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, params string[] includes);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate, params string[] includes);

        Task<T> AddAsync(T schema);
        Task AddRangeAsync(IEnumerable<T> schemas);
        void Delete(T schema);
        void Update(T schema);
        Task SaveChangeAsync();
    }
}
