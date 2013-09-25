(function ($) {
    $(document).ready(function () {
        initializeDatepicker();
        loadEvents();
    });

    initializeDatepicker = function () {
        $('#datepicker').datepicker({
            changeMonth: false,
            changeYear: false,
            stepMonths: 0,
            showOtherMonths: true,
            onSelect: function (date, inst) {
                if (!$.isNumeric(inst.selectedDay))
                    date = getSelectedDate(inst);
                openRequestForm(date);
                loadEvents();
            }
        });
    };

    loadEvents = function () {
        var events = $.parseJSON(eventsForMonth);
        $.each(events, function (i) {
            setTimeout(function () {
                var eventDay = events[i].Day;
                if (eventDay != 'undefined' && eventDay != '') {
                    var day = $('.ui-datepicker-calendar a.ui-state-default').filter(function () { return $(this).text() == eventDay; });
                    day.html('<text>{0}</text>'.f(day.html()));
                    day.append('<div class="events"><u class="event_title">{0}</u></div>'.f(events[i].Title))
                       .addClass('have_events')
                       .hide()
                       .fadeIn();
                }
            }, i * 100);
        });
    };

    // делаем так, потому что текст в ячейках изменён (добавлен тэг <text> + события)
    // и из-за этого datepicker не парсит дату
    getSelectedDate = function (inst) {
        var selectedDay = $('<div/>')
            .html(inst.selectedDay)
            .find('text')
            .text();
        return $.datepicker.formatDate('dd.mm.yy', new Date('{0}/{1}/{2}'.f(inst.selectedYear, inst.selectedMonth + 1, selectedDay)));
    };
}(jQuery));