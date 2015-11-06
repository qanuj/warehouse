using System.Data.Entity;
using Autofac;
using Warehouse.Data.Repository;
using Warehouse.Data.Services;
using e10.Shared;
using e10.Shared.Data;
using e10.Shared.Data.Abstraction;
using e10.Shared.Repository;
using e10.Shared.Services.Abstraction;
using Warehouse.Data.Core;

namespace Warehouse.Data
{
    public class DataAccessLayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<SharedLayerModule>();
            builder.RegisterType<DefaultEventManager>().As<IEventManager>();
            builder.RegisterType<ApplicationDataContext>().As<ApplicationDbContext>().As<DbContext>().InstancePerRequest();

            builder.RegisterType<ConfigRepository>().As<IConfigRepository>();
            builder.RegisterType<WebSiteService>().As<IWebSiteService>().As<ISiteService>();
            builder.RegisterType<WebMemberService>().As<IWebMemberService>();
            builder.RegisterType<SystemService>().As<ISystemService>();

            builder.RegisterType<ConfigRepository>().As<IConfigRepository>();
            builder.RegisterType<ActorRepository>().As<IActorRepository>().As<IMemberRepository<Actor>>();
        }
    }
}
