using System.Globalization;
using System.Linq;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.Helpers.EntityHelpers
{
    public class EventHelper
    {
        /// <summary>
        /// Возвращает главную фотографию для события.
        /// </summary>
        /// <returns></returns>
        public static string GetCoverImage(Event @event)
        {
            if (@event == null || @event.EventMedia.Count == 0)
                return ImageHelper.GetDefaultImageUrl(Consts.Controllers.Event.DefaultImagePath);

            var coverImage = @event.EventMedia.FirstOrDefault(em => em.IsPrimary ?? false);
            return coverImage != null ? coverImage.MediumUrl : @event.EventMedia.First().MediumUrl;
        }
    }
}