using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using System.Web.Optimization;
using System.Web.Http;

namespace WebSiteMusicBand
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        //public static string linkToApp = "http://localhost:31914/";

        protected void Application_Start()
        {
            logger.Trace("Application Start");

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
