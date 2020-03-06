using AspCoreDataTable.Core.Button.Abstract;
using AspCoreDataTable.Core.Button.Concrete;
using AspCoreDataTable.Core.DataTable.Abstract;
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
            this.condition = new Condition();
            string memberStr = (expression.Body as MemberExpression).ToString();
            this.condition.property = memberStr;
            this.condition.value = value;
            return _instance;
        }
    }
}
