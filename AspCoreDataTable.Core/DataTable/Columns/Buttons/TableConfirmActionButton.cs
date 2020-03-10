using AspCoreDataTable.Core.Button.Abstract;
using AspCoreDataTable.Core.Button.Concrete;
using AspCoreDataTable.Core.DataTable.Abstract;
using AspCoreDataTable.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AspCoreDataTable.Core.DataTable.Columns.Buttons
{
    public class TableConfirmActionButton<TModel> : ConfirmActionButton, ITableActionButton<IConfirmActionButton, TModel>
            where TModel : class
    {
        public TableConfirmActionButton() : base(string.Empty)
        {

        }

        public IConfirmActionButton Visible(bool visible)
        {
            return _instance;
        }

        public IConfirmActionButton Hidden<TProperty>(Expression<Func<TModel, TProperty>> expression, object value)
        {
            this.condition = expression.ToCondition(value);
            return _instance;
        }
    }
}
