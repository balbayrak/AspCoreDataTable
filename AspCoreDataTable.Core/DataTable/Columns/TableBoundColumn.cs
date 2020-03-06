using AspCoreDataTable.Core.DataTable.Abstract;
using AspCoreDataTable.Core.DataTable.Columns.Buttons;
using AspCoreDataTable.Core.General;
using AspCoreDataTable.Core.General.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace AspCoreDataTable.Core.DataTable.Columns
{
    public class TableBoundColumn<TModel, TProperty> : TableColumn<ITableBoundColumn<TModel, TProperty>>, ITableBoundColumn<TModel, TProperty>, ITableBoundColumnInternal<TModel>
        where TModel : class
    {
        protected override ITableBoundColumn<TModel, TProperty> _instance
        {
            get
            {
                return this;
            }
        }

        public string orderByDirection { get; set; }

        public bool columnIsPrimaryKey { get; set; }

        public string columnProperty { get; set; }

        public string columnPropertyExp { get; set; }

        public string searchable { get; set; }

        public Condition condition { get; set; }

        public string css { get; set; }


        public TableBoundColumn(Expression<Func<TModel, TProperty>> expression, int columnCount) : base()
        {
            string memberStr = (expression.Body as MemberExpression).ToString();
            this.columnPropertyExp = memberStr;
            this.columnProperty = (expression.Body as MemberExpression).Member.Name + columnCount.ToString();
            this.columnTitle = Regex.Replace(this.columnProperty, "([a-z])([A-Z])", "$1 $2");
            this.orderByDirection = string.Empty;
            this.searchable = null;
            this.condition = null;
        }

        public ITableBoundColumn<TModel, TProperty> IsPrimaryKey(bool value)
        {
            this.columnIsPrimaryKey = value;
            return _instance;
        }

        public ITableBoundColumn<TModel, TProperty> OrderBy(EnumSortingDirection direction)
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

        public ITableBoundColumn<TModel, TProperty> Searchable(Operation operation)
        {
            this.searchable = operation.GetHashCode().ToString();
            return _instance;

        }

    }
}
