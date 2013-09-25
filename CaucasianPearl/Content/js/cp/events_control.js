(function ($) {
    $(document).ready(function () {
        $('.event').each(function (i) {
            $(this)
                .css({ visibility: 'visible' })
                .hide()
                .fadeIn(i * 500);
        });
    });
}(jQuery));