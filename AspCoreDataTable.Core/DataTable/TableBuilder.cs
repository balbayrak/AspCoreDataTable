using AspCoreDataTable.Core.DataTable.Abstract;
using AspCoreDataTable.Core.DataTable.Columns;
using AspCoreDataTable.Core.DataTable.Storage;
using AspCoreDataTable.Core.DataTable.Toolbar;
using AspCoreDataTable.Core.DataTable.Toolbar.Buttons;
using AspCoreDataTable.Core.Extensions;
using AspCoreDataTable.Core.General;
using AspCoreDataTable.Core.General.Enums;
using AspCoreDataTable.Core.General.Portlet;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;

namespace AspCoreDataTable.Core.DataTable
{
    public class TableBuilder<TModel> : IHtmlContent, ITableBuilder<TModel>, ITableLoadBuilder<TModel> where TModel : class
    {
        private TableToolBar _tableToolBarActions { get; set; }
        private PortletForm _tablePortletSetting { get; set; }
        private EnumPagingType _pagingType { get; set; }
        private string _id { get; set; }

        private string _cssClass { get; set; }

        private bool _searchable { get; set; }

        private string _loadActionUrl { get; set; }

        private bool _stateSave { get; set; }

        private IList<ITableColumnInternal> _tableColumns { get; set; }

        public TableBuilder(string id)
        {
            this._tableColumns = new List<ITableColumnInternal>();
            this._tableToolBarActions = new TableToolBar();
            this._pagingType = EnumPagingType.bootstrap_number;
            this._id = id;
            this._searchable = true;
            this._stateSave = false;
        }

        public ITableBuilder<TModel> Columns(Action<ColumnBuilder<TModel>> columnBuilder)
        {
            if (columnBuilder != null)
            {
                ColumnBuilder<TModel> builder = new ColumnBuilder<TModel>(this);
                columnBuilder(builder);
            }
            return this;
        }

        private IHtmlContent ToHtml()
        {
            var table = new TagBuilder("table");
            table.GenerateId(_id, "");

            table.Attributes.Add(HelperConstant.General.DATA_ACTION_URL, _loadActionUrl);

            Guid uniqueId = Guid.NewGuid();

            table.Attributes.Add(HelperConstant.General.DATA_COMPONENT_UNIQUE_ID, uniqueId.ToString());

            table.Attributes.Add(HelperConstant.DataTable.DATA_SSEARCH_ENABLED, _searchable.ToString());

            table.Attributes.Add(HelperConstant.DataTable.DATA_PAGING_TYPE, _pagingType.ToString());

            table.Attributes.Add(HelperConstant.DataTable.DATA_STATE_SAVE, _stateSave.ToString());


            if (!_cssClass.Contains(HelperConstant.CssClassName.DATATABLE_CLASS))
            {
                _cssClass += " " + HelperConstant.CssClassName.DATATABLE_CLASS;
            }

            if (!_cssClass.Contains(HelperConstant.CssClassName.DATATABLE_CHECK_CLASS))
            {
                _cssClass += " " + HelperConstant.CssClassName.DATATABLE_CHECK_CLASS;
            }

            table.AddCssClass(_cssClass);

            TagBuilder thead = new TagBuilder("thead");
            TagBuilder tr = new TagBuilder("tr");
            TagBuilder tbody = new TagBuilder("tbody");

            StringBuilder sb = new StringBuilder();

            DatatableStorageObject<TModel> datatableStorageObject = new DatatableStorageObject<TModel>();
            datatableStorageObject.DatatableProperties = new List<DatatableBoundColumn<TModel>>();

            int indexCounter = 1;
            string modals = string.Empty;

            foreach (ITableColumnInternal tc in this._tableColumns)
            {
                tc.tableid = _id;

                if (tc is ITableBoundColumnInternal<TModel>)
                {
                    ITableBoundColumnInternal<TModel> boundColumn = ((ITableBoundColumnInternal<TModel>)tc);

                    if (!boundColumn.columnIsPrimaryKey)
                    {
                        tr.InnerHtml.Append(tc.HtmlColumn());
                    }

                    var bcolumn = new DatatableBoundColumn<TModel>
                    {
                        columnIsPrimaryKey = boundColumn.columnIsPrimaryKey,
                        columnProperty = boundColumn.columnProperty,
                        column_Property_Exp = boundColumn.columnPropertyExp,
                        orderByDirection = boundColumn.orderByDirection,
                        searchable = boundColumn.searchable
                    };


                    datatableStorageObject.DatatableProperties.Add(bcolumn);
                }
                else if (tc is ITableCheckColumnInternal)
                {
                    ITableCheckColumnInternal checkColumn = ((ITableCheckColumnInternal)tc);
                    checkColumn.actionColumnIndex = indexCounter++;
                    tr.InnerHtml.Append(tc.HtmlColumn());

                    datatableStorageObject.DatatableActions = datatableStorageObject.DatatableActions ?? new List<DatatableActionColumn>();
                    datatableStorageObject.DatatableActions.Add(new DatatableActionColumn { ActionColumn = checkColumn.checkActionHtml, ActionColumnHeader = checkColumn.columnDataProperty });
                }
                else
                {
                    ITableActionColumnInternal act = ((ITableActionColumnInternal)tc);
                    act.actionColumnIndex = indexCounter++;
                    tr.InnerHtml.Append(tc.HtmlColumn());

                    if (!string.IsNullOrEmpty(act.columnActionsModalHtml))
                    {
                        modals += act.columnActionsModalHtml;
                    }

                    datatableStorageObject.DatatableActions = datatableStorageObject.DatatableActions ?? new List<DatatableActionColumn>();
                    datatableStorageObject.DatatableActions.Add(new DatatableActionColumn { ActionColumn = act.columnActionsHtml, ActionColumnHeader = act.columnDataProperty });
                }
            }

            thead.InnerHtml.Append(tr.ConvertHtmlString());
            sb.Append(thead.ConvertHtmlString());
            sb.Append(tbody.ConvertHtmlString());

            table.InnerHtml.Append(sb.ToString());


            table.Attributes.Add(HelperConstant.DataTable.DATA_COLUMN_INFO, datatableStorageObject.Serialize());
            datatableStorageObject = null;
            string toolbar = string.Empty;
            if (this._tableToolBarActions != null)
            {
                bool isExportToolEnable = false;

                if (this._tableToolBarActions.exportSetting != null)
                    isExportToolEnable = this._tableToolBarActions.exportSetting.isExportCSV || this._tableToolBarActions.exportSetting.isExportExcel || this._tableToolBarActions.exportSetting.isExportPdf || this._tableToolBarActions.exportSetting.isPrintable;

                if (isExportToolEnable)
                {
                    table.Attributes.Add(HelperConstant.DataTable.DATA_EXPORTCSV, this._tableToolBarActions.exportSetting.isExportCSV.ToString().ToLower());
                    table.Attributes.Add(HelperConstant.DataTable.DATA_EXPORTEXCEL, this._tableToolBarActions.exportSetting.isExportExcel.ToString().ToLower());
                    table.Attributes.Add(HelperConstant.DataTable.DATA_EXPORTPDF, this._tableToolBarActions.exportSetting.isExportPdf.ToString().ToLower());
                    table.Attributes.Add(HelperConstant.DataTable.DATA_PRINTABLE, this._tableToolBarActions.exportSetting.isPrintable.ToString().ToLower());
                }

                string toolbarModal = string.Empty;
                toolbar = this._tableToolBarActions.GetToolBarHtml(_id, out toolbarModal);
                modals += toolbarModal;
            }

            if (this._tablePortletSetting != null)
            {
                return new HtmlString(this._tablePortletSetting.ToHtml(toolbar + table.ConvertHtmlString() + modals).ToString());

            }
            else
            {
                return new HtmlString(toolbar + table.ConvertHtmlString() + modals);
            }
        }

        public ITableBuilder<TModel> ToolBarActions(Action<ToolBarBuilder<TModel>> toolBarBuilder, TableExportSetting exportSetting)
        {
            if (toolBarBuilder != null)
            {
                ToolBarBuilder<TModel> builder = new ToolBarBuilder<TModel>(this, exportSetting);
                toolBarBuilder(builder);
            }
            this._tableToolBarActions.exportSetting = exportSetting;

            return this;
        }

        public ITableBuilder<TModel> Portlet(string title, Color color, string iClass)
        {
            this._tablePortletSetting = this._tablePortletSetting ?? new PortletForm();
            this._tablePortletSetting.title = title;
            this._tablePortletSetting.color = color;
            this._tablePortletSetting.iClass = iClass;
            return this;
        }

        public ITableBuilder<TModel> PagingType(EnumPagingType pagingType)
        {
            this._pagingType = pagingType;
            return this;
        }

        public ITableBuilder<TModel> CssClass(string cssClass)
        {
            this._cssClass = cssClass;
            return this;
        }

        public ITableBuilder<TModel> LoadAction(string loadAction)
        {
            this._loadActionUrl = loadAction;
            return this;
        }

        public ITableBuilder<TModel> Searching(bool searchable)
        {
            this._searchable = searchable;
            return this;
        }

        public ITableBuilder<TModel> StateSave(bool stateSave)
        {
            this._stateSave = stateSave;
            return this;
        }

        public void AddColumn<TProperty>(TableBoundColumn<TModel, TProperty> column)
        {
            this._tableColumns.Add(column);
        }

        public int GetColumnCount()
        {
            return this._tableColumns != null ? this._tableColumns.Count : 0;
        }

        public void AddActionColumn(TableActionColumn column)
        {
            this._tableColumns.Add(column);
        }

        public void AddCheckColumn(TableCheckColumn column)
        {
            this._tableColumns.Add(column);
        }

        public void AddToolBarAction(IToolbarModalActionButtonInternal button)
        {
            this._tableToolBarActions.ToolBarActions.Add(button);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write(this.ToHtml());

            writer.Write("<script> $(document).ready(function () {");
            writer.Write($"var {_id} = DataTableFunc.initDataTable('{_id}');");
            writer.Write("}</script>");
        }
    }
}
