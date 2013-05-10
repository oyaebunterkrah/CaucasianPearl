using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Models;

namespace CaucasianPearl.Core.UserControls
{
    public class Paginator
    {
        /// <summary>
        /// Формирование списка ссылок на страницы.
        /// </summary>
        /// <param name="totalCount">Количество объектов всего (на всех страницах).</param>
        /// <param name="linksPerPage">Количество объектов на одной странице.</param>
        /// <param name="numberOfVisibleLinks">Количество отображаемых страниц перед многоточием.</param>
        /// <returns></returns>
        public static List<string> GetLinks(int totalCount, int linksPerPage, int numberOfVisibleLinks)
        {
            var result = new List<string>();

            if (totalCount == linksPerPage)
                return result;

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest == null)
                throw new Exception("httpRequest");

            int currentPage;
            int.TryParse(httpRequest.QueryString["page"], out currentPage);
            if (currentPage < 1)
                currentPage = 1;

            var queryStringParams = httpRequest.QueryString.Keys
                                            .Cast<string>().Where(key => key != "page")
                                            .Aggregate("?",
                                                       (current, key) =>
                                                       string.Format("{0}{1}={2}&", currentPage, key, httpRequest.QueryString[key]));

            var countPages = (int) Math.Ceiling(totalCount/(double) linksPerPage);

            var bThreeDots1 = false;
            var bThreeDots2 = false;

            var visibleLinksHead = numberOfVisibleLinks;
            if (numberOfVisibleLinks >= (currentPage - numberOfVisibleLinks))
                visibleLinksHead = numberOfVisibleLinks * 3 + 1;

            var visibleLinksTail = numberOfVisibleLinks;
            if ((currentPage + numberOfVisibleLinks) >= (countPages - numberOfVisibleLinks))
                visibleLinksTail = numberOfVisibleLinks * 3 + 1;

            for (var i = 1; i <= countPages; i++)
            {
                if (i <= visibleLinksHead
                    || i > (countPages - visibleLinksTail)
                    || (i <= currentPage && i >= (currentPage - numberOfVisibleLinks))
                    || (i >= currentPage && i <= (currentPage + numberOfVisibleLinks)))
                {
                    result.Add(i == currentPage
                                   ? i.ToString(CultureInfo.InvariantCulture)
                                   : string.Format("<a href='{0}page={1}'>{1}</a>", queryStringParams, i));
                }
                else
                {
                    if (i < currentPage)
                    {
                        if (!bThreeDots1)
                        {
                            result.Add("...");
                            bThreeDots1 = true;
                        }
                    }
                    else
                    {
                        if (!bThreeDots2)
                        {
                            result.Add("...");
                            bThreeDots2 = true;
                        }
                    }
                }
            }

            return result;
        }
    }
}
