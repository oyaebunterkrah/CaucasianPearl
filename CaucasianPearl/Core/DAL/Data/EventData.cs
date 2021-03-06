﻿using System;
using System.Collections.Generic;
using System.Linq;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Core.Helpers.EntityHelpers;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.DAL.Data
{
    public class EventItem
    {
        public EventItem()
        {

        }

        public EventItem(Event @event)
        {
            ID = @event.ID;
            Content = StringHelper.DecodeScriptTags(@event.Content);
            Description = StringHelper.DecodeScriptTags(@event.Description);
            EventDate = @event.EventDate.HasValue ? @event.EventDate.Value.Date : new DateTime();
            EventMedia = @event.EventMedia.Select(em => new EventMediaItem(em)).OrderByDescending(emi => emi.IsPrimary);
            Title = @event.Title;
        }

        public int ID { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public DateTime? EventDate { get; set; }
        public IEnumerable<EventMediaItem> EventMedia { get; set; }
        public string Title { get; set; }
    }

    /// <summary>
    /// Упрощённый вариант EventItem для главной страницы.
    /// </summary>
    public class EventItemInfo
    {
        public EventItemInfo()
        {

        }

        public EventItemInfo(Event @event)
        {
            ID = @event.ID;
            Day = @event.EventDate.HasValue
                      ? @event.EventDate.Value.Day
                      : new DateTime().Day;
            Description = @event.Description;
            CoverImage = EventHelper.GetCoverImage(@event);
            Month = StringHelper.DeclineMonth(@event.EventDate.HasValue
                                                  ? @event.EventDate.Value
                                                  : new DateTime());
            Title = @event.Title;
        }

        public int ID { get; set; }
        public int Day { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
        public string Month { get; set; }
        public string Title { get; set; }
    }

    public class EventMediaItem
    {
        public EventMediaItem()
        {

        }

        public EventMediaItem(EventMedia eventMedia)
        {
            ID = eventMedia.ID;
            EventId = eventMedia.EventId;
            Title = StringHelper.DecodeScriptTags(eventMedia.Title);
            Description = StringHelper.DecodeScriptTags(eventMedia.Description);
            FlickrUrl = eventMedia.FlickrUrl;
            IsPrimary = eventMedia.IsPrimary ?? false;
            LargeUrl = eventMedia.LargeUrl;
            MediaType = eventMedia.MediaType;
            ThumbnailUrl = eventMedia.ThumbnailUrl;
            VideoUrl = eventMedia.VideoUrl;
        }

        public int ID { get; set; }
        public int? EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FlickrUrl { get; set; }
        public bool? IsPrimary { get; set; }
        public string LargeUrl { get; set; }
        public string MediaType { get; set; }
        public string ThumbnailUrl { get; set; }
        public string VideoUrl { get; set; }
    }
}