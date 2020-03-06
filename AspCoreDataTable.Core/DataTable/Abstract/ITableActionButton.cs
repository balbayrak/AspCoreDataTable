using AspCoreDataTable.Core.Button.Abstract;
using AspCoreDataTable.Core.DataTable.Columns.Buttons;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AspCoreDataTable.Core.DataTable.Abstract
{
    public interface ITableActionButton<T, TModel> 
        where T : IActionButton<T>
        where TModel : class
    {
        T Visible(bool visible);

        T Hidden<TProperty>(Expression<Func<TModel, TProperty>> expression, object value);
    }
}
