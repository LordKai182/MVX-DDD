using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MBR.Startup))]
namespace MBR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
