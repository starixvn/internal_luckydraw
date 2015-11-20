// ----------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.20">Created.</change>
// </history>

namespace LuckyDraw.Site
{
    using System.Web.Optimization;

    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                        "~/Libs/jquery-{version}.js",
                        "~/Libs/bootstrap.min.js",
                        "~/Libs/angular.min.js",
                        "~/Libs/angular-resource.min.js",
                        "~/Libs/angular-route.min.js",
                        "~/Libs/angular-translate.js"));

            bundles.AddAndInclude(
                        "~/Content/css",
                        "~/Content/site.css");

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/SourceApp/app.js",
                        "~/SourceApp/routes.js",
                        "~/SourceApp/Components/Dashboard/dashboard-controller.js"));
        }
    }
}