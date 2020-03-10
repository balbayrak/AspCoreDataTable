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
    public class TableDownloadActionButton<TModel> : DownloadActionButton, ITableActionButton<IDefaultActionButton, TModel>
            where TModel : class
    {
        public TableDownloadActionButton() : base(string.Empty)
        {

        }

        public IDefaultActionButton Visible(bool visible)
        {
            throw new NotImplementedException();
        }

        public IDefaultActionButton Hidden<TProperty>(Expression<Func<TModel, TProperty>> expression, object value)
        {
            this.condition = expression.ToCondition(value);
            return _instance;
        }
    }
}
