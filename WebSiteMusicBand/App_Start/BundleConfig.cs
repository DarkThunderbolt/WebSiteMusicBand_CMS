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
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //    "~/Content/JS/lib/jquery.min.js",
            //    "~/FrontEnd/JS/lib/jqBootstrapValidation.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Content/JS/clean-blog.min.js",
                "~/Content/JS/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/CSS/clean-blog.min.css",
                 "~/Content/CSS/bootstrap.min.css"
                ));
        }
    }
}
