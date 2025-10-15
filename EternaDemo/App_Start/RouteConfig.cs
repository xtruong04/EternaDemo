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
                 name: "ProductByCateID",
                 url: "product-by-category-{cateID}",
                 defaults: new { controller = "Shop", action = "Index", id = UrlParameter.Optional }
             );

            routes.MapRoute(
              name: "About",
              url: "about",
              defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Contact",
               url: "contact",
               defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Blog",
              url: "blog",
              defaults: new { controller = "Home", action = "Blog", id = UrlParameter.Optional }
           );

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

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "EternaDemo.Controllers" }
            );
        }
    }
}