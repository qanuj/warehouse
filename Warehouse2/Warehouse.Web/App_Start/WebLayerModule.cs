using Autofac;
using e10.Shared.Services.Abstraction;
using Warehouse.Web.Mailers;

namespace Warehouse.Web
{
    public class WebLayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Notifications>().As<INotificationService>();
        }
    }
}