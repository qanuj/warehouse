using System.Data.Entity;
using e10.Shared.Data.Abstraction;

namespace e10.Shared.Repository
{
    public interface ICountryRepository : IRepository<Country>
    {
    }
    public class CountryRepository : EfRepository<Country>, ICountryRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="eventManager"></param>
        public CountryRepository(DbContext context, IEventManager eventManager)
            : base(context, eventManager)
        {
        }

        public static void Register(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasKey(x => x.Id);
        }
    }
}