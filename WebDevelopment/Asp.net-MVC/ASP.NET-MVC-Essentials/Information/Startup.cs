using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Information.Startup))]
namespace Information
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app) 
        {
            ConfigureAuth(app);
        }
    }
}
