
function BlockInfo(isenabled, target) {
    this.isenabled = isenabled;
    this.target = target;
}

var SubmitFunc =
{
    submitClick: function () {
        $('body').on("click", ".btn-submitform", function (e) {
            e.preventDefault();

            var $btn = $(this);

            var blockInfo = BlockFunc.controlBlockUI($btn);
            var parentModal = $btn.closest("div .modal");
            var actionurl = $btn.data("actionurl");
            var formid = $btn.data("submitformid");
            var datahtml = $btn.data('target-url');
            var methodType = $btn.data('evet-httpmethod');

            var closemodal = false;
            var closemodalStr = $btn.data('closeparentmodal');
            if (typeof closemodalStr !== 'undefined' && closemodalStr !== '' && closemodalStr !== null) {
                if (closemodalStr.toLowerCase() === "true")
                    closemodal = true;
            }

            $.ajax({
                cache: false,
                type: methodType,
                url: datahtml,
                data: $(formid).serializeArray(),
                success: function (data) {
                    if (data.isSucceded === true) {
                        if (blockInfo.isenabled) {
                            BlockFunc.closeSpinnerBlock(blockInfo.target);
                        }
                        if (closemodal) $(parentModal).modal('hide');
                        reloadTable('itemquestionstable');
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    if (blockInfo.isenabled) {
                        BlockFunc.closeSpinnerBlock(blockInfo.target);
                    }
                    alert('Veriler getirilirken hata oluştu.');
                }
            });

        });
    }
};

var DismissFunc =
{
    dismissClick: function () {
        $('body').on("click", ".btndismiss", function (e) {
            e.preventDefault();

            var $btn = $(this);
            var $modal = $btn.closest('div.custommodal');
            $modal.removeClass("in");
            $modal.modal('hide');

        });
    }
};

var ModalFunc = {
    clickModal: function () {
        $('body').on("click", ".btn-blockui-modal", function (e) {

            e.preventDefault();

            var $btn = $(this);

            var blockInfo = BlockFunc.controlBlockUI($btn);

            var datatarget = $btn.data('target');
            var datatargetBody = $btn.data('target-body');
            var datahtml = $btn.data('target-url');
            var methodType = $btn.data('evet-httpmethod');

            $.ajax({
                url: datahtml,
                type: methodType,
                success: function (data) {
                    if (blockInfo.isenabled) {
                        BlockFunc.closeSpinnerBlock(blockInfo.target);
                    }
                    $(datatargetBody).html(data);
                    $(datatarget).modal('show');
                },
                error: function (data) {
                    if (blockInfo.isenabled) {
                        BlockFunc.closeSpinnerBlock(blockInfo.target);
                    }
                }
            });
        });
    }
};

var SpinnerButton =
{
    clickButton: function () {
        $('.start').on('click', function () {
            var $btn = $(this);
            var blockInfo = BlockFunc.controlBlockUI($btn);

            $btn.button('loading');
            setTimeout(function () {
                $btn.button('reset');
            }, 300000);
        });
    },

    clickBlockUIButton: function () {
        $('body').on("click", ".blockuibutton", function (e) {

            var $btn = $(this);

            var blockInfo = BlockFunc.controlBlockUI($btn);
            var targeturl = $btn.data('target-url');
            window.location.href = targeturl;
        });
    },
    clickDownloadButton: function () {
        $('body').on("click", ".downloadbutton", function (e) {
            var $btn = $(this);

            var blockInfo = BlockFunc.controlBlockUI($btn);

            var targeturl = $btn.data('target-url');

            if (targeturl.startsWith("/") === false) {
                targeturl = "/" + targeturl;
            }

            var result = targeturl.split('/');

            var id = "";

            if (result.length > 3) {
                id = result[3];
            }

            var controller = result[1];
            var action = result[2];
            var methodType = $btn.data('evet-httpmethod');

            $.ajax({
                cache: false,
                type: methodType,
                url: "/" + controller + "/" + action,
                data: {
                    "id": id
                },
                success: function (returnValue) {
                    if (blockInfo.isenabled) {
                        BlockFunc.closeSpinnerBlock(blockInfo.target);
                    }
                    if (returnValue.isSucceded === true) {
                        window.location = "/" + controller + "/DownloadDocument?documentUrl=" + returnValue.documentUrl;
                    }
                },
                error: function (data) {
                    if (blockInfo.isenabled) {
                        BlockFunc.closeSpinnerBlock(blockInfo.target);
                    }
                }
            });
        });
    }
};

var BlockFunc =
{
    controlBlockUI: function (target) {
        var info = new BlockInfo(false, "");

        var datablockui = target.data('blockui');
        if (datablockui !== null && typeof datablockui !== 'undefined' && datablockui !== '')
            datablockui = datablockui.toLowerCase();
        else datablockui = 'false';

        if (datablockui === 'true') {
            var blocktarget = target.data('blocktarget');
            if (blocktarget !== null && typeof blocktarget !== 'undefined' && blocktarget !== '')
                blocktarget = blocktarget.toLowerCase();
            else blocktarget = '';

            BlockFunc.showSpinnerBlock(blocktarget);
            info.isenabled = true;
            info.target = blocktarget;
            return info;
        }
        else {
            return info;
        }
    },

    showSpinnerBlock: function (target) {
        if (target === "#" || target === '' || target === null || typeof target === 'undefined') {
            Loader.loaderStart();
        }
        else {
            if (!target.startsWith("#")) {
                target = '#' + target.trim();
            }

            Loader.loaderStart();
        }
    },

    closeSpinnerBlock: function (target) {
        if (target === "#" || target === '' || target === null || typeof target === 'undefined')
            // App.unblockUI();
            Loader.loaderFinish();
        else {
            if (!target.startsWith("#")) {
                target = '#' + target.trim();
            }
            //  App.unblockUI(target);
            Loader.loaderFinish();
        }
    }
};

var Loader = {
    loaderStart: function (data) {
        $(".big-loader").show();
        $.blockUI();
    },
    loaderFinish: function () {
        $(".big-loader").hide();

        $.unblockUI();
    },
    AjaxTemplate:
        '<p class="padding-20"><i class="fa fa-spinner fa-spin"></i> İçerik yüklenirken lütfen bekleyiniz...</p>',
    AjaxSaveTemplate: '<i class="fa fa-spinner fa-spin"></i> İçerik kaydedilirken lütfen bekleyiniz...'

};


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
    var $modal = $('div.custommodal');
    $modal.removeClass("in");
    $modal.modal('hide');
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
    }
});

$.fn.extend(
    {
        serializeObject: function () {
            var o = {};
            var a = this.serializeArray();
            $.each(a, function () {
                if (o[this.name]) {
                    if (!o[this.name].push) {
                        o[this.name] = [o[this.name]];
                    }
                    o[this.name].push(this.value || '');
                } else {
                    o[this.name] = this.value || '';
                }
            });
            return o;
        }
    });

$.extend({
    getParams: function (url, keyname) {
        var returnvalue = "";
        var match = url.match(/\?(.*)$/);

        if (match && match[1]) {
            match[1].split('&').forEach(function (pair) {
                pair = pair.split('=');
                if (pair[0] === keyname)
                    returnvalue = decodeURIComponent(pair[1]);
            });
        }

        return returnvalue;
    },
    getUrlVars: function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = decodeURIComponent(hash[1]);
        }
        return vars;
    },
    getUrlVar: function (name) {
        return decodeURIComponent($.getUrlVars()[name]);
    },
    getValidationMessage: function (selector, validatorname) {
        var text = $(selector).data(validatorname + '-msg');
        return text;
    },
    isNumber: function (evt) {
        evt = evt ? evt : window.event;
        var charCode = evt.which ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
});



$(document).ready(function () {
    DismissFunc.dismissClick();
    SubmitFunc.submitClick();
    ModalFunc.clickModal();
    SpinnerButton.clickButton();
    SpinnerButton.clickBlockUIButton();
    SpinnerButton.clickDownloadButton();

    $(".post-method-logoff").on("click", function (e) {
        e.preventDefault();
        var hrefvalue = $(this).attr('href');
        $.ajax({
            cache: false,
            type: "POST",
            url: hrefvalue,
            data: {},
            success: function (data) {
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert('Veriler getirilirken hata oluştu.');
            }
        });

    });
});



