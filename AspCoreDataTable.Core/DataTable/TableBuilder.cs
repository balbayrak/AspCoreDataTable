using AspCoreDataTable.Core.DataTable.Abstract;
using AspCoreDataTable.Core.DataTable.Columns;
using AspCoreDataTable.Core.DataTable.Storage;
using AspCoreDataTable.Core.DataTable.Toolbar;
using AspCoreDataTable.Core.DataTable.Toolbar.Buttons;
using AspCoreDataTable.Core.Extensions;
using AspCoreDataTable.Core.General;
using AspCoreDataTable.Core.General.Portlet;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AspCoreDataTable.Core.DataTable
{
    public class TableBuilder<TModel> : ITableBuilder<TModel> where TModel : class
    {
        private TableToolBar TableToolBarActions { get; set; }
        private PortletForm TablePortletSetting { get; set; }

        private IList<ITableColumnInternal> TableColumns { get; set; }

        public TableBuilder()
        {
            this.TableColumns = new List<ITableColumnInternal>();
            this.TableToolBarActions = new TableToolBar();
        }

        public TableBuilder<TModel> Columns(Action<ColumnBuilder<TModel>> columnBuilder)
        {
            if (columnBuilder != null)
            {
                ColumnBuilder<TModel> builder = new ColumnBuilder<TModel>(this);
                columnBuilder(builder);
            }
            return this;
        }

        public IHtmlContent ToHtml(string id, string actionUrl, string CssClass, bool isSearchEnabled = true)
        {
            var table = new TagBuilder("table");
            table.GenerateId(id, "");

            table.Attributes.Add(HelperConstant.General.DATA_ACTION_URL, actionUrl);
            
            Guid uniqueId = Guid.NewGuid();

            table.Attributes.Add(HelperConstant.General.DATA_COMPONENT_UNIQUE_ID, uniqueId.ToString());

            table.Attributes.Add(HelperConstant.DataTable.DATA_SSEARCH_ENABLED, isSearchEnabled.ToString());


            if (!CssClass.Contains(HelperConstant.CssClassName.DATATABLE_CLASS))
            {
                CssClass += " " + HelperConstant.CssClassName.DATATABLE_CLASS;
            }

            if (!CssClass.Contains(HelperConstant.CssClassName.DATATABLE_CHECK_CLASS))
            {
                CssClass += " " + HelperConstant.CssClassName.DATATABLE_CHECK_CLASS;
            }

            table.AddCssClass(CssClass);

            TagBuilder thead = new TagBuilder("thead");
            TagBuilder tr = new TagBuilder("tr");
            TagBuilder tbody = new TagBuilder("tbody");

            StringBuilder sb = new StringBuilder();

            DatatableStorageObject<TModel> datatableStorageObject = new DatatableStorageObject<TModel>();
            datatableStorageObject.DatatableProperties = new List<DatatableBoundColumn<TModel>>();

            int indexCounter = 1;
            string modals = string.Empty;

            foreach (ITableColumnInternal tc in this.TableColumns)
            {
                tc.tableid = id;

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
            if (this.TableToolBarActions != null)
            {
                bool isExportToolEnable = false;

                if (this.TableToolBarActions.exportSetting != null)
                    isExportToolEnable = this.TableToolBarActions.exportSetting.isExportCSV || this.TableToolBarActions.exportSetting.isExportExcel || this.TableToolBarActions.exportSetting.isExportPdf || this.TableToolBarActions.exportSetting.isPrintable;

                if (isExportToolEnable)
                {
                    table.Attributes.Add(HelperConstant.DataTable.DATA_EXPORTCSV, this.TableToolBarActions.exportSetting.isExportCSV.ToString().ToLower());
                    table.Attributes.Add(HelperConstant.DataTable.DATA_EXPORTEXCEL, this.TableToolBarActions.exportSetting.isExportExcel.ToString().ToLower());
                    table.Attributes.Add(HelperConstant.DataTable.DATA_EXPORTPDF, this.TableToolBarActions.exportSetting.isExportPdf.ToString().ToLower());
                    table.Attributes.Add(HelperConstant.DataTable.DATA_PRINTABLE, this.TableToolBarActions.exportSetting.isPrintable.ToString().ToLower());
                }

                string toolbarModal = string.Empty;
                toolbar = this.TableToolBarActions.GetToolBarHtml(id, out toolbarModal);
                modals += toolbarModal;
            }

            if (this.TablePortletSetting != null)
            {
                return new HtmlString(this.TablePortletSetting.ToHtml(toolbar + table.ConvertHtmlString() + modals).ToString());

            }
            else
            {
                return new HtmlString(toolbar + table.ConvertHtmlString() + modals);
            }
        }

        public TableBuilder<TModel> ToolBarActions(Action<ToolBarBuilder<TModel>> toolBarBuilder, TableExportSetting exportSetting)
        {
            if (toolBarBuilder != null)
            {
                ToolBarBuilder<TModel> builder = new ToolBarBuilder<TModel>(this, exportSetting);
                toolBarBuilder(builder);
            }
            this.TableToolBarActions.exportSetting = exportSetting;

            return this;
        }

        public TableBuilder<TModel> Portlet(string title, Color color, string iClass)
        {
            this.TablePortletSetting = this.TablePortletSetting ?? new PortletForm();
            this.TablePortletSetting.title = title;
            this.TablePortletSetting.color = color;
            this.TablePortletSetting.iClass = iClass;
            return this;
        }

        public void AddColumn<TProperty>(TableBoundColumn<TModel, TProperty> column)
        {
            this.TableColumns.Add(column);
        }

        public int GetColumnCount()
        {
            return this.TableColumns != null ? this.TableColumns.Count : 0;
        }

        public void AddActionColumn(TableActionColumn column)
        {
            this.TableColumns.Add(column);
        }

        public void AddCheckColumn(TableCheckColumn column)
        {
            this.TableColumns.Add(column);
        }

        public void AddToolBarAction(IToolbarModalActionButtonInternal button)
        {
            this.TableToolBarActions.ToolBarActions.Add(button);
        }
    }
}
