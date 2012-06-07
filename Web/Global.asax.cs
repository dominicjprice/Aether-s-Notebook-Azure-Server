using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Aethers.Notebook.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Web",
                "Web/{action}",
                new { controller = "Web", action = "Index" }
            );

            routes.MapRoute(
                "AJAX",
                "AJAX/{action}",
                new { controller = "AJAX" }
            );

            routes.MapRoute(
                "API",
                "API/{action}",
                new { controller = "API" }
            );

            routes.MapRoute(
                "Dataware",
                "Dataware/{action}",
                new { controller = "Dataware" }
            );

            routes.MapRoute(
                "Default",
                "{controller}/{action}",
                new
                {
                    controller = "Entry",
                    action = "Index"
                }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            Aethers.Notebook.Configuration.onStart();
            Aethers.Notebook.Configuration.configureStorage();
            Aethers.Notebook.Configuration.configureDevelopmentDiagnostics();
        }
    }
}