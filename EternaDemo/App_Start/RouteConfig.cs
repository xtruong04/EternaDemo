using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EternaDemo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "TestLayoutPage", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ListCategory", 
                url: "list-category",
                defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional }
             );

            routes.MapRoute(
               name: "CreateCategory",
               url: "create-category",
               defaults: new { controller = "Category", action = "Create", id = UrlParameter.Optional }
            );
        }
    }
}