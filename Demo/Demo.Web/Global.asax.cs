using NFlex.Core;
using NFlex.Core.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Demo.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            IocConfig.Initialize();
            DatabaseConfig.Initialize();
        }

        protected void Application_Error()
        {
            var lastError = Server.GetLastError();
        }
    }
}
