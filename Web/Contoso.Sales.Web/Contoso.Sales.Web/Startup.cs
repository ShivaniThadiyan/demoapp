using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Contoso.Sales.Web.Startup))]
namespace Contoso.Sales.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var config = new System.Web.Http.HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
    }
}
