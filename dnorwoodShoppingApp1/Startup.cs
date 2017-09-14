using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(dnorwoodShoppingApp1.Startup))]
namespace dnorwoodShoppingApp1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
