using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DMYTest.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional }
            );


            routes.MapRoute(
               name: "Product.List",
               url: "products/list/{id}",
               defaults: new { controller = "Products", action = "List", id = UrlParameter.Optional }
               );

            routes.MapRoute(
               name: "Product.Add",
               url: "products/add/{id}",
               defaults: new { controller = "Products", action = "Add", id = UrlParameter.Optional }
               );
            routes.MapRoute(
               name: "Product.Edit",
               url: "products/edit/{id}",
               defaults: new { controller = "Products", action = "Edit", id = UrlParameter.Optional }
               );

        }
    }
}
