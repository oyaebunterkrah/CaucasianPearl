(function ($) {
    var requestFormLoadedSuccessful;

    $(document).ready(function () {
        // ������-������� �������� ����� ������
        $(elementId).click(function (e) {
            openRequestForm();
        });

        // ����� - ����� ������
        $('#requestBlock').popup({
            height: 610,
            width: 500,
            buttons:
            [
                {
                    text: '���������',
                    id: 'btnSend',
                    click: function () {
                        btnSendRequestFormClick();
                    }
                },
                {
                    text: '������',
                    id: 'btnCancel',
                    click: function () {
                        closeDialogs();
                    }
                }
            ],
        });

        // ����� - �������
        $('#thanks').popup({
            title: '�������!',
            buttons:
            [
                {
                    text: '�������',
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

            // ��� ������� �� �����
            $('#captchaReload').click(function (e) {
                e.preventDefault();
                reloadCaptcha();
            });

            $('#captchaCode').keyup(function (e) {
                $("#recaptcha_response_field").val($(this).val());
            });

            // �������������� "����" �����
            initializeCaptcha();

            // ������ ����� �� ���� ����� ������ ��������
            $("#Phone").mask("+7 (999) 999-99-99");

            // ��� ��������� ���� ������ ��������� ����
            $('#RequestDate').change(function () {
                var selectedDate = $(this).val();
                if (selectedDate != '')
                    $('.ui-dialog-title').text(getRequestFormTitle(selectedDate));
            });

            replaceMvcDatepicker();
        });
    };

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
            alert('�������� ����������� ������');
            return;
        }

        showLoading();
        prepareRequestForm(date);
        $('#requestBlock').dialog({ title: getRequestFormTitle(date) }).dialog("open");
        hideLoading();
    };

    // ���������� ��������� ��� ���� ������
    getRequestFormTitle = function (date) {
        var dateStr = getDateStr($.datepicker.parseDate('dd.mm.yy', date));
        return '�������� ������ �� {0}'.f(dateStr);
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