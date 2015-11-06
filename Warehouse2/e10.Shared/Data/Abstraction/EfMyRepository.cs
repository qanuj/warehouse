using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace e10.Shared.Data.Abstraction
{
    public abstract class EfMyRepository<TEntity> : EfRepository<TEntity>, IMyRepository<TEntity> where TEntity : Entity
    {
       

        protected EfMyRepository(DbContext db, IEventManager eventManager) : base(db, eventManager) { }
        public TEntity MyOne(string userId, int id)
        {
            return Mine(userId).FirstOrDefault(x => x.Id == id);
        }
        public Task<TEntity> MyOneAsync(string userId, int id)
        {
            return Mine(userId).FirstOrDefaultAsync(x => x.Id == id);
        }
        public abstract IQueryable<TEntity> Mine(string id);
        public virtual int Count(string userId, Func<TEntity, bool> func)
        {
            return Mine(userId).Count(func);
        }

        public virtual int Count(string userId)
        {
            return Mine(userId).Count();
        }

    }
}