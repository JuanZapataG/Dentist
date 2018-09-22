using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pratice1_2018_II.Backend.Startup))]
namespace Pratice1_2018_II.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
