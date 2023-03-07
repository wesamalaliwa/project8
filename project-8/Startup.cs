using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(project_8.Startup))]
namespace project_8
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
