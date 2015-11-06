using System;
using System.Data.Entity;
using System.Linq;
using e10.Shared.Data.Abstraction;

namespace e10.Shared.Repository
{
    public interface IConfigRepository<T> : IRepository<T> where T:SiteConfig
    {
        T Config();
    }
    public abstract class ConfigRepository<T> : EfRepository<T>, IConfigRepository<T> where T: SiteConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="eventManager"></param>
        protected ConfigRepository(DbContext context, IEventManager eventManager)
            : base(context, eventManager)
        {
        }

        public static void Register(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T>().HasKey(x => x.Id);
        }

        public T Config()
        {
            var config = All.OfType<T>().FirstOrDefault();
            if (config == null)
            {
                Create(Activator.CreateInstance<T>());
                SaveChanges();
            }
            return config;
        }

    }
}