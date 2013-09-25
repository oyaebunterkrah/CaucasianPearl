(function ($) {
    var feedbackFormLoadedSuccessful;

    $(document).ready(function () {
        // кнопка-триггер открытия формы заявки
        $(triggerElementId).click(function (e) {
            openFeedbackForm();
        });

        // попап - форма заявки
        $('#feedbackBlock').popup({
            height: 500,
            width: 350,
            buttons:
            [
                {
                    text: 'Отправить',
                    id: 'btnSend',
                    click: function () {
                        btnSendFeedbackFormClick();
                    }
                },
                {
                    text: 'Отмена',
                    id: 'btnCancel',
                    click: function () {
                        closeDialogs();
                    }
                }
            ],
        });

        // попап - спасибо
        $('#thanks').popup({
            title: 'Спасибо!',
            buttons:
            [
                {
                    text: 'Закрыть',
                    id: 'btnClose',
                    click: function () { closeDialogs(); }
                }
            ],
        });

        initializeFeedbackForm();
    });

    initializeFeedbackForm = function () {
        $('#feedbackForm').load(formUrl, function (responseText, textStatus, XMLHttpFeedback) {
            if (textStatus == "error") {
                feedbackFormLoadedSuccessful = false;
                return;
            }

            // инициализируем "нашу" капчу
            initializeCaptcha();
        });
    };

    prepareFeedbackForm = function (date) {
        $('#feedbackForm')
            .clearData()
            .resetValidation();

        // название полей внутри инпутов
        $('label.inside').inFieldLabels();
        
        reloadCaptcha();
        hideStatusInfo();
    };

    openFeedbackForm = function (date) {
        if (feedbackFormLoadedSuccessful == false) {
            alert('Возникла неожиданная ошибка');
            return;
        }

        showLoading();
        prepareFeedbackForm(date);
        $('#feedbackBlock').dialog({ title: 'Оставить отзыв' }).dialog('open');
        hideLoading();
    };

    btnSendFeedbackFormClick = function () {
        var form = $('#formSendFeedback');
        if (form.isValid()) {
            hideStatusInfo();

            form.submit();
        }
    };

    hideStatusInfo = function () {
        $('#thanks').hide();
        $('#errorMessage').hide();
        hideLoading(true);
    };

    // callback {
    // - - - - - - - - - - - - - - - - - - -
    onSendFeedbackBegin = function (success) {
        showLoading(true);
    };

    onSendFeedbackComplete = function (complete) {
        hideLoading(true);
    };

    onSendFeedbackSuccess = function (result) {
        if (result.success) {
            $('#feedbackBlock').dialog('close');
            setTimeout(function () {
                $('#thanks').dialog('open');
            }, 500);
        } else {
            if (result.errorMessage != '')
                $('#errorMessage')
                    .html(result.errorMessage)
                    .fadeIn();
            reloadCaptcha(true);
        }
    };

    onSendFeedbackFailure = function (error) {
        reloadCaptcha();
        $('#errorMessage').fadeIn();
    }; // - - - - - - - - - - - - - - - - - -
}(jQuery));