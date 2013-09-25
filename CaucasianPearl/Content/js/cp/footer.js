(function ($) {
    $(document).ready(function () {
        $('.developer_contacts a').click(function (e) {
            e.preventDefault();
            var self = $(this);
            var popup = $('.{0}'.f(self.data('who')));
            if (popup.css('display') != 'none') {
                popup.hide();
                return;
            } else
                $('.popup').hide();
            popup.css({ left: e.pageX - popup.width() - 5, top: e.pageY - popup.height() - 5 }).show();
            popup.show();
        });

        $('.popup').click(function (e) {
            $(this).hide();
        });

        $('.popup a').click(function (e) {
            $('.popup').hide();
        });
    });
}(jQuery));