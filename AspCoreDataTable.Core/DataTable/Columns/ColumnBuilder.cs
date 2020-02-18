using AspCoreDataTable.Core.DataTable.Abstract;
using System;
using System.Linq.Expressions;

namespace AspCoreDataTable.Core.DataTable.Columns
{
    public class ColumnBuilder<TModel> where TModel : class
    {
        private TableBuilder<TModel> TableBuilder
        {
            get;
            set;
        }
        public ColumnBuilder(TableBuilder<TModel> tableBuilder)
        {
            TableBuilder = tableBuilder;
        }
        public ITableBoundColumn BoundColumn<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            TableBoundColumn<TModel, TProperty> column = new TableBoundColumn<TModel, TProperty>(expression,TableBuilder.GetColumnCount());
            TableBuilder.AddColumn(column);
            return column;
        }
        public ITableActionColumn ActionColumn()
        {
            TableActionColumn column = new TableActionColumn();
            TableBuilder.AddActionColumn(column);
            return column;
        }
        public ITableCheckColumn CheckColumn<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            string propertyName = (expression.Body as MemberExpression).Member.Name;

            TableCheckColumn column = new TableCheckColumn(propertyName);
            TableBuilder.AddCheckColumn(column);
            return column;
        }
    }
}