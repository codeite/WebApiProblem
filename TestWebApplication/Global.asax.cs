using System;
using System.Web;
using System.Web.Http;

namespace WebApiProblem.TestWebApplication
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}