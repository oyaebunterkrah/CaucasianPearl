$(document).ready(function () {
    jQuery.noConflict();

    // сравнивает значение из data по ключу key с указанным value
    $.fn.extend({
        isData: function (key, value) {
            return $(this).data(key) == value;
        }
    });

    // открывает модальное окно у указанного элемента
    $.fn.extend({
        popup: function (options) {
            $(this).dialog(options);
        }
    });

    $.fn.center = function () {
        this.css('position', 'absolute');
        this.css('top', Math.max(0, (($(window).height() - $(this).outerHeight()) / 2) +
            $(window).scrollTop()) + 'px');
        this.css('left', Math.max(0, (($(window).width() - $(this).outerWidth()) / 2) +
            $(window).scrollLeft()) + 'px');

        return this;
    };

    $('.ui-widget-overlay').live('click', function () {
        closeDialogs();
    });
});

var consts = {
    'fields': {
        'eventid': 'eventid',
        'photoid': 'photoid',
        'photosetid': 'photosetid',
        'description': 'description',
        'content': 'content',
        'mediatype': 'mediatype',
        'flickrurl': 'flickrurl',
        'thumbnailurl': 'thumbnailurl',
        'smallurl': 'smallurl',
        'mediumurl': 'mediumurl',
        'largeurl': 'largeurl',
        'videourl': 'videourl',
        'isprimary': 'isprimary',

        'EventId': 'EventId',
        'PhotoId': 'PhotoId',
        'PhotosetId': 'PhotosetId',
        'Description': 'Description',
        'Content': 'Content',
        'MediaType': 'MediaType',
        'FlickrUrl': 'FlickrUrl',
        'ThumbnailUrl': 'ThumbnailUrl',
        'SmallUrl': 'SmallUrl',
        'MediumUrl': 'MediumUrl',
        'LargeUrl': 'LargeUrl',
        'VideoUrl': 'VideoUrl',
        'IsPrimary': 'IsPrimary'
    },

    'ids': {
        'eventid': 'eventid',
        'mediaitemid': 'mediaitemid',
        'mediaItemId': 'mediaItemId'
    }
};

(function ($) {
    String.prototype.format = String.prototype.f = function () {
        var str = this;
        for (var i = 0; i < arguments.length; i++) {
            var reg = new RegExp('\\{' + i + '\\}', 'gm');
            str = str.replace(reg, arguments[i]);
        }
        return str;
    };

    Date.prototype.toDate = function (key) {
        function f(n) {
            return n < 10 ? '0' + n : n;
        }

        return f(this.getUTCDate()) + '.' +
			 f(this.getUTCMonth() + 1) + '.' +
			 this.getUTCFullYear();
    };

    scrollToElement = function (element, duration) {
        var offset = element.offset().top + element.height() - $(window).scrollTop();

        // not in view so scroll to it
        if (offset > window.innerHeight) {
            $('html, body').animate({ scrollTop: offset }, duration ? duration : 1000);
        }
    };

    replaceByCKEditor = function (configPath, elementId, submitId) {
        CKEDITOR.config.customConfig = configPath;
        CKEDITOR.replace(elementId);
        CKEDITOR.instances[elementId].setData($('#' + elementId).val());

        $('#{0}'.f(submitId)).click(function () {
            $('#{0}'.f(elementId)).val(CKEDITOR.instances[elementId].getData());
        });
    };

    toArray = function (json) {
        var photoArray = [];

        $.each(json, function (key, value) {
            photoArray.push(json[key]);
        });

        return photoArray;
    };

    openInNewTab = function (url) {
        window.open(url, '_blank');
    };

    encodeScriptTags = function (value) {
        return value.replace(/\</g, '[').replace(/\>/g, ']');
    };

    decodeScriptTags = function (value) {
        return value.replace(/\[/g, '<').replace(/\]/g, '>');
    };

    // Обёртка для ajax вызовов.
    $.ajaxRequest = function (options) {
        $.ajax({
            // parameters {
            url: options.url,
            type: options.type,
            data: options.data, // }

            // methods {
            success: function (data) {
                if (typeof options.success !== 'undefined') {
                    options.success(data);
                };
            },
            beforeSend: function () {
                if (typeof options.beforeSend !== 'undefined') {
                    options.beforeSend();
                };
            },
            complete: function () {
                if (typeof options.complete !== 'undefined') {
                    options.complete();
                };
            },
            error: function (request, status, error) {
                if (typeof options.error !== 'undefined') {
                    options.error();
                };
            } // }
        });
    };
    
    closeDialogs = function () {
        $('.ui-dialog-content').remove();
    };
}(jQuery));