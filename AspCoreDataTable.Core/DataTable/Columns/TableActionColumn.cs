using AspCoreDataTable.Core.Button.Abstract;
using AspCoreDataTable.Core.Button.Concrete;
using AspCoreDataTable.Core.DataTable.Abstract;
using AspCoreDataTable.Core.Extensions;
using AspCoreDataTable.Core.General;
using AspCoreDataTable.Core.General.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace AspCoreDataTable.Core.DataTable.Columns
{
    public class TableActionColumn<TModel> : TableColumn<ITableActionColumn<TModel>>, ITableActionColumn<TModel>, ITableActionColumnInternal
        where TModel : class
    {
        public List<IActionButtonInternal> actions { get; set; }

        public string columnActionsModalHtml
        {
            get; private set;
        }

        public string columnActionsHtml
        {
            get; private set;
        }

        public int actionColumnIndex { get; set; }

        public string columnDataProperty
        {
            get
            {
                return string.IsNullOrEmpty(this.columnTitle) ? HelperConstant.DataTable.ACTIONCOLUMN_PROPERTY : this.columnTitle + "_" + this.actionColumnIndex.ToString();
            }
        }

        protected override ITableActionColumn<TModel> _instance
        {
            get
            {
                return this;
            }
        }

        public TableActionColumn()
        {
            this.columnIsActionColumn = true;
        }

        public ITableActionColumn<TModel> Actions(Action<ActionBuilder<TModel>> actionBuilder)
        {
            if (actionBuilder != null)
            {
                ActionBuilder<TModel> builder = new ActionBuilder<TModel>(this);
                actionBuilder(builder);
            }
            return _instance;
        }

        public void AddAction(IActionButtonInternal button)
        {
            actions = actions ?? new List<IActionButtonInternal>();
            actions.Add(button);
        }

        private void ActionsHtml()
        {
            int indexCounterAction = 1;

            if (this.actions != null && this.actions.Count > 0)
            {
                foreach (var actionItem in this.actions)
                {
                    if (actionItem != null)
                    {
                        actionItem.id = string.IsNullOrEmpty(actionItem.id) ? this.tableid + "_" + (indexCounterAction++).ToString() : actionItem.id;

                        string actionurl = string.Empty;

                        if (actionItem.action != null && !string.IsNullOrEmpty(actionItem.action.actionUrl))
                        {
                            actionurl = actionItem.action.actionUrl.EndsWith("/") ? actionItem.action.actionUrl + "{0}" : actionItem.action.actionUrl + "/" + "{0}";
                            actionurl = actionurl.StartsWith("/") ? actionurl : "/" + actionurl;
                            actionItem.action.actionUrl = actionurl;
                        }
                       
                        columnActionsHtml += actionItem.CreateLink(); ;
                       

                        if (actionItem is ModalActionButton)
                        {
                            string modalId = actionItem.id + HelperConstant.DataTable.LINK_MODAL_ID;
                            ModalActionButton modalItem = (ModalActionButton)actionItem;
                            modalItem.modalui.id = modalId;
                            columnActionsModalHtml += modalItem.ModalDialog();
                        }
                    }
                }
            }
        }

        public override TagBuilder SubHtmlColumn(TagBuilder column)
        {
            column.InnerHtml.Append(this.columnTitle);
            column.Attributes.Add(HelperConstant.DataTable.DATA_PROPERTY, columnDataProperty);
            column.Attributes.Add(HelperConstant.DataTable.DATA_ORDERBY, "#");
            column.AddCssClass(HelperConstant.DataTable.NOEXPORT_CSS);
            ActionsHtml();
            return column;
        }
    }
}
