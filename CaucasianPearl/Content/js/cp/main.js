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
    $(document).ready(function () {
        jQuery.noConflict();

        $.fn.extend({
            // центрирует элемент
            center: function () {
                this.css('position', 'absolute');
                this.css('top', Math.max(0, (($(window).height() - $(this).outerHeight()) / 2) +
                    $(window).scrollTop()) + 'px');
                this.css('left', Math.max(0, (($(window).width() - $(this).outerWidth()) / 2) +
                    $(window).scrollLeft()) + 'px');

                return this;
            }, // }
            // открывает popup для указанного элемента {
            popup: function (options) {
                if (options == undefined)
                    options = {};

                $(this).dialog({
                    autoOpen: options.autoOpen != undefined ? options.autoOpen : false,
                    autoResize: options.autoResize != undefined ? options.autoResize : true,
                    modal: options.modal != undefined ? options.modal : true,
                    show: { effect: 'clip', duration: 350 },
                    hide: { effect: 'clip', duration: 350 },
                    height: options.height != undefined ? options.height : 'auto',
                    maxHeight: options.maxHeight != undefined ? options.maxHeight : 'auto',
                    width: options.width != undefined ? options.width : 'auto',
                    maxWidth: options.maxWidth != undefined ? options.maxWidth : 'auto',
                    title: options.title != undefined ? options.title : '',
                    resizable: options.resizable != undefined ? options.resizable : false,
                    closeOnEscape: options.closeOnEscape != undefined ? options.closeOnEscape : true,
                    close: options.close != undefined ? options.close : function () { closeDialogs(); },
                    buttons: options.buttons != undefined ? options.buttons :
                        [
                            {
                                text: 'Закрыть',
                                id: 'btnClose',
                                click: function () {
                                    closeDialogs();
                                }
                            }
                        ],
                });
            },
            // сравнивает значение из data по ключу key с указанным value {
            isData: function (key, value) {
                return $(this).data(key) == value;
            }, // }
            // сбрасывает сообщения об ошибках валидации для инпутов в элементе (форме) {
            resetValidation: function () {
                var self = $(this);
                self.find('.field-validation-error')
                    .removeClass('field-validation-error')
                    .addClass('field-validation-valid');

                self.find('.input-validation-error')
                    .removeClass('input-validation-error')
                    .addClass('valid');
                
                // обновляем валидацию для формы для попап окон {
                var form = $('form');
                form.removeData('validator')
                    .removeData('nobtrusiveValidation');
                $.validator.unobtrusive.parse(form); // }
                
                return self;
            }, // }
            // очищает инпуты внутри элемента (формы) {
            clearData: function () {
                var self = $(this);
                self.find('textarea').val('');
                self.find('input[type="email"]').val('');
                self.find('input[type="phone"]').val('');
                self.find('input[type="text"]').val('');

                return self;
            }, // }
            // валидация формы {
            isValid: function () {
                var self = $(this);
                $.validator.unobtrusive.parse(self);
                
                return self.data('unobtrusiveValidation').validate();
            } // }
        });

        $('.ui-widget-overlay').live('click', function () {
            closeDialogs();
        });
    });

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
        if (offset > innerHeight) {
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

    closeDialogs = function () {
        $('.ui-dialog-content').dialog('close');
    };

    showLoading = function (min) {
        if (min) {
            $('.overlay_min').show();
            $('.loading_min').show();
        } else {
            $('.overlay').show();
            $('.loading').show();
        }
    };

    hideLoading = function (min) {
        if (min) {
            $('.overlay_min').hide();
            $('.loading_min').hide();
        } else {
            $('.overlay').hide();
            $('.loading').hide();
        }
    };

    // Обёртка для ajax вызовов.
    ajaxRequest = function (options) {
        $.ajax({
            // parameters {
            url: options.url,
            type: options.type != undefined ? options.type : 'GET',
            data: options.data != undefined ? options.data : '',
            contentType: options.contentType != undefined ? options.contentType : 'application/x-www-form-urlencoded; charset=UTF-8',
            dataType: options.dataType != undefined ? options.dataType : 'json', // }
            
            // methods {
            success: function (data) {
                if (options.success != undefined) {
                    options.success(data);
                };
            },
            beforeSend: function () {
                if (options.beforeSend != undefined) {
                    options.beforeSend();
                };
            },
            complete: function () {
                if (options.complete != undefined) {
                    options.complete();
                };
            },
            error: function (request, status, error) {
                if (options.error != undefined) {
                    options.error();
                };
            } // }
        });
    };
}(jQuery));