using AspCoreDataTable.Core.DataTable.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AspCoreDataTable.Core.DataTable.Rows
{
    public class RowCssBuilder<TModel> : ITableRowCssBuilder<TModel>
        where TModel : class
    {
        private TableBuilder<TModel> TableBuilder { get; set; }
        public RowCssBuilder(TableBuilder<TModel> tableBuilder)
        {
            TableBuilder = tableBuilder;
        }

        public ITableRowCssBuilder<TModel> AddRowCss<TProperty>(Expression<Func<TModel, TProperty>> expression, object value, string css)
        {
            TableBuilder.AddRowCss(expression, value, css);
            return this;
        }

    }
}
