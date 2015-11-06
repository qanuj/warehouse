using System.Threading.Tasks;

namespace e10.Shared.Security
{
    public interface IUserService 
    {
        Task<string> CreateAsync(string email, string password, string role);
    }
}