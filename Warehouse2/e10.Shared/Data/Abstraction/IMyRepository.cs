using System;
using System.Linq;
using System.Threading.Tasks;

namespace e10.Shared.Data.Abstraction
{
    public interface IMyRepository<T> : IRepository<T> where T : Entity
    {
        T MyOne(string userId, int id);
        Task<T> MyOneAsync(string userId, int id);
        IQueryable<T> Mine(string id);
        int Count(string userId, Func<T, bool> func);
        int Count(string userId);
    }
}