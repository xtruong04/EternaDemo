using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EternaDemo.Startup))]
namespace EternaDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
