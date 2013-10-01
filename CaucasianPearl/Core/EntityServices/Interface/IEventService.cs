using System;
using System.Collections.Generic;
using CaucasianPearl.Core.DAL.Data;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.EntityServices.Interface
{
    public interface IEventService<T> : IUrlFriendlyService<T> where T : class, IUrlFriendly, new()
    {
        /// <summary>
        /// Возвращает указанное кол-во событий.
        /// </summary>
        /// <param name="count">Количество событий.</param>
        /// <returns>Список EventItem</returns>
        IEnumerable<EventItem> GetLastEventItems(int count);

        /// <summary>
        /// Возвращает указанное кол-во событий.
        /// </summary>
        /// <param name="count">Количество событий.</param>
        /// <returns>Список EventItemInfo</returns>
        IEnumerable<EventItemInfo> GetLastEventsInfo(int count);

        /// <summary>
        /// Возвращает соседние события.
        /// </summary>
        /// <param name="eventId">ID текущего события.</param>
        /// <returns>Список EventItem</returns>
        IEnumerable<EventItem> GetNeighborEvents(int eventId);

        /// <summary>
        /// Возвращает события на месяц.
        /// </summary>
        /// <returns>Список EventItemInfo</returns>
        IEnumerable<EventItemInfo> GetEventsForMonth();

        /// <summary>
        /// Возвращает события на указанную дату.
        /// </summary>
        /// <returns>Список EventItem</returns>
        IEnumerable<EventItem> GetEventsToDate(DateTime date);

        /// <summary>
        /// Возвращает путь к главному изображению события.
        /// </summary>
        /// <returns>Список EventItem</returns>
        string GetPrimaryPhotoUrl(EventItem eventItem);
    }
}