using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Optimization;

namespace WebSiteMusicBand
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/JS").Include(
                "~/Content/JS/jquery.min.js",
                "~/Content/JS/bootstrap.min.js",
                "~/Content/JS/clean-blog.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                  "~/Content/CSS/bootstrap.min.css",
                  "~/Content/CSS/clean-blog.min.css"
               
                ));
        }
    }
}
