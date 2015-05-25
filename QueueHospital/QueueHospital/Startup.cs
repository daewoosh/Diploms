using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QueueHospital.Startup))]
namespace QueueHospital
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
