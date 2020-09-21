using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GigHubBC10.Startup))]
namespace GigHubBC10
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
