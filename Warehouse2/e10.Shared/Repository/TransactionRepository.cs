using System.Data.Entity;
using System.Linq;
using e10.Shared.Data.Abstraction;

namespace e10.Shared.Repository
{
    public class TransactionRepository : EfMyRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DbContext context, IEventManager eventManager)
            : base(context, eventManager)
        {
        }
        public Transaction ByCode(string code)
        {
            return All.Include(x=>x.User).FirstOrDefault(x => x.Code == code);
        }

        public IQueryable<Transaction> Full
        {
            get { return this.All.Include(x => x.User); }
        }

        public int Balance(string id)
        {
            return Mine(id).Any() ? Mine(id).Sum(x => x.Credit) : 0;
        }

        public IQueryable<Transaction> Completed(string userId)
        {
            return All.Include(x => x.User).Where(x => x.UserId == userId && x.IsSuccess);
        }

        public override IQueryable<Transaction> Mine(string id)
        {
            return All.Where(x => x.IsSuccess).Where(x => x.UserId == id);
        }

        public static void Register(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().HasKey(x => x.Id);
        }

    }
    public interface ITransactionRepository : IMyRepository<Transaction>
    {
        Transaction ByCode(string code);
        IQueryable<Transaction> Full { get; } 
        int Balance(string id);
        IQueryable<Transaction> Completed(string userId);
    }
}
