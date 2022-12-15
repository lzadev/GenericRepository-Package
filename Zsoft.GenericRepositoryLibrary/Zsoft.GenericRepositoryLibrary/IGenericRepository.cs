using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Zsoft.GenericRepositoryLibrary
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAllWithIncludes(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> GetAllWithIncludes(Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> FindByIdAsync(object Id);

        TEntity FindById(object Id);

        TEntity Insert(TEntity entity);

        Task<TEntity> InsertAsync(TEntity entity);

        void InsertMany(IEnumerable<TEntity> entities);

        Task InsertManyAsync(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);

        void DeleteMany(IEnumerable<TEntity> entities);

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
