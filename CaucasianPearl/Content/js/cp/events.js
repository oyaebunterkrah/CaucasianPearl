(function ($) {
    $(document).ready(function () {
        jQuery.noConflict();
    });

    $(window).load(function () {
        setSlider();
    });

    $(window).resize(function () {
        if ($(window).width() <= 940)
            $('.container *').width($(window).width());
    });

    setSlider = function () {
        $('#featured').orbit({
            animation: 'fade',  // fade, horizontal-slide, vertical-slide, horizontal-push
            animationSpeed: 1000,// how fast animtions are
            timer: true, // true or false to have the timer
            advanceSpeed: 4000, // if timer is enabled, time between transitions 
            pauseOnHover: true, // if you hover pauses the slider
            startClockOnMouseOut: false, // if clock should start on MouseOut
            startClockOnMouseOutAfter: 1000, // how long after MouseOut should the timer start again
            directionalNav: true, // manual advancing directional navs
            captions: true, // do you want captions?
            captionAnimation: 'fade', // fade, slideOpen, none
            captionAnimationSpeed: 800, // if so how quickly should they animate in
            bullets: false, // true or false to activate the bullet navigation
            bulletThumbs: false, // thumbnails for the bullets
            bulletThumbLocation: '', // location from this file where thumbs will be
            afterSlideChange: function () { } // empty function 
        });
    };

    setEventMedia = function (self) {
        $('html, body').animate({ scrollTop: 0 }, 'fast');
        var img = self.find('img');
        var eventItem = img.parents('.event_item');

        var eventMediaItems = eventItem.find('.event_media_items').children();
        var eventMediaItemsInfoBlocks = eventItem.find('.event_media_items_data');
        var featured =
            $('<div/>')
                .attr({ id: 'featured' })
                .html(eventMediaItems);
        $('.container')
            .empty()
            .append(featured)
            .append(eventMediaItemsInfoBlocks);

        $('#featured img, #featured div.video').first().hide().fadeIn().show();
        setSlider();

        getNeighborEvents(self.data(consts.ids.eventid));
    };

    getNeighborEvents = function (eventId) {
        var options = {
            url: '/event/getneighborevents',
            type: 'POST',
            data: { eventId: eventId },
            success: function (events) { onGetNeighborEventsSuccess(events); }
        };
        ajaxRequest(options);
    };

    onGetNeighborEventsSuccess = function (events) {
        if (!events)
            return;

        var eventItems = [];

        // события
        $.each(events, function (i, eventItem) {
            var eventItemControl = $('.event_item_template').clone();

            var eventPrimaryPhotoSrc = '';
            $.each(eventItem.EventMedia, function () {
                var self = $(this)[0];
                if (self.IsPrimary == true)
                    eventPrimaryPhotoSrc = self.ThumbnailUrl;
            });

            eventItemControl.find('.image img').attr('src', eventPrimaryPhotoSrc);
            eventItemControl.find('a').data(consts.ids.eventid, eventItem.ID);

            eventItemControl.find('.title').text(eventItem.Title);
            eventItemControl.find('.date').text(new Date(eventItem.EventDate).toDate());
            eventItemControl.find('.description').text(eventItem.Description);
            eventItemControl.find('.content').text(eventItem.Content);

            // div с медиа файлами события
            var eventMediaItemsControl = eventItemControl.find('.event_media_items');

            // медиа файлы события
            $.each(eventItem.EventMedia, function (k, eventMediaItem) {
                var eventMediaItemControl =
                    $('.event_media_item_template')
                        .clone()
                        .find('{0}'.f(eventMediaItem.MediaType == 'photos'
                            ? 'img'
                            : 'embed'));

                eventMediaItemControl.data({
                    eventid: eventMediaItem.EventId,
                    flickrurl: eventMediaItem.FlickrUrl,
                    description: eventMediaItem.Description,
                    content: eventMediaItem.Content,
                    caption: '#mediaItemInfo_{0}'.f(eventMediaItem.ID)
                });

                eventMediaItemControl
                    .attr({
                        src: eventMediaItem.MediaType == 'photos'
                            ? eventMediaItem.LargeUrl
                            : eventMediaItem.VideoUrl + eventMediaItemControl.attr('src')
                    })
                    .dblclick(function () {
                        openInNewTab(eventMediaItem.FlickrUrl);
                    });

                // если видео - оборачиваем в div
                if (eventMediaItem.MediaType != 'photos')
                    eventMediaItemControl = $('<div class="video"></div>').append($(eventMediaItemControl));

                eventMediaItemsControl.append(eventMediaItemControl);
            });

            // div с описанием/контентом для медиа файлов
            var eventMediaItemsInfosControl = eventItemControl.find('.event_media_items_data');

            $.each(eventItem.EventMedia, function (k, eventMediaItem) {
                if (eventMediaItem.Description != '' || eventMediaItem.Content != '') {
                    var eventmediaItemInfoControl =
                    $('.event_media_item_info_template')
                        .find('.orbit-caption')
                        .clone();

                    eventmediaItemInfoControl.attr({ id: 'thumbMediaItemInfo_{0}'.f(eventMediaItem.ID) });

                    eventmediaItemInfoControl.find('.description').html(eventMediaItem.Description);
                    eventmediaItemInfoControl.find('.content').html(eventMediaItem.Content);

                    eventMediaItemsInfosControl.append(eventmediaItemInfoControl);
                }
            });

            eventItems.push(eventItemControl);
        });

        $('#events .event_item').fadeOut(200, function () { $(this).remove(); });

        setTimeout(function () {
            $.each(eventItems, function (index) {
                $(this)
                    .removeClass('event_item_template')
                    .addClass('event_item')
                    .fadeIn()
                    .appendTo('#events');
            });
        }, 200);
    };
}(jQuery));