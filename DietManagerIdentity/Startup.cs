using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DietManagerIdentity.Startup))]
namespace DietManagerIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
