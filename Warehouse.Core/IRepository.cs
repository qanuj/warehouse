using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Core
{
    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : IEntity
    {

    }

    public interface IRepository<TEntity, in TKey> where TEntity : IEntity
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();

        TEntity ById(TKey id);
        IQueryable<TEntity> ById(IEnumerable<TKey> ids);

        void Create(TEntity entity); //returns Rows Affected
        void Create(ICollection<TEntity> entity); //returns Rows Affected
        void Update(TEntity entity); //returns Rows Affected
        void Delete(TEntity entity); //returns Rows Affected
        void Delete(TKey id); //returns Rows Affected
        void Attach(TEntity entity); //returns Rows Affected
        void Delete(ICollection<TEntity> entities); //returns Rows Affected
        void Purge(TEntity entity);

        IQueryable<TEntity> All { get; }
    }
}