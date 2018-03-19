using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CustProject.Startup))]
namespace CustProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
