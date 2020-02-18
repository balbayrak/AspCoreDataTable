var AlertTypeEnum = {
    Default: "1",
    Toast: "2",
    Sweet: "3",
    Alertify: "4",
    BootBox: "5"
};

var ConfirmTypeEnum = {
    Default: "1",
    Sweet: "2",
    Alertify: "3",
    BootBox: "4"
};

var HttpMethodType = {
    GET_METHOD: "GET",
    POST_METHOD: "POST",
};



var Alertify =
{
    showAlert: function (type, title, message) {

        if (type === "success") {
            alertify.success(message);
        } else if (type === "error") {
            alertify.error(message);
        } else if (type === "warning") {
            alertify.warning(message);
        } else {
            alertify.message(message);
        }
    },
    showConfirm: function (title, message, actionUrl, callbackfunc) {

        event.preventDefault();
        alertify.confirm(message,
            function () {
                BlockFunc.showSpinnerBlock();
                $.ajax(
                    {
                        type: 'POST',
                        url: decodeURIComponent(actionUrl),
                        success: function () {
                            if (callbackfunc == "") {
                                Confirm.pageReload();
                            }
                            else {
                                window[callbackfunc]();
                            }
                            BlockFunc.closeSpinnerBlock();
                        },
                        error: function () {
                            BlockFunc.closeSpinnerBlock();
                        }
                    }); return false;
            }
            , function () { return false; });
    }
};

var BootBox =
{
    showAlert: function (type, title, message) {
        bootbox.alert(message);
    },
    showConfirm: function (title, message, actionUrl, actionType, callbackfunc) {

        event.preventDefault();
        bootbox.confirm({
            title: title,
            message: message,
            callback: function (result) {
                if (result === true) {
                    BlockFunc.showSpinnerBlock();
                    if (actionType == HttpMethodType.POST_METHOD) {
                        $.ajax(
                            {
                                type: actionType,
                                url: decodeURIComponent(actionUrl),
                                success: function () {
                                    if (callbackfunc == "") {
                                        Confirm.pageReload();
                                    }
                                    else {
                                        window[callbackfunc]();
                                    }
                                    BlockFunc.closeSpinnerBlock();
                                },
                                error: function () {
                                    BlockFunc.closeSpinnerBlock();
                                }
                            }); return false;
                    }
                    else {
                        window.location.href = decodeURIComponent(actionUrl);
                    }
                }
            }
        });
    }
};

var ToastAlert =
{
    showAlert: function (type, title, message) {

        toastr[type](message, title);

    }
};

var DefaultAlert =
{
    showAlert: function (type, title, message) {

        alert(message);

    },
    showConfirm: function (title, message, actionUrl, actionType, callbackfunc) {

        event.preventDefault();

        if (confirm(title + " " + message)) {
            BlockFunc.showSpinnerBlock();
            if (actionType == HttpMethodType.POST_METHOD) {
                $.ajax(
                    {
                        type: actionType,
                        url: decodeURIComponent(actionUrl),
                        success: function () {
                            if (callbackfunc == "") {
                                Confirm.pageReload();
                            }
                            else {
                                window[callbackfunc]();
                            }
                            BlockFunc.closeSpinnerBlock();
                        },
                        error: function () {
                            BlockFunc.closeSpinnerBlock();
                        }
                    }); return false;
            }
            else {
                window.location.href = decodeURIComponent(actionUrl);
            }
        }
    }
};

var SweetAlert =
{
    showAlert: function (type, title, message) {

        swal(
            {
                text: message,
                title: title,
                type: type,
            });
    },
    showConfirm: function (title, message, actionUrl, actionType, callbackfunc) {

        event.preventDefault();

        swal(
            {
                title: title,
                text: message,
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d9534f',
                closeOnConfirm: true,
            }
            , function (isconfirm) {
                if (isconfirm) {
                    BlockFunc.showSpinnerBlock();
                    if (actionType == HttpMethodType.POST_METHOD) {
                        $.ajax(
                            {
                                type: actionType,
                                url: decodeURIComponent(actionUrl),
                                success: function () {
                                    if (callbackfunc == "") {
                                        Confirm.pageReload();
                                    }
                                    else {
                                        window[callbackfunc]();
                                    }
                                    BlockFunc.closeSpinnerBlock();
                                },
                                error: function () {
                                    BlockFunc.closeSpinnerBlock();
                                }
                            }); return false;
                    }
                    else {
                        window.location.href = decodeURIComponent(actionUrl);
                    }
                }
            }
        );
    }
};

var Alert =
{
    showAlert: function (type, title, message, alertType) {

        if (alertType === AlertTypeEnum.Default) {
            DefaultAlert.showAlert(type, title, message);
        } else if (alertType === AlertTypeEnum.Toast) {
            ToastAlert.showAlert(type, title, message);
        } else if (alertType === AlertTypeEnum.Sweet) {
            SweetAlert.showAlert(type, title, message);
        } else if (alertType === AlertTypeEnum.Alertify) {
            Alertify.showAlert(type, title, message);
        } else if (alertType === AlertTypeEnum.BootBox) {
            BootBox.showAlert(type, title, message);
        }
    },
};

var Confirm =
{
    showConfirm: function (title, message, actionUrl,actionType, callbackfunc, confirmType) {

        if (confirmType === ConfirmTypeEnum.Sweet) {
            SweetAlert.showConfirm(title, message, actionUrl, actionType, callbackfunc);
        } else if (confirmType === ConfirmTypeEnum.Alertify) {
            Alertify.showConfirm(title, message, actionUrl, actionType, callbackfunc);
        } else if (confirmType === ConfirmTypeEnum.BootBox) {
            BootBox.showConfirm(title, message, actionUrl,actionType,callbackfunc);
        } else if (confirmType === ConfirmTypeEnum.Default) {
            DefaultAlert.showConfirm(title, message, actionUrl, actionType, callbackfunc);
        }
    },

    pageReload: function() {
        location.reload();
    }
};
