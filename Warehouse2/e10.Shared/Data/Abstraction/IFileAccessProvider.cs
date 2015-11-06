using System.Threading.Tasks;

namespace e10.Shared.Data.Abstraction
{
    public interface IFileAccessProvider
    {
        FileAccessInfo ByUrl(string userId, string filepath);
        FileAccessInfo ByUrl(string filepath);
    }
}