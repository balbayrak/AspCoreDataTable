
var AjaxResultEnum = {
    Success: 1,
    Warning: 2,
    Error: 0
};

var Entity =
{
    Init: function(entity,
        formid,
        tableid,
        submitClass = ".entitysubmit",
        successTitle = null,
        errorTitle = null
     ) {

        $("body").delegate(submitClass,
            "click",
            function(e) {
                e.preventDefault();
                var value = Entity.AddOrEdit(entity,
                    formid,
                    tableid,
                    successTitle,
                    errorTitle
                   );
                if (value === true) {
                    var $btn = $(this);
                    var $modal = $btn.closest('div.custommodal');
                    if ($modal) {
                        $modal.removeClass("in");
                        $modal.modal('hide');
                        $modal.removeData('modal');
                    }
                }
            });
        var entitydt = DataTableFunc.initDataTable(tableid);
    },
    Validate: function(formid) {
        if (!$("#" + formid).validationEngine('validate')) {
            return false;
        }
        return true;
    },
    AddOrEdit: function(entity,
        formid,
        tableid = null,
        successTitle = null,
        errorTitle = null
      ) {
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
                success: function(data) {
                    var res = JSON.parse(data);
                    if (res.Result === AjaxResultEnum.Success) {
                        if (successTitle !== null) {
                            var table = $('#' + tableid);
                            var dt = table.DataTable();
                            dt.ajax.reload();
                            Alert.showAlert("success", successTitle, res.ResultText, AlertTypeEnum.Sweet);
                        } else {
                            window.location.href = "/" + entity + "/Index";
                        }
                    } else {
                        if (tableid !== null && errorTitle !== null) {
                            Alert.showAlert("error", errorTitle, res.ResultText, AlertTypeEnum.Sweet);
                        }
                    }
                },
                
            });
            return true;
        } else
            return false;
    }
};
