﻿using System.Web;
using System.Web.Optimization;

namespace CMD1
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/app.css",
                "~/Content/font-awesome.css",
                "~/Content/themes/base/jquery-ui.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/jquery-3.3.1.min.js",
                "~/Scripts/inputmask/inputmask.js",
                "~/Scripts/inputmask/inputmask.extensions.js",
                "~/Scripts/inputmask/inputmask.numeric.extensions.js",
                "~/Scripts/inputmask/inputmask.date.extensions.js",
                "~/Scripts/inputmask/inputmask.phone.extensions.js",
                "~/Scripts/inputmask/jquery.inputmask.js",
                "~/Scripts/inputmask/phone-codes/phone.js",
                "~/Scripts/inputmask/phone-codes/phone-be.js",
                "~/Scripts/inputmask/phone-codes/phone-ru.js",
                "~/Scripts/jquery-ui-1.12.1.min.js",
                "~/Scripts/angular.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/sweet-alert.min.js",
                "~/Scripts/Popper.min.js",
                "~/Scripts/Helpers.js"));
        }
    }
}
