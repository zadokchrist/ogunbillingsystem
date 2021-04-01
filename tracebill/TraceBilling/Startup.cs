using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TraceBilling.Startup))]
namespace TraceBilling
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
