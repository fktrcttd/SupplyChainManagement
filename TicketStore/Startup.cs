using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TicketStore.Startup))]
namespace TicketStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
