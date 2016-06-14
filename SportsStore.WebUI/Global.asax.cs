using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using SportsStore.WebUI.Infrastructure;
using System.Web.Routing;

namespace SportsStore.WebUI
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            //            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }

        private void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("Default",  // Route name
          "{controller}/{action}/{id}",   // URL with parameters
          new
          {
              controller = "Product",
              action = "List",
              id = UrlParameter.Optional
          }    // Parameter defaults
        );

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}