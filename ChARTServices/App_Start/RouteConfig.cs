using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ChARTServices
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "StationsByRoute",
                url: "routes/{route}/stations",
                defaults: new {controller = "Stations", action = "ByRoute" }
            );

            routes.MapRoute(
                name: "StationIcon",
                url: "stations/{id}/icon",
                defaults: new { controller = "Stations", action = "Icon" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}