using AspCoreDataTable.Core.General.Enums;

namespace AspCoreDataTable.Core.DataTable.Abstract
{
    public interface ITableBoundColumn : ITableColumn<ITableBoundColumn>
    {
        ITableBoundColumn IsPrimaryKey(bool value);

        ITableBoundColumn OrderBy(EnumSortingDirection direciton);

        ITableBoundColumn Searchable(Operation operation);
    }
}
