using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebSiteMusicBand.Startup))]
namespace WebSiteMusicBand
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            MvcApplication.logger.Trace("Configuration Owin Startup");
        }
    }
}
