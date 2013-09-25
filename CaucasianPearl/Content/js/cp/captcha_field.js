(function ($) {
    $(document).ready(function () {
        Recaptcha.destroy();
        Recaptcha.create(reCaptchaPublicKey, 'reCaptcha', { theme: 'red' });

        // ��� ������� �� �����
        $('#captchaReload').click(function (e) {
            e.preventDefault();
            reloadCaptcha();
        });

        $('#captchaCode').keyup(function (e) {
            $("#recaptcha_response_field").val($(this).val());
        });
    });

    // ������������� "�����" �����
    initializeCaptcha = function () {
        // �������, ����� ������ ������������ reCaptcha
        setTimeout(function () {
            var reCaptchaImageSrc = $('#recaptcha_image img').attr('src');
            $('#captchaImage').attr('src', reCaptchaImageSrc);
        }, 300);
    };

    // ��������� "����" �����
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