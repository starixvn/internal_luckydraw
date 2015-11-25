// ----------------------------------------------------------------------
// <copyright file="ResourcesController.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.25">Created.</change>
// </history>

namespace LuckyDraw.Site.Controllers.API
{
    using System.Collections;
    using System.Globalization;
    using System.Linq;
    using System.Resources;
    using System.Web.Http;
    using LuckyDraw.Resources;

    public class ResourcesController : ApiController
    {
        public string Get(string lang)
        {
            ResourceManager resourceManager = null;
            switch (lang)
            {
                case "en":
                    resourceManager = new ResourceManager(typeof(Lang)) { IgnoreCase = true };
                    break;

                default:
                    resourceManager = new ResourceManager(typeof(Lang)) { IgnoreCase = true };
                    break;
            }

            ResourceSet resourceSet = resourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            var dictionary = resourceSet.Cast<DictionaryEntry>();
            var items = dictionary.Select(dict => string.Format("\"{0}\" : \"{1}\"", dict.Key, string.Join(",", dict.Value)));
            var jsonString = "{" + string.Join(",", items) + "}";

            return jsonString;
        }
    }
}