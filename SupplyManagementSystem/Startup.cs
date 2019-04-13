using Microsoft.Owin;
using Owin;
using SCM.Models;
using SCM.ViewModels;

[assembly: OwinStartupAttribute(typeof(SCM.Startup))]
namespace SCM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            AutoMapper.Mapper.Initialize(cfg => {

                cfg.CreateMap<Client, ClientViewModel>();

                /* etc */
            });
        }
    }
}
