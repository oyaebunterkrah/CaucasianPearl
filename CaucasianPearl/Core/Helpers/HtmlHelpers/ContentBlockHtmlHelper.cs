using System;
using System.Web;
using System.Web.Mvc;
using CaucasianPearl.Core.DAL.Repository;
using CaucasianPearl.Core.Services.LoggingService;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.Helpers.HtmlHelpers
{
    /// <summary>
    /// Контент-блок.
    /// Рендерит блок контента по ключу из таблицы ContentBlock.
    /// Использование во View: @Html.Raw(Html.ContentBlock("BlockId"))
    /// </summary>
    public static class ContentBlockHtmlHelper
    {
        private static ILogService _logService
        {
            get { return ServiceHelper<ILogService>.GetService(); }
        }

        private static IRepository<ContentBlock> _repository
        {
            get { return ServiceHelper<IRepository<ContentBlock>>.GetService(); }
        }

        public static string ContentBlock(this HtmlHelper html, string blockId)
        {
            if (_logService == null)
                throw new NullReferenceException("_logService");

            if (_repository == null)
            {
                _logService.Error("_repository");
                throw new NullReferenceException("_repository");
            }

            var contentBlock = _repository.Single(cb => cb.BlockId == blockId);

            if (contentBlock == null)
                return string.Empty;

            if (!contentBlock.IsPublished)
                return string.Empty;

            return HttpUtility.HtmlDecode(contentBlock.Content);
        }
    }
}