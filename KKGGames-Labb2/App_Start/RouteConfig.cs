using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KKGGames_Labb2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Win",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Game21", action = "Win", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "Lose",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Game21", action = "Lose", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Game21", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
