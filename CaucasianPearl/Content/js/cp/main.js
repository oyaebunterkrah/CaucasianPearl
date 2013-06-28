$(document).ready(function () {
    // сравнивает значение из аттрибута "data" (key) с указанным value
    $.fn.extend({
        isData: function (key, value) {
            return $(this).data(key) == value;
        }
    });

    $.fn.center = function() {
        this.css('position', 'absolute');
        this.css('top', Math.max(0, (($(window).height() - $(this).outerHeight()) / 2) +
            $(window).scrollTop()) + 'px');
        this.css('left', Math.max(0, (($(window).width() - $(this).outerWidth()) / 2) +
            $(window).scrollLeft()) + 'px');
        
        return this;
    };
});

(function ($) {
    String.prototype.format = String.prototype.f = function() {
        var str = this;
        for (var i = 0; i < arguments.length; i++) {
            var reg = new RegExp('\\{' + i + '\\}', 'gm');
            str = str.replace(reg, arguments[i]);
        }
        return str;
    };

    
}(jQuery));

scrollToElement = function(element, duration) {
    var offset = element.offset().top + element.height() - $(window).scrollTop();

    // not in view so scroll to it
    if (offset > window.innerHeight) {
        $('html, body').animate({ scrollTop: offset }, duration ? duration : 1000);
    }
};

replaceByCKEditor = function(configPath, elementId, submitId) {
    CKEDITOR.config.customConfig = configPath;
    CKEDITOR.replace(elementId);
    CKEDITOR.instances[elementId].setData($('#' + elementId).val());

    $('#' + submitId).click(function() {
        $('#' + elementId).val(CKEDITOR.instances[elementId].getData());
    });
};