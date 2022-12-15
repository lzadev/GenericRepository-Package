using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Zsoft.GenericRepositoryLibrary
{
    public class GenericRepository<TContext, TEntity> : IGenericRepository<TEntity> where TEntity : class where TContext : DbContext
    {
        private readonly TContext _context;

        public GenericRepository(TContext context)
        {
            _context = context;
        }

        protected DbSet<TEntity> Table => _context.Set<TEntity>();
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await Table.ToListAsync();
        public virtual IEnumerable<TEntity> GetAll() => Table;
        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate) => await Table.Where(predicate).ToListAsync();
        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate) => Table.Where(predicate).ToList();
        public virtual IEnumerable<TEntity> GetAllWithIncludes(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>[] includes)
        {
            var query = Table.Where(predicate);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).ToList();
        }

        public virtual IEnumerable<TEntity> GetAllWithIncludes(Expression<Func<TEntity, object>>[] includes)
        {
            var query = Table.AsQueryable();
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(Expression<Func<TEntity, object>>[] includes)
        {
            var query = Table.AsQueryable();
            return await includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>[] includes)
        {
            var query = Table.Where(predicate);
            return await includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).ToListAsync();
        }

        public virtual async Task<TEntity> FindByIdAsync(object Id) => await Table.FindAsync(Id);

        public virtual TEntity FindById(object Id) => Table.Find(Id);

        public virtual TEntity Insert(TEntity entity)
        {
            Table.Add(entity);
            return entity;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
            return entity;
        }

        public virtual void InsertMany(IEnumerable<TEntity> entities)
        {
            Table.AddRange(entities);
        }

        public virtual async Task InsertManyAsync(IEnumerable<TEntity> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public virtual TEntity Update(TEntity entity)
        {
            Table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public virtual void Delete(TEntity entity)
        {
            Table.Remove(entity);
        }

        public virtual void DeleteMany(IEnumerable<TEntity> entities)
        {
            Table.AddRange(entities);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
