using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Airline.Web.Startup))]
namespace Airline.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
