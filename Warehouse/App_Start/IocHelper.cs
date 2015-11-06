using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using Warehouse.Data;
using Warehouse.Services;

namespace Warehouse
{
    public class IocHelper
    {
        public static IContainer CreateContainer(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<ServiceLayerModule>();
            builder.RegisterModule<WebLayerModule>();
            builder.RegisterModule<DataAccessModule>();

            builder.Register(c => app.GetDataProtectionProvider()).InstancePerRequest();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();

            var container = builder.Build();
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            //GlobalHost.DependencyResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);

            app.UseAutofacMiddleware(container);

            return container;
        }
    }
}
