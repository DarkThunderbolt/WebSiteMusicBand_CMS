using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using System.Web.Optimization;

namespace WebSiteMusicBand
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static string linkToApp = "http://localhost:31914/";

        protected void Application_Start()
        {
            logger.Trace("Application Start");

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
