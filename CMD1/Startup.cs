using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CMD1.Startup))]
namespace CMD1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
