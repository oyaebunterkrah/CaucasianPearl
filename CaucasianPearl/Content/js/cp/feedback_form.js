(function ($) {
    var feedbackFormLoadedSuccessful;

    $(document).ready(function () {
        // ������-������� �������� ����� ������
        $(triggerElementId).click(function (e) {
            openFeedbackForm();
        });

        // ����� - ����� ������
        $('#feedbackBlock').popup({
            height: 500,
            width: 350,
            buttons:
            [
                {
                    text: '���������',
                    id: 'btnSend',
                    click: function () {
                        btnSendFeedbackFormClick();
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

        initializeFeedbackForm();
    });

    initializeFeedbackForm = function () {
        $('#feedbackForm').load(formUrl, function (responseText, textStatus, XMLHttpFeedback) {
            if (textStatus == "error") {
                feedbackFormLoadedSuccessful = false;
                return;
            }

            // �������������� "����" �����
            initializeCaptcha();
        });
    };

    prepareFeedbackForm = function (date) {
        $('#feedbackForm')
            .clearData()
            .resetValidation();

        // �������� ����� ������ �������
        $('label.inside').inFieldLabels();
        
        reloadCaptcha();
        hideStatusInfo();
    };

    openFeedbackForm = function (date) {
        if (feedbackFormLoadedSuccessful == false) {
            alert('�������� ����������� ������');
            return;
        }

        showLoading();
        prepareFeedbackForm(date);
        $('#feedbackBlock').dialog({ title: '�������� �����' }).dialog('open');
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