using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReferVille.Startup))]
namespace ReferVille
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
