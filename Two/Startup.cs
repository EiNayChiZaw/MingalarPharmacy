using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Two.Startup))]
namespace Two
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
