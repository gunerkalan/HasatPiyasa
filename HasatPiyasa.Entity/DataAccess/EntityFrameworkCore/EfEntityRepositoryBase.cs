using HasatPiyasa.Entity.DataAccess.Entities;
using HasatPiyasa.Entity.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace HasatPiyasa.Entity.DataAccess.EntityFrameworkCore
{
    public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
        where TEntity : BaseEntity, new()

    {
        private DbContext _context;

        public EfEntityRepositoryBase()
        {

        }
        public EfEntityRepositoryBase(DbContext context)
        {
            _context = context;
        }

        public TEntity Add(TEntity entity)
        {
           
                var addedEntity = _context.Entry(entity);
                addedEntity.State = EntityState.Added;
                _context.SaveChanges();
                return entity;
             
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            
                var addedEntity = _context.Entry(entity);
                addedEntity.State = EntityState.Added;
                await _context.SaveChangesAsync();
                return entity;
            
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        { 
                _context.Set<TEntity>().AddRange(entities);
                await _context.SaveChangesAsync();
             
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> filter = null)
        {
             
                return filter == null
              ? await _context.Set<TEntity>().CountAsync()
              : await _context.Set<TEntity>().Where(filter).CountAsync();
             
        }

        public void Delete(TEntity entity)
        {
            
                var deletedEntity = _context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                _context.SaveChanges();
            
        }

        public async Task DeleteAsync(TEntity entity)
        {
             
                var deletedEntity = _context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                await _context.SaveChangesAsync();
             
        }

        public async Task DeleteRange(IEnumerable<TEntity> entities)
        {
            
                _context.Set<TEntity>().RemoveRange(entities);
                await _context.SaveChangesAsync();
            
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter = null)
        { 
                return _context.Set<TEntity>().SingleOrDefault(filter);
            
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
             
                return filter == null
                    ? _context.Set<TEntity>().ToList()
                    : _context.Set<TEntity>().Where(filter).ToList();
             
        }

        public async Task<IQueryable<TEntity>> GetTable()
        {
             
                return _context.Set<TEntity>().AsQueryable();
            
        }

        public TEntity Update(TEntity entity)
        {
            
                var updatedEntity = _context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                _context.SaveChanges();
                return entity;
             
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
             
                var updatedEntity = _context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return entity;
             
        }
    }
}
