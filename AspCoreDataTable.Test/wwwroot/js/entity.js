var Entity =
{
    Init: function (entity, formid, tableid, submitClass = ".entitysubmit", successTitle = null, successMessage = null, errorTitle = null, errorMessage = null) {

        $("body").delegate(submitClass, "click", function (e) {
            e.preventDefault();
            var $btn = $(this);
            Entity.AddOrEdit($btn, entity, formid, tableid, successTitle, successMessage, errorTitle, errorMessage);
        });

        var entitydt = DataTableFunc.initDataTable(tableid);
    },
    Validate: function (formid) {
        if (!$("#" + formid).validationEngine('validate')) {
            return false;
        }
        return true;
    },
    AddOrEdit: function (btnRef,entity, formid, tableid = null, successTitle = null, successMessage = null, errorTitle = null, errorMessage = null) {
        if (this.Validate(formid)) {
            var formData = new FormData($('#' + formid)[0]);
            $.ajax({
                url: "/" + entity + "/AddOrEdit",
                type: "POST",
                data: formData,
                contentType: false,
                datatype: 'json',
                cache: false,
                processData: false,
                success: function (data) {
                    var res = JSON.parse(data);
                    if (res.Result == 1) {
                        if (successTitle != null && successMessage != null) {
                            var table = $('#' + tableid);
                            var dt = table.DataTable();
                            dt.ajax.reload();
                            Alert.showAlert("success", successTitle, successMessage, AlertTypeEnum.Sweet);
                            Modal.CloseModal(btnRef); 
                        }
                        else {
                            window.location.href = "/" + entity + "/Index";
                        }
                    }
                    else {
                        if (tableid != null && errorMessage != null && errorTitle != null) {
                            Alert.showAlert("error", errorTitle, errorMessage, AlertTypeEnum.Sweet);
                            Modal.CloseModal(btnRef);
                        }
                    }
                }
            });
        }
    }
}
var Modal =
{
    CloseModal: function (btn) {
        var $modal = btn.closest('div.custommodal');
        if ($modal) {
            $modal.removeClass("in");
            $modal.modal('hide');
        }
    }
}
