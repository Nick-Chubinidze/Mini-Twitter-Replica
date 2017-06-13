using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Twitter_Nika_Chubinidze.Startup))]
namespace Twitter_Nika_Chubinidze
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
