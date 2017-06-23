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
                "~/Content/JS/bootstrap.min.js",
                "~/Content/JS/clean-blog.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                  "~/Content/CSS/bootstrap.min.css",
                  "~/Content/CSS/clean-blog.min.css",
                  "~/Content/CSS/MyCss.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/JS/UserAuth//jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/JS/UserAuth//modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/JS/UserAuth//bootstrap.js",
                      "~/Content/JS/UserAuth//respond.js"));



            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            // isotope grid
            bundles.Add(new ScriptBundle("~/bundles/isotope/js").Include(
                "~/Content/JS/isotope.pkgd.min.js"));

            // jsgrid
            bundles.Add(new ScriptBundle("~/bundles/jsgrid/js").Include(
                "~/Scripts/jsgrid/jsgrid.min.js"
                 )); 
            bundles.Add(new StyleBundle("~/bundles/jsgrid/css").Include(
                "~/Scripts/jsgrid/jsgrid.min.css",      
                "~/Scripts/jsgrid/jsgrid-theme.min.css"
                ));

            // modalwindow
            bundles.Add(new StyleBundle("~/bundles/modalwindow/css").Include(
                "~/Content/Css/ModalWindow.css"
                ));
            bundles.Add(new StyleBundle("~/bundles/modalwindow/js").Include(
                "~/Content/JS/ModalWindow.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/picpreview").Include(
                "~/Scripts/PicPreview.js"
                ));
        }
    }
}
