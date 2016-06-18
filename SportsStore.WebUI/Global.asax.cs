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
            routes.IgnoreRoute("{resource}.axd/{*pathinfo}");

            routes.MapRoute(
                name: null,
                url: "", //matches empty URL
                defaults: new
                {
                    controller = "Product",
                    action = "List",
                    category = (string)null,
                    page = 1
                });

            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new { Controller = "Product", action = "List", category = (string)null },
                constraints: new { page = @"\d+" } //constraint: page nust be numeric
                );

            routes.MapRoute(null,
                "{Category}",
                new { controller = "Product", action = "List", page = "1" }
                );

            routes.MapRoute(null,
                "{category}/Page{page}",
                new { controller = "Product", action = "List" }, //defaults
                new { Page = @"\d+" }
                );

            routes.MapRoute(null, "{controller}/{action}");
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