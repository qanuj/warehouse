using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace e10.Shared.Data.Abstraction
{
    public abstract class EfDictionaryRepository<TDictionary> : EfRepository<TDictionary>, IDictionaryRepository<TDictionary> where TDictionary : Dictionary
    {
        protected EfDictionaryRepository(DbContext context, IEventManager eventManager) : base(context, eventManager)
        {
        }
        public IQueryable<TDictionary> ByCode(IEnumerable<string> codes)
        {
            return All.Where(x => codes.Contains(x.Code));
        }
        public TDictionary ByCode(string code)
        {
            return All.FirstOrDefault(x => x.Code == code);
        }

        public IQueryable<TDictionary> ByTitle(IEnumerable<string> titles)
        {
            return All.Where(x => titles.Contains(x.Title));
        }
        public TDictionary ByTitle(string title)
        {
            return All.FirstOrDefault(x => x.Title == title);
        }
    }
}