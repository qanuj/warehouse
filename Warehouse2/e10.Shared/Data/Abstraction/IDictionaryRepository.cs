using System.Collections.Generic;
using System.Linq;

namespace e10.Shared.Data.Abstraction
{
    public interface IDictionaryRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        IQueryable<TEntity> ByCode(IEnumerable<string> codes);
        TEntity ByCode(string code);

        IQueryable<TEntity> ByTitle(IEnumerable<string> titles);
        TEntity ByTitle(string title);
    }
}