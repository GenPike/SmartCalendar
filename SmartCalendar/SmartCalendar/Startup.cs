using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartCalendar.Startup))]
namespace SmartCalendar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
