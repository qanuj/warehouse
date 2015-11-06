using Autofac;
using Warehouse.Core;

namespace Warehouse.Services
{
    public class ServiceLayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WarehouseService>().As<IWarehouseService>();
        }
    }
}