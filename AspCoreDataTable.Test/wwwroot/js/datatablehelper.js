var DataTableConstant =
{
    EmptyMessage: "Tabloda herhangi bir veri mevcut değil",
    SInfo: "_MAX_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
    LoadingRecords: "Yükleniyor...",
    SSearch: "Ara",
    PaginateFirst: "İlk",
    PaginateLast: "Son",
    PaginateNext: "Sonraki",
    PaginatePrevious: "Önceki"
};


var DataTableFunc = {

    initDataTable: function (tableid) {
        var item = $("#" + tableid);
        var uniqueid = $(item).data('uid');
        var action = $(item).data('actionurl');
        var aoColumns = new Array();
        var orderColumns = new Array();
        var headers = $(item).find('th');

        var autowidth = false;
        for (var j = 0; j < headers.length; j++) {
            var prop = $(headers[j]).data('property');

            var sortable = false;
            var orderprop = $(headers[j]).data('orderby');
            if (orderprop !== '#') {
                orderColumns.push([j, orderprop]);
                sortable = true;
            }
            var widthstr = $(item).data('width');
            autowidth = false;
            if (typeof widthstr !== 'undefined' && widthstr !== '' && widthstr !== null) {
                widthstr = 'auto';
                autowidth = true;
            }

            aoColumns.push({ 'mDataProp': prop, "bSortable": sortable, "sWidth": widthstr });
        }


        var ssearchenabled = false;
        var ssearchenabledstr = $(item).data('ssearch-enabled');

        var pagingtype = $(item).data('paging-type');

        var stateSave = $(item).data('state-save');

        var columnInfo = $(item).data('columninfo');

        if (typeof ssearchenabledstr !== 'undefined') {
            if (ssearchenabledstr !== null && ssearchenabledstr.toLowerCase() === "true")
                ssearchenabled = true;
        }
        var tablebuttons = DataTableFunc.tableToolbarInit($(item));

        var dt = $(item).dataTable({
            "lengthMenu": [[5, 10, 15], [5, 10, 15]],
            "pageLength": 5,
            "searching": ssearchenabled,
            "stateSave": stateSave,
            "pagingType": pagingtype,
            "retrieve": true,
            "bProcessing": true,
            "bServerSide": true,
            "sAjaxSource": action,
            "sServerMethod": "POST",
            "bAutoWidth": autowidth,
            "bPaginate": true,
            "order": orderColumns,
            "oLanguage": {
                "sDecimal": ",",
                "sEmptyTable": DataTableConstant.EmptyMessage,
                "sInfo": DataTableConstant.SInfo,
                "sInfoEmpty": "",
                "sLoadingRecords": DataTableConstant.LoadingRecords,
                "sProcessing": DataTableConstant.LoadingRecords,
                "sSearch": DataTableConstant.SSearch,
                "oPaginate": {
                    "sFirst": DataTableConstant.PaginateFirst,
                    "sLast": DataTableConstant.PaginateLast,
                    "sNext": DataTableConstant.PaginateNext,
                    "sPrevious": DataTableConstant.PaginatePrevious
                }
            },
            buttons: tablebuttons,
            "aoColumns": aoColumns,

            fnServerParams: function (aoData) {
                aoData.push({ name: "datatableId", value: uniqueid });
                aoData.push({ name: "columnInfo", value: columnInfo });
            },
            //initComplete: function (settings, json) {
            //    var api = new $.fn.dataTable.Api(settings);
            //    var tableColumns = api.rows().eq(0).columns();
            //    tableColumns.every(function (index) {
            //        var data = this.data();
            //        if (data[0] == "-") {
            //            api.columns([index]).visible(false);
            //        }
            //    });
            //}
            "fnRowCallback": function (nRow, aData) {
                if (aData[nRow._DT_RowIndex] != null && aData[nRow._DT_RowIndex] != 'undefined') {
                    $('td', nRow).css('background-color', '#f2dede');
                }
            }
        });


        $("#" + tableid + '_paginate').click(function () {
            var thead = item.find("thead");
            var chkAll = item.find(':input:checkbox[name="selectAll"]');
            chkAll.prop('checked', false);
        });
        return dt;
    },

    tableToolbarInit: function (table) {
        var btns = new Array();

        var isexportcsv = $(table).data('exportcsv');
        if (isexportcsv)
            btns.push({ extend: 'csv', className: 'btn default', footer: false, exportOptions: { columns: "thead th:not(.noexport)" } });

        var isexportexcel = $(table).data('exportexcel');
        if (isexportexcel)
            btns.push({
                extend: 'excel', className: 'btn default', footer: false, exportOptions: { columns: "thead th:not(.noexport)" }
            });

        var isexportpdf = $(table).data('exportpdf');
        if (isexportpdf)
            btns.push({ extend: 'pdf', className: 'btn default', footer: false, exportOptions: { columns: "thead th:not(.noexport)" } });

        var isprintable = $(table).data('printable');
        if (isprintable)
            btns.push({
                extend: 'print', className: 'btn default', footer: false, exportOptions: { columns: "thead th:not(.noexport)" }
            });

        return btns;
    }
};

var TableCheckFunc = {

    checkFunc: function () {

        $("table").on("click", ".group-checkable", function (e) {
            var chkall = e.target;
            var table = $(chkall).closest('table');
            $('td input:checkbox', table).prop('checked', this.checked);
        });

        $("table").on("click", ".checkboxes", function (e) {
            var table = $(e.target).closest('table');
            var thead = table.find("thead");
            var chkAll = thead.find(':input:checkbox[name="selectAll"]');
            if ($(this).is(":checked") === false) {
                chkAll.prop('checked', false);
            }
        });



        $.fn.getAllCheckedValues = function (propertyname) {
            var table = $(this);
            var qs = '';
            $('td input[name="' + "checkboxRow" + propertyname + '"]:checked', table).each(function () {
                qs += this.value + ',';
            });
            return qs;
        };
    }
};

function reloadTable(tableid) {
    var table = $("#" + tableid);
    var dt = table.DataTable();
    dt.draw();
}

$(function () {

    TableCheckFunc.checkFunc();

    $("body").delegate("li > a.tool-action", "click", function (e) {
        var link = e.target;
        var action = $(link).data('action');
        var toolbar = $(link).closest("div .table-toolbar");
        var tableId = '#' + $(toolbar).data('tableid');
        var table = $("body").find(tableId);
        table.DataTable().button(action).trigger();
    });
});


