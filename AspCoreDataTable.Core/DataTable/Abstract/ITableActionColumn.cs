using AspCoreDataTable.Core.Button.Concrete;
using System;

namespace AspCoreDataTable.Core.DataTable.Abstract
{
    public interface ITableActionColumn : ITableColumn<ITableActionColumn>
    {
        ITableActionColumn Actions(Action<ActionBuilder> actionBuilder);
    }
}
