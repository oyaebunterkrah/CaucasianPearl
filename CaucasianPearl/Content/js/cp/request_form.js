(function ($) {
    var requestFormLoadedSuccessful;

    $(document).ready(function () {
        // кнопка-триггер открытия формы заявки
        $(elementId).click(function (e) {
            openRequestForm();
        });

        // попап - форма заявки
        $('#requestBlock').popup({
            height: 610,
            width: 500,
            buttons:
            [
                {
                    text: 'Отправить',
                    id: 'btnSend',
                    click: function () {
                        btnSendRequestFormClick();
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

        initializeRequestForm();
    });

    initializeRequestForm = function () {
        $('#requestForm').load(formUrl, function (responseText, textStatus, XMLHttpRequest) {
            if (textStatus == "error") {
                requestFormLoadedSuccessful = false;
                return;
            }

            Recaptcha.create(reCaptchaPublicKey, 'reCaptcha', { theme: 'red' });

            // при нажатии на капчу
            $('#captchaReload').click(function (e) {
                e.preventDefault();
                reloadCaptcha();
            });

            $('#captchaCode').keyup(function (e) {
                $("#recaptcha_response_field").val($(this).val());
            });

            // инициализируем "нашу" капчу
            initializeCaptcha();

            // ставим маску на поле ввода номера телефона
            $("#Phone").mask("+7 (999) 999-99-99");

            // при изменении даты меняем заголовок окна
            $('#RequestDate').change(function () {
                var selectedDate = $(this).val();
                if (selectedDate != '')
                    $('.ui-dialog-title').text(getRequestFormTitle(selectedDate));
            });

            replaceMvcDatepicker();
        });
    };

    // инициализация "нашей" капчи
    initializeCaptcha = function () {
        // таймаут, чтобы успела прогрузиться reCaptcha
        setTimeout(function () {
            var reCaptchaImageSrc = $('#recaptcha_image img').attr('src');
            $('#captchaImage').attr('src', reCaptchaImageSrc);
        }, 300);
    };

    // обновляем "нашу" капчу
    reloadCaptcha = function (showError) {
        var captchaCode = $('#captchaCode'),
            captchaError = $('#captchaError');
        Recaptcha.reload();
        captchaCode.val('');
        initializeCaptcha();
        if (showError)
            captchaError
                .removeClass('field-validation-valid')
                .addClass('field-validation-error');
        else
            captchaError
                .removeClass('field-validation-error')
                .addClass('field-validation-valid');

        captchaCode.focus();
    };

    prepareRequestForm = function (date) {
        $('#requestForm')
            .clearData()
            .resetValidation()
            .find('#RequestDateTime').val(date);

        reloadCaptcha();
        hideStatusInfo();
    };

    openRequestForm = function (date) {
        if (requestFormLoadedSuccessful == false) {
            alert('Возникла неожиданная ошибка');
            return;
        }

        showLoading();
        prepareRequestForm(date);
        $('#requestBlock').dialog({ title: getRequestFormTitle(date) }).dialog("open");
        hideLoading();
    };

    // возвращает заголовок для окна заявки
    getRequestFormTitle = function (date) {
        var dateStr = getDateStr($.datepicker.parseDate('dd.mm.yy', date));
        return 'Оставить заявку на {0}'.f(dateStr);
    };

    btnSendRequestFormClick = function () {
        var form = $('#formCreateRequest');
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
    onBegin = function (success) {
        showLoading(true);
    };

    onComplete = function (complete) {
        hideLoading(true);
    };
    
    onSuccess = function (result) {
        if (result.success) {
            $('#requestBlock').dialog('close');
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

    onFailure = function (error) {
        reloadCaptcha();
        $('#errorMessage').fadeIn();
    }; // - - - - - - - - - - - - - - - - - -
}(jQuery));