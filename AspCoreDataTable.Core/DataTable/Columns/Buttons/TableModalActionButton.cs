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
    public class TableModalActionButton<TModel> : ModalActionButton, ITableActionButton<IModalActionButton, TModel>
        where TModel : class
    {
        public TableModalActionButton() : base(string.Empty)
        {

        }

        public IModalActionButton Visible(bool visible)
        {
            return _instance;
        }

        public IModalActionButton Hidden<TProperty>(Expression<Func<TModel, TProperty>> expression, object value)
        {
            this.condition = expression.ToCondition(value);
            return _instance;
        }
    }
}
