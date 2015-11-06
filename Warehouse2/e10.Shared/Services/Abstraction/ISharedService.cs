using System.Linq;
using e10.Shared.Data.Abstraction;
using e10.Shared.Models;

namespace e10.Shared.Services.Abstraction
{
    public interface ISharedService : ISecuredService
    {
        IQueryable<TransactionViewModel> Transactions();
        string AddCredits(int num, string userId="");
        int GetBalance(string userId="");
    }
}