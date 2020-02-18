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
    public abstract class TableColumn<TInterface> : ITableColumnInternal
        where TInterface : ITableColumn<TInterface>
    {
        protected abstract TInterface _instance { get; }

        public bool columnIsActionColumn { get; set; }

        public string columnTitle { get; set; }

        public int width { get; set; }

        public string tableid { get; set; }

        public string HtmlColumn()
        {
            TagBuilder column = new TagBuilder("th");

            if (this.width > 0)
            {
                column.Attributes.Add(HelperConstant.General.DATA_WIDTH, this.width.ToString() + "%");
            }
            column = SubHtmlColumn(column);

            return column.ConvertHtmlString();
        }

        public virtual TagBuilder SubHtmlColumn(TagBuilder column)
        {
            return column;
        }

        public TInterface Title(string title)
        {
            this.columnTitle = title;
            return _instance;
        }

        public TInterface Width(int width)
        {
            this.width = width;
            return _instance;
        }
    }

    public class TableActionColumn : TableColumn<ITableActionColumn>, ITableActionColumn, ITableActionColumnInternal
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

        protected override ITableActionColumn _instance
        {
            get
            {
                return this;
            }
        }

        public TableActionColumn()
        {
            this.columnIsActionColumn = true;
            this.width = 0;
        }

        public ITableActionColumn Actions(Action<ActionBuilder> actionBuilder)
        {
            if (actionBuilder != null)
            {
                ActionBuilder builder = new ActionBuilder(this);
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
                        columnActionsHtml += actionItem.CreateLink();

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

    public class TableBoundColumn<TModel, TProperty> : TableColumn<ITableBoundColumn>, ITableBoundColumn, ITableBoundColumnInternal<TModel>
        where TModel : class
    {
        protected override ITableBoundColumn _instance
        {
            get
            {
                return this;
            }
        }

        public Func<TModel, TProperty> CompiledExpression
        {
            get;
            set;
        }

        public string orderByDirection { get; set; }

        public bool columnIsPrimaryKey
        {
            get; set;
        }

        public string columnProperty
        {
            get; set;
        }

        public string columnPropertyExp
        {
            get; set;
        }

        public string Evaluate(TModel model)
        {
            var result = this.CompiledExpression(model);
            return result == null ? string.Empty : result.ToString();
        }

        public string searchable { get; set; }


        public TableBoundColumn(Expression<Func<TModel, TProperty>> expression, int columnCount)
        {
            string memberStr = (expression.Body as MemberExpression).ToString();
            this.columnPropertyExp = memberStr;
            this.columnProperty = (expression.Body as MemberExpression).Member.Name + columnCount.ToString();
            this.columnTitle = Regex.Replace(this.columnProperty, "([a-z])([A-Z])", "$1 $2");
            this.CompiledExpression = expression.Compile();
            this.columnIsActionColumn = false;
            this.orderByDirection = string.Empty;
            this.width = 0;
            this.searchable = null;
        }

        public ITableBoundColumn IsPrimaryKey(bool value)
        {
            this.columnIsPrimaryKey = value;
            return _instance;
        }

        public ITableBoundColumn OrderBy(EnumSortingDirection direction)
        {
            this.orderByDirection = direction == EnumSortingDirection.Ascending ? "asc" : "desc";
            return _instance;
        }

        public override TagBuilder SubHtmlColumn(TagBuilder column)
        {
            column.InnerHtml.Append(this.columnTitle);
            column.Attributes.Add(HelperConstant.DataTable.DATA_PROPERTY, this.columnProperty);

            if (!string.IsNullOrEmpty(this.orderByDirection))
            {
                column.Attributes.Add(HelperConstant.DataTable.DATA_ORDERBY, this.orderByDirection);
            }
            else
            {
                column.Attributes.Add(HelperConstant.DataTable.DATA_ORDERBY, "#");
            }

            return column;
        }

        public ITableBoundColumn Searchable(Operation operation)
        {
            this.searchable = operation.GetHashCode().ToString();
            return _instance;

        }
    }

    public class TableCheckColumn : TableColumn<ITableCheckColumn>, ITableCheckColumn, ITableCheckColumnInternal
    {
        public TableCheckColumn(string propertyName)
        {
            this.columnTitle = string.Empty;
            this.propertyName = propertyName;
        }
        protected override ITableCheckColumn _instance
        {
            get
            {
                return this;
            }
        }

        public string propertyName { get; set; }
        public string checkActionHtml { get; private set; }

        public bool checkAllEnabled { get; set; }

        public string columnDataProperty
        {
            get
            {
                return HelperConstant.DataTable.CHK_ACTIONCOLUMN_PROPERTY + "_" + this.actionColumnIndex.ToString();
            }
        }

        public int actionColumnIndex { get; set; }

        public ITableCheckColumn CheckAllEnabled()
        {
            this.checkAllEnabled = true;
            return _instance;
        }

        public override TagBuilder SubHtmlColumn(TagBuilder column)
        {
            column.Attributes.Add(HelperConstant.DataTable.DATA_PROPERTY, columnDataProperty);
            column.Attributes.Add(HelperConstant.DataTable.DATA_ORDERBY, "#");
            column.AddCssClass(HelperConstant.DataTable.NOEXPORT_CSS);
            if (this.checkAllEnabled) column.InnerHtml.Append(GetCheckElement(false));
            checkActionHtml = GetCheckElement(true);
            return column;
        }

        private string GetCheckElement(bool isActionElement)
        {
            var label = new TagBuilder("label");
            label.AddCssClass("mt-checkbox mt-checkbox-single mt-checkbox-outline");
            //if (!isActionElement)
            //{
            //    label.Attributes.Add("style", "padding-left:10px; float:left");
            //}

            var input = new TagBuilder("input");
            input.Attributes.Add("type", "checkbox");
            if (!isActionElement)
            {
                input.Attributes.Add("name", "selectAll");
                input.AddCssClass("group-checkable");
            }
            else
            {
                input.Attributes.Add("name", "checkboxRow" + this.propertyName);
                input.AddCssClass("checkboxes");
                input.Attributes.Add("value", "{0}");
            }

            TagBuilder span = new TagBuilder("span");

            label.InnerHtml.Append(input.ConvertHtmlString() + span.ConvertHtmlString());

            return label.ConvertHtmlString();
        }
    }
}