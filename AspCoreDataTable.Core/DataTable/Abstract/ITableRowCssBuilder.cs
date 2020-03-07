using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AspCoreDataTable.Core.DataTable.Abstract
{
    public interface ITableRowCssBuilder<TModel> : IFluentInterface
           where TModel : class
    {
        ITableRowCssBuilder<TModel> AddRowCss<TProperty>(Expression<Func<TModel, TProperty>> expression, object value, string css);
    }
}
