using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CaucasianPearl.Core.DAL.Repository;
using CaucasianPearl.Core.Services.LoggingService;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.Helpers.HtmlHelpers
{
    /// <summary>
    /// Блок расположения.
    /// Рендерит все блоки контента, которые с ним связаны, из таблицы ContentBlock.
    /// TODO: изменить описание: Использование во View: @Html.Raw(Html.ContentBlock("BlockId"))
    /// </summary>
    public static class PlaceHolderHtmlHelper
    {
        private static ILogService _logService
        {
            get { return ServiceHelper<ILogService>.GetService(); }
        }

        private static IRepository<ContentBlock> _repository
        {
            get { return ServiceHelper<IRepository<ContentBlock>>.GetService(); }
        }

        public static string PlaceHolder(this HtmlHelper html, string contentBlockId)
        {
            if (_logService == null)
                throw new NullReferenceException("_logService");

            if (_repository == null)
            {
                _logService.Error("_repository");
                throw new NullReferenceException("_repository");
            }

            var contentBlocks =
                _repository.OrderByDescendingFilter(fcb => fcb.PlaceHolderId == contentBlockId, ocb => ocb.Sequence)
                           .ToList();

            if (!contentBlocks.Any())
                return string.Empty;

            var last = contentBlocks.Last();

            var stringBuilder = new StringBuilder();

            foreach (var contentBlock in contentBlocks.Where(contentBlock => contentBlock.IsPublished))
            {
                stringBuilder.Append(contentBlock.Content);

                if (contentBlock != last)
                    stringBuilder.AppendLine();
            }

            return HttpUtility.HtmlDecode(stringBuilder.ToString());
        }
    }
}