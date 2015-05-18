using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CompanyManager.Startup))]
namespace CompanyManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
