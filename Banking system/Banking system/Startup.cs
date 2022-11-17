using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Banking_system.Startup))]
namespace Banking_system
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
