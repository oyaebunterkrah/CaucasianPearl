using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using CaucasianPearl.Core.Constants;

namespace CaucasianPearl.Core.Helpers
{
    public class ControllerHelper
    {
        public class CurrentLocation
        {
            public string Controller { get; set; }
            public string Action { get; set; }
        }

        // Возвращает строку с первым символом в нижнем регистре.
        public static int GetCurrentPageNumber()
        {
            var page = 1;
            if (HttpContext.Current.Request.QueryString[Consts.QueryStringParameters.Page] != null)
                page = int.Parse(HttpContext.Current.Request.QueryString[Consts.QueryStringParameters.Page]);
            if (page < 1)
                page = 1;

            return page;
        }

        // Возвращает текущее местоположение.
        public static CurrentLocation GetCurrentLocation()
        {
            return new CurrentLocation
                {
                    Controller = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString(),
                    Action = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString()
                };
        }
    }
}