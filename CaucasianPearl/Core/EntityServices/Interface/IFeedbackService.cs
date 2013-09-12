using System;
using System.Collections.Generic;
using CaucasianPearl.Core.DAL.Data;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.EntityServices.Interface
{
    public interface IFeedbackService<T> : IBaseService<T> where T : class, IBase, new()
    {
        /// <summary>
        /// Возвращает последний отзыв.
        /// </summary>
        /// <returns></returns>
        FeedbackItem GetLastFeedback();

        /// <summary>
        /// Возвращает предыдущий отзыв.
        /// </summary>
        /// <returns></returns>
        FeedbackItem GetPreviousFeedback(int id);

        /// <summary>
        /// Возвращает следующий отзыв.
        /// </summary>
        /// <returns></returns>
        FeedbackItem GetNextFeedback(int id);
    }
}