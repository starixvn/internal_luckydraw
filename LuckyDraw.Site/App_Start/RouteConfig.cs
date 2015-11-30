// ----------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.20">Created.</change>
// </history>

namespace LuckyDraw.Site
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Collections.Generic;

    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var homeRouteUrls = new List<string>
            {
                "Home/AccessDenied",
                "Client/Index",
                "Client/Index/{grade}",
                "Server/Index",
                "Server/SuperGrand/Index",
                "Server/Grand/Index",
                "Server/Major/Index",
                "Server/Cash/Index",
                "Server/PrizeList/{prizeType}",
                "Server/WinnerDetails/Index",
                "Server/LuckyList/Index"
            };

            homeRouteUrls.ForEach(url => routes.MapRoute(
                name: url,
                url: url,
                defaults: new
                {
                    controller = "Home",
                    action = "Index"
                }));

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}