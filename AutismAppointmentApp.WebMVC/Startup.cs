using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutismAppointmentApp.WebMVC.Startup))]
namespace AutismAppointmentApp.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
