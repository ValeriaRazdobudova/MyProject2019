using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NTCService.Startup))]
namespace NTCService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
