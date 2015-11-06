using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Warehouse.Core;
using Warehouse.Data.Respository;

namespace Warehouse.Data
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfRepository<Weather>>().As<IRepository<Weather>>();
            builder.RegisterType<EfRepository<Taxonomy>>().As<IRepository<Taxonomy>>();
        }
    }
}
