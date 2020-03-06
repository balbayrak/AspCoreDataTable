using AspCoreDataTable.Core.Button.Concrete;
using System;

namespace AspCoreDataTable.Core.DataTable.Abstract
{
    public interface ITableActionColumn<TModel> : ITableColumn<ITableActionColumn<TModel>>
        where TModel : class
    {
        ITableActionColumn<TModel> Actions(Action<ActionBuilder<TModel>> actionBuilder);
    }
}
