using AspCoreDataTable.Core.Button.Concrete;
using AspCoreDataTable.Core.DataTable.Toolbar.Buttons;
using AspCoreDataTable.Core.Extensions;
using AspCoreDataTable.Core.General;
using AspCoreDataTable.Core.General.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AspCoreDataTable.Core.DataTable.Toolbar
{
    public class TableToolBar
    {
        public TableExportSetting exportSetting { get; set; }

        public IList<IToolbarModalActionButtonInternal> ToolBarActions { get; set; }

        public TableToolBar()
        {
            this.exportSetting = new TableExportSetting();
            this.ToolBarActions = new List<IToolbarModalActionButtonInternal>();
        }

        public void GetToolBarActions(string tableId, out string actionLeftString, out string actionRightString, out string toolbarmodals)
        {
            string actionRight = string.Empty;
            string actionLeft = string.Empty;
            string modal = string.Empty;
            
            if (this.ToolBarActions != null)
            {
                string customActions = string.Empty;
                int indexCounter = 1;

                foreach (var item in this.ToolBarActions)
                {
                    if (item != null)
                    {
                        item.id = string.IsNullOrEmpty(item.id) ? tableId + HelperConstant.DataTable.TABLE_TOOLBAR + (indexCounter++).ToString() : item.id;

                        var link = item.CreateLink();
                       
                        if (item is ModalActionButton)
                        {
                            string modalId = item.id + HelperConstant.DataTable.LINK_MODAL_ID;
                            ModalActionButton modalItem = (ModalActionButton)item;
                            modalItem.modalui.id = modalId;
                            modal += modalItem.ModalDialog();
                        }

                        if (!string.IsNullOrEmpty(link))
                        {
                            if (item.formSide.Equals(EnumFormSide.LetfSide))
                            {
                                actionLeft = string.IsNullOrEmpty(actionLeft) ? link : actionLeft + " " + link;
                            }
                            else
                            {
                                var div = new TagBuilder("div");
                                div.AddCssClass("pull-right");
                                div.Attributes.Add("style", "margin-left:5px");
                                div.InnerHtml.Clear();
                                div.InnerHtml.Append(link);
                                actionRight = div.ConvertHtmlString();
                            }
                        }
                    }
                }
            }
            actionRightString = actionRight;
            actionLeftString = actionLeft;
            toolbarmodals = modal;
        }
        public string GetToolBarHtml(string tableId, out string toolbarmodal)
        {
            toolbarmodal = string.Empty;

            TagBuilder headerActions = new TagBuilder("div");

            headerActions.AddCssClass(HelperConstant.CssClassName.DATATABLE_TOOLBAR_CLASS);
            headerActions.Attributes.Add(HelperConstant.DataTable.DATATABLES_TABLE_ID, tableId);

            if ((this.ToolBarActions != null && this.ToolBarActions.Count > 0))
            {
                var divActionsRow = new TagBuilder("div");
                divActionsRow.AddCssClass("row");

                string actionRight = string.Empty;
                string actionLeft = string.Empty;

                var divActionsRightLayout = new TagBuilder("div");
                divActionsRightLayout.AddCssClass("col-md-6");

                var divActionsLeftLayout = new TagBuilder("div");
                divActionsLeftLayout.AddCssClass("col-md-6");

                GetToolBarActions(tableId, out actionLeft, out actionRight, out toolbarmodal);

                if (!string.IsNullOrEmpty(actionRight))
                {
                    divActionsRightLayout.InnerHtml.Append(actionRight);
                }

                if (!string.IsNullOrEmpty(actionLeft))
                {
                    divActionsLeftLayout.InnerHtml.Append(actionLeft);
                }


                if (this.exportSetting != null)
                {
                    if (this.exportSetting.formSide.Equals(EnumFormSide.LetfSide))
                    {
                        divActionsLeftLayout.InnerHtml.Append(" " + GetToolbarExportOptions(tableId, this.exportSetting));
                    }
                    else
                    {
                        divActionsRightLayout.InnerHtml.Append(" " + GetToolbarExportOptions(tableId, this.exportSetting));
                    }
                }


                divActionsRow.InnerHtml.Append(divActionsLeftLayout.ConvertHtmlString());

                divActionsRow.InnerHtml.Append(divActionsRightLayout.ConvertHtmlString());

                headerActions.InnerHtml.Append(divActionsRow.ConvertHtmlString());

                return headerActions.ConvertHtmlString();
            }

            return string.Empty;
        }
        public string GetToolbarExportOptions(string tableId, TableExportSetting exportSetting)
        {
            var exporttools = new TagBuilder("div");
            if (exportSetting.formSide.Equals(EnumFormSide.RightSide))
            {
                exporttools.AddCssClass("btn-group pull-right");
                exporttools.Attributes.Add("style", "margin-left:5px");
            }

            else
                exporttools.AddCssClass("btn-group");

            string exportButton = "<a class=\"{0}\" href=\"javascript:;\" data-toggle=\"dropdown\" aria-expanded=\"false\"><span class=\"hidden-xs\"> {1} </span><i class=\"fa fa-angle-down\"></i></a>";
            exportButton = string.Format(exportButton, exportSetting.cssClass, exportSetting.title);

            string exportTool = "<li><a href =\"javascript:;\" data-action =\"{0}\" class=\"tool-action\"><i class=\"{1}\"></i> {2}</a></li>";

            var toollist = new TagBuilder("ul");
            toollist.AddCssClass("dropdown-menu pull-right");
            toollist.GenerateId(tableId + HelperConstant.DataTable.TABLE_TOOLS, "");

            int indexCounter = 0;
            if (exportSetting.isExportCSV)
            {
                var tool = string.Format(exportTool, (indexCounter++).ToString(), "icon-cloud-upload", "Export CSV");
                toollist.InnerHtml.Append(tool);
            }

            if (exportSetting.isExportExcel)
            {
                var tool = string.Format(exportTool, (indexCounter++).ToString(), "icon-paper-clip", "Export Excel");
                toollist.InnerHtml.Append(tool);
            }

            if (exportSetting.isExportPdf)
            {
                var tool = string.Format(exportTool, (indexCounter++).ToString(), "icon-doc", "Export PDF");
                toollist.InnerHtml.Append(tool);
            }

            if (exportSetting.isPrintable)
            {
                var tool = string.Format(exportTool, (indexCounter++).ToString(), "icon-printer", "Print");
                toollist.InnerHtml.Append(tool);
            }

            exporttools.InnerHtml.Append(exportButton);

            exporttools.InnerHtml.Append(toollist.ConvertHtmlString());

            return exporttools.ConvertHtmlString();
        }
    }
}
