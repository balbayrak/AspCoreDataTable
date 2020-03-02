var Entity =
{
    Init: function(entity,
        formid,
        tableid,
        submitClass = ".entitysubmit",
        successTitle = null,
        errorMessage = null) {
        $("body").delegate(submitClass,
            "click",
            function(e) {
                e.preventDefault();
                var $btn = $(this);
                Entity.AddOrEdit($btn, entity, formid, tableid, successTitle,  errorMessage);
            });

        var entitydt = DataTableFunc.initDataTable(tableid);
    },
    Validate: function(formid) {
        if (!$("#" + formid).validationEngine('validate')) {
            return false;
        }
        return true;
    },
    AddOrEdit: function(btnRef,
        entity,
        formid,
        tableid = null,
        successTitle = null,
        errorMessage = null) {
        if (this.Validate(formid)) {
            var formData = new FormData($('#' + formid)[$('#formPerson').length -1]);
            $.ajax({
                url: "/" + entity + "/AddOrEdit",
                type: "POST",
                data: formData,
                contentType: false,
                datatype: 'json',
                cache: false,
                processData: false,
                success: function(data) {
                    var res = JSON.parse(data);
                    if (res.Result === 1) {
                        if (successTitle !== null) {
                            var table = $('#' + tableid);
                            var dt = table.DataTable();
                            dt.ajax.reload();
                            Alert.showAlert("success", successTitle, res.ResultText, AlertTypeEnum.Sweet);
                            Modal.CloseModal(btnRef);
                        } else {
                            window.location.href = "/" + entity + "/Index";
                        }
                    } else {
                        if (tableid !== null) {
                            Alert.showAlert("error", errorTitle, errorMessage, AlertTypeEnum.Sweet);
                            Modal.CloseModal(btnRef);
                        }
                    }
                }
            });
        }
    }
};
var Modal =
{
    CloseModal: function(btn) {
        var $modal = btn.closest('div.custommodal');
        if ($modal) {
            $modal.removeClass("in");
            $modal.modal('hide');
        }
    }
};
