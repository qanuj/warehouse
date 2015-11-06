using Autofac;
using e10.Data;
namespace Warehouse.Services
{
    public class ServiceLayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DataAccessLayerModule>();
        }
    }
}
