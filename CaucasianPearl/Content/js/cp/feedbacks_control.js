(function ($) {
    var numWords = 10;

    $(document).ready(function () {
        $('#previousFeedback').click(function (e) {
            e.preventDefault();
            var self = $(this);

            if (self.hasClass('disabled'))
                return;

            var options = {
                url: '/feedback/getpreviousfeedback',
                type: 'POST',
                data: { id: $(this).data('feedbackid') },
                success: function (result) { onSuccess(result, self); },
                error: function (result) { onError(result); }
            };

            ajaxRequest(options);
        });

        $('#nextFeedback').click(function (e) {
            e.preventDefault();
            var self = $(this);

            if (self.hasClass('disabled'))
                return;

            var options = {
                url: '/feedback/getnextfeedback',
                type: 'POST',
                data: { id: $(this).data('feedbackid') },
                success: function (result) { onSuccess(result, self); },
                error: function (result) { onError(result); }
            };

            ajaxRequest(options);
        });

        // т.к. при первой загрузке подгружается последний отзыв
        $('#nextFeedback').addClass('disabled');

        $('.trunc').trunc(numWords);
    });
    
    onSuccess = function (result, sender) {
        if (result.IsLast) {
            $('.navigate.disabled').removeClass('disabled');
            sender.addClass('disabled');
        } else
            $('.navigate.disabled').removeClass('disabled');
        
        $('#feedbackName').fadeOut(125, function () { $(this).text(result.Name).fadeIn(); });
        $('#feedbackCity').fadeOut(250, function () { $(this).text(result.City).fadeIn(); });
        $('#feedbackComment').fadeOut(375, function () { $(this).text(result.Comment).trunc(numWords).fadeIn(); });
        $('#feedbackCreated').fadeOut(500, function () { $(this).text(result.Created).fadeIn(); });

        $('#previousFeedback, #nextFeedback').data('feedbackid', result.ID);

        //$('#feedbackName').hide('slide', { direction: 'left' }, 125, function () { $(this).text(result.Name).show('slide', { direction: 'left' }); });
        //$('#feedbackCity').hide('slide', { direction: 'left' }, 250, function () { $(this).text(result.City).show('slide', { direction: 'left' }); });
        //$('#feedbackComment').hide('slide', { direction: 'left' }, 375, function () { $(this).text(result.Comment).show('slide', { direction: 'left' }); });
        //$('#feedbackCreated').hide('slide', { direction: 'left' }, 500, function () { $(this).text(result.Created).show('slide', { direction: 'left' }); });

    };

    onError = function (result) {
        alert(result);
    };
}(jQuery));