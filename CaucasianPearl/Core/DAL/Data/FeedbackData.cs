using System;
using System.Collections.Generic;
using System.Linq;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Core.Helpers.EntityHelpers;
using CaucasianPearl.Models.EDM;
using Resources;

namespace CaucasianPearl.Core.DAL.Data
{
    public class FeedbackItem
    {
        public FeedbackItem()
        {

        }

        public FeedbackItem(Feedback feedback, bool isLast)
        {
            ID = feedback.ID;
            Name = !string.IsNullOrWhiteSpace(feedback.Name) ? feedback.Name : ViewRes.NotSpecified;
            City = !string.IsNullOrWhiteSpace(feedback.City) ? feedback.City : ViewRes.NotSpecified;
            Comment = !string.IsNullOrWhiteSpace(feedback.Comment) ? feedback.Comment : ViewRes.NotSpecified;
            Created = feedback.Created.HasValue
                          ? feedback.Created.Value.ToShortDateString()
                          : DateTime.Now.ToShortDateString();
            IsLast = isLast;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Comment { get; set; }
        public string Created { get; set; }
        public bool IsLast { get; set; }
    }
}