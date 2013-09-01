using System.Collections.Generic;
using System.Web;
using CaucasianPearl.Core.Constants;

namespace CaucasianPearl.Core.Helpers
{
    public class ControllerHelper
    {
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
    }
}