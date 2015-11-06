using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Core;

namespace Warehouse.Data.Respository
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, ISoftDelete
    {
        public EfRepository(DbContext context){
            Context = context;
        }

        protected DbContext Context { get; private set; }

        public IQueryable<TEntity> ById(IEnumerable<int> ids)
        {
            return All.Where(x => ids.Contains(x.Id));
        }

        public virtual IQueryable<TEntity> All
        {
            get
            {
                return Set.Where(x => !x.IsDeleted);
            }
        }

        private DbSet<TEntity> Set
        {
            get { return Context.Set<TEntity>(); }
        }

        protected void Attach(TEntity entity, EntityState state)
        {
            Set.Attach(entity);
            Context.Entry(entity).State = state;
        }


        protected int UpdateEntity(TEntity entity)
        {
            Set.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChanges();
        }

        protected int CreateEntity(TEntity entity)
        {
            Set.Attach(entity);
            Context.Entry(entity).State = EntityState.Added;
            return Context.SaveChanges();
        }

        public virtual void Create(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            if (entity is IState){
                UpdateCreateState(entity as IState);
            }
        }

        private void UpdateCreateState(IState state)
        {
            state.Created = DateTime.UtcNow;
            state.Modified = DateTime.UtcNow;
        }

        private void UpdateState(IState state)
        {
            state.Modified = DateTime.UtcNow;
        }

        public virtual TEntity ById(int id)
        {
            var entity = Set.Find(id);
            if (entity is ISoftDelete) if ((entity as ISoftDelete).IsDeleted) return null;
            return entity;
        }

        private void SoftDelete(ISoftDelete entity)
        {
            entity.IsDeleted = true;
            entity.Deleted = DateTime.UtcNow;
        }

        public virtual void Purge(TEntity entity)
        {
            Attach(entity);
            Context.Set<TEntity>().Remove(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            Attach(entity);
            if (entity is ISoftDelete)
            {
                SoftDelete(entity as ISoftDelete);
            }
            else
            {
                Context.Set<TEntity>().Remove(entity);
            }
        }

        public virtual void Update(TEntity entity)
        {
            Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            if (entity is IState)
            {
                UpdateState(entity as IState);
            }
        }

        protected void AttachDetachedCollection(ICollection<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Attach(entity);
                Context.Entry(entity).State = EntityState.Unchanged;
            }
        }

        public void Attach(TEntity entity)
        {
            Set.Attach(entity);
        }

        internal void Attach(ICollection<TEntity> entities)
        {
            AttachDetachedCollection(entities);
        }

        public virtual void Create(ICollection<TEntity> entities)
        {
            Attach(entities);
        }

        public virtual void Delete(ICollection<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            Delete(ById(id));
        }
    }
}
