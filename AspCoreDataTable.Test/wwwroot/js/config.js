var Loader = {
    loaderStart: function (data) {
        // $("#loadSpin").show();
        $(".big-loader").show();

        $.blockUI();
  
    },
    loaderFinish: function () {
        // $("#loadSpin").hide();
        $(".big-loader").hide();
        $(".tooltipShow").tooltip();

        $.unblockUI();
    },
    AjaxTemplate: '<p class="padding-20"><i class="fa fa-spinner fa-spin"></i> İçerik yüklenirken lütfen bekleyiniz...</p>',
    AjaxSaveTemplate: '<i class="fa fa-spinner fa-spin"></i> İçerik kaydedilirken lütfen bekleyiniz...',

}


$(window).on('beforeunload', function () {
    Loader.loaderStart();
});

function StartIt() {
    $(window).on('beforeunload', function () {
        Loader.loaderStart();
    });
    return true;
}

function StopIt() {
    $(window).off('beforeunload');
    Loader.loaderFinish();
    return true;
}

$(document).ajaxStart(function (e) {
    Loader.loaderStart();
});

$(document).ajaxStop(function (e) {
    Loader.loaderFinish();
});

$(document).ajaxError(function () {
    toastr["error"]("Sistemde bir hata oluştu! Daha sonra tekrar deneyiniz.");
});

Number.prototype.format = function (n, x, s, c) {
    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\D' : '$') + ')',
        num = this.toFixed(Math.max(0, ~~n));

    return (c ? num.replace('.', c) : num).replace(new RegExp(re, 'g'), '$&' + (s || ','));
};


$(document).keyup(function (e) {
    if (e.keyCode === 27) {
        Loader.loaderFinish();
        $.unblockUI();
        $(".tooltipShow").tooltip();
    }
});




$(document).ready(function () {

    $.ajaxSetup({ cache: false });

    $("body").delegate(".toast", "click", function (e) {
        e.preventDefault();
        $(".toast").hide();
    });

    $(document.body).on('hidden.bs.modal', function () {
        $('.modal').removeData('bs.modal')
    });
});
