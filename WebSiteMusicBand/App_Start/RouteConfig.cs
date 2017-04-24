﻿using System;
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
            routes.IgnoreRoute("");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "News", action = "ListLong", id = UrlParameter.Optional }            );

            //routes.MapRoute(
            //    name: "Members",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Members", action = "Index", id = UrlParameter.Optional }
            //);
        }

    }
}
