using AspCoreDataTable.Core.General.Enums;
using System;
using System.Linq.Expressions;

namespace AspCoreDataTable.Core.DataTable.Abstract
{
    public interface ITableBoundColumn<TModel,TProperty> : ITableColumn<ITableBoundColumn<TModel, TProperty>>
    {
        ITableBoundColumn<TModel, TProperty> IsPrimaryKey(bool value);

        ITableBoundColumn<TModel, TProperty> OrderBy(EnumSortingDirection direciton);

        ITableBoundColumn<TModel, TProperty> Searchable(Operation operation);


    }
}
