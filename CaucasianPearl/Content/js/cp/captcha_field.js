(function ($) {
    $(document).ready(function () {
        Recaptcha.destroy();
        Recaptcha.create(reCaptchaPublicKey, 'reCaptcha', { theme: 'red' });

        // при нажатии на капчу
        $('#captchaReload').click(function (e) {
            e.preventDefault();
            reloadCaptcha();
        });

        $('#captchaCode').keyup(function (e) {
            $("#recaptcha_response_field").val($(this).val());
        });
    });

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
}(jQuery));