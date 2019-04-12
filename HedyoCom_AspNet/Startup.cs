using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HedyoCom_AspNet.Startup))]
namespace HedyoCom_AspNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
