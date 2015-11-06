using System.Data.Entity;
using Warehouse.Data.Core;
using e10.Shared.Data.Abstraction;
using e10.Shared.Repository;

namespace Warehouse.Data.Repository
{
    public interface IConfigRepository : IConfigRepository<AppSiteConfig>
    {

    }

    public class ConfigRepository : ConfigRepository<AppSiteConfig>, IConfigRepository
    {
        public ConfigRepository(DbContext context, IEventManager eventManager) : base(context, eventManager)
        {
        }
    }
}
