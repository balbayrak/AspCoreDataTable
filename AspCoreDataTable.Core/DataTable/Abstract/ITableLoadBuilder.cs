using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreDataTable.Core.DataTable.Abstract
{
    public interface ITableLoadBuilder<TModel> : IFluentInterface where TModel : class
    {
        ITableBuilder<TModel> LoadAction(string loadAction);
    }
}
