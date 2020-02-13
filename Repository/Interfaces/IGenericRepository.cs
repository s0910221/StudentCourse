using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           bool disableTracking = true);

        IQueryable<T> GetList(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true);

        Task<EntityEntry<T>> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        Task AddAsync(params T[] entities);

        Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken));

        void Update(T entity);

        void Delete(T entity);

        void Delete(IEnumerable<T> entities);
    }
}
