using System.Data.Entity;
using Autofac;
using Warehouse.Models;

namespace Warehouse
{
    public class WebLayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().As<DbContext>();
        }
    }
}