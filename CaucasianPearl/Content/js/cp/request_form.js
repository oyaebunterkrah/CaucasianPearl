(function ($) {
    var requestFormLoadedSuccessful;

    $(document).ready(function () {
        // ������-������� �������� ����� ������
        $(triggerElementId).click(function (e) {
            openRequestForm();
        });

        // ����� - ����� ������
        $('#requestBlock').popup({
            height: 500,
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

        initializeFeedbackForm();
    });

    initializeFeedbackForm = function () {
        $('#requestForm').load(formUrl, function (responseText, textStatus, XMLHttpRequest) {
            if (textStatus == "error") {
                requestFormLoadedSuccessful = false;
                return;
            }

            // �������������� "����" �����
            initializeCaptcha();

            // ������ ����� �� ���� ����� ������ ��������
            $("#Phone").mask("+7 (999) 999-99-99");

            // ��� ��������� ���� ������ ��������� ����
            $(requestDateControlId).change(function () {
                var selectedDate = $(this).val();
                if (selectedDate != '')
                    $('.ui-dialog-title').text(getRequestFormTitle(selectedDate));
            });

            replaceMvcDatepicker();
        });
    };

    prepareRequestForm = function (date) {
        $('#requestForm')
            .clearData()
            .resetValidation()
            .find(requestDateControlId).val(getRequestDate(date, false));

        // �������� ����� ������ �������
        $('label.inside').inFieldLabels();

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
        $('#requestBlock').dialog({ title: getRequestFormTitle(date) }).dialog('open');
        hideLoading();
    };

    // ���������� ��������� ��� ���� ������
    getRequestFormTitle = function (date) {
        return '�������� ������ �� {0}'.f(getRequestDate(date, true));
    };

    getRequestDate = function (date, isFull) {
        if (date == undefined)
            date = $.datepicker.formatDate('dd.mm.yy', new Date());

        return isFull ? getDateStr($.datepicker.parseDate('dd.mm.yy', date)) : date;
    };

    btnSendRequestFormClick = function () {
        var form = $('#formSendRequest');
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
    onSendRequestBegin = function (success) {
        showLoading(true);
    };

    onSendRequestComplete = function (complete) {
        hideLoading(true);
    };
    
    onSendRequestSuccess = function (result) {
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

    onSendRequestFailure = function (error) {
        reloadCaptcha();
        $('#errorMessage').fadeIn();
    }; // - - - - - - - - - - - - - - - - - -
}(jQuery));