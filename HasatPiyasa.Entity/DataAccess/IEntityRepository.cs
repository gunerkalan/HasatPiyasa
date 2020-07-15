using HasatPiyasa.Entity.DataAccess.Entities;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HasatPiyasa.Entity.DataAccess
{
    public interface IEntityRepository<T> where T : IEntity, new()
    {
        T Add(T entity);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        Task<T> UpdateAsync(T entity);
        void Delete(T entity);
        Task DeleteAsync(T entity);
        T Get(Expression<Func<T, bool>> filter = null);
        IEnumerable<T> GetList(Expression<Func<T, bool>> filter = null);
        Task<int> Count(Expression<Func<T, bool>> filter = null);
        Task AddRange(IEnumerable<T> entities);
        Task DeleteRange(IEnumerable<T> entities);
        Task<IQueryable<T>> GetTable();
    }
}
