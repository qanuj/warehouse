using System.Threading.Tasks;

namespace e10.Shared.Services.Abstraction
{
    public interface IDemoDataService
    {
        Task<string> BuildAsync();
        void BuildMaster();
    }
}