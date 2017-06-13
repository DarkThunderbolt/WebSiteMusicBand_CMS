using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebSiteMusicBand
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            ///////////////////////////////////////////////////
            routes.MapRoute(
                name: "Members",
                url: "Members/{id}",
                defaults: new { controller = "Members", action = "Details" }
                , constraints: new { id = @"\d+" }
            );
            routes.MapRoute(
                name: "Members2",
                url: "Members/{action}/{id}",
                defaults: new { controller = "Members", action = "Index", id = UrlParameter.Optional }
                );

            ///////////////////////////////////////////////////
            routes.MapRoute(
                name: "History",
                url: "History/{id}",
                defaults: new { controller = "History", action = "Details" }
                , constraints: new { id = @"\d+" }
            );
            routes.MapRoute(
                name: "History2",
                url: "History/{action}/{id}",
                defaults: new { controller = "History", action = "Index", id = UrlParameter.Optional }
                );

            ///////////////////////////////////////////////////
            routes.MapRoute(
                name: "News",
                url: "News/{id}",
                defaults: new { controller = "News", action = "Details" }
                , constraints: new { id = @"\d+" }
            );
            routes.MapRoute(
                name: "News2",
                url: "News/{action}/{id}",
                defaults: new { controller = "News", action = "ListLong", id = UrlParameter.Optional }
                );

            ///////////////////////////////////////////////////
            routes.MapRoute(
                name: "Album",
                url: "Albums/{id}",
                defaults: new { controller = "Albums", action = "Details" }
                , constraints: new { id = @"\d+" }
            );
            routes.MapRoute(
                name: "Albums",
                url: "Albums/{action}/{id}",
                defaults: new { controller = "Albums", action = "Index", id = UrlParameter.Optional }
                );

            ///////////////////////////////////////////////////
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "News", action = "ListLong", id = UrlParameter.Optional }
            );
        }
    }
}
