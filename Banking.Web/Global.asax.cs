using System.Configuration;
using System.Web;
using System.Web.Http;
using Banking.Web.Infra.Jwt;

namespace Banking.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            UnityConfig.RegisterComponents();
            JwtAuthConfig.Configure(ConfigurationManager.AppSettings);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
