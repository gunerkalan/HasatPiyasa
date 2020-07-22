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
        private   DbContext _context;        

        public   DbContext Context
        {
            get {
                
                if(_context==null)
                {
                    _context = new HasatPiyasaContext();
                    
                }

                return _context; 
            }

            set { _context = value; }
        }


        

        public TEntity Add(TEntity entity)
        {
           
                var addedEntity = Context.Entry(entity);
                addedEntity.State = EntityState.Added;
                Context.SaveChanges();
                return entity;
             
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            
                var addedEntity = Context.Entry(entity);
                addedEntity.State = EntityState.Added;
                await Context.SaveChangesAsync();
                return entity;
            
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                Context.Set<TEntity>().AddRange(entities);
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
             
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> filter = null)
        {
             
                return filter == null
              ? await Context.Set<TEntity>().CountAsync()
              : await Context.Set<TEntity>().Where(filter).CountAsync();
             
        }

        public void Delete(TEntity entity)
        {
            
                var deletedEntity = Context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                Context.SaveChanges();
            
        }

        public async Task DeleteAsync(TEntity entity)
        {
             
                var deletedEntity = Context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                await Context.SaveChangesAsync();
             
        }

        public async Task DeleteRange(IEnumerable<TEntity> entities)
        {
            
                Context.Set<TEntity>().RemoveRange(entities);
                await Context.SaveChangesAsync();
            
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter = null)
        { 
                return Context.Set<TEntity>().SingleOrDefault(filter);
            
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
             
                return filter == null
                    ? Context.Set<TEntity>().ToList()
                    : Context.Set<TEntity>().Where(filter).ToList();
             
        }

        public async Task<IQueryable<TEntity>> GetTable()
        {
             
                return Context.Set<TEntity>().AsQueryable();
            
        }

        public TEntity Update(TEntity entity)
        {
            
                var updatedEntity = Context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                Context.SaveChanges();
                return entity;
             
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
             
                var updatedEntity = Context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                await Context.SaveChangesAsync();
                return entity;
             
        }
    }
}
