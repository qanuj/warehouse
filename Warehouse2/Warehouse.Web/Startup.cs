using Warehouse.Web;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Warehouse.Web
{
    public class Product
    {
        public static string Name => "Drama";
        public static string Slogan => "It works.";
    }
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            IocHelper.CreateContainer(app);
        }
    }
}
