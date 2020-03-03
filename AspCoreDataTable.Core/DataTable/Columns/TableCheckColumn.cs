using AspCoreDataTable.Core.DataTable.Abstract;
using AspCoreDataTable.Core.Extensions;
using AspCoreDataTable.Core.General;
using AspCoreDataTable.Core.General.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace AspCoreDataTable.Core.DataTable.Columns
{
    public class TableCheckColumn : TableColumn<ITableCheckColumn>, ITableCheckColumn, ITableCheckColumnInternal
    {
        public TableCheckColumn(string propertyName) : base()
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
