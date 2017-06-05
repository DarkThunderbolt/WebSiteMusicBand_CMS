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
                  "~/Content/CSS/clean-blog.min.css",
                  "~/Content/CSS/MyCss.css"
                ));



            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            "~/Content/JS/UserAuth/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/JS/UserAuth//jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/JS/UserAuth//modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/JS/UserAuth//bootstrap.js",
                      "~/Content/JS/UserAuth//respond.js"));
        }
    }
}
