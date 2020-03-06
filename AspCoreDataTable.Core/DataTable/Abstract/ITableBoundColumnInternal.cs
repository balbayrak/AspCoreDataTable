using AspCoreDataTable.Core.DataTable.Columns.Buttons;

namespace AspCoreDataTable.Core.DataTable.Abstract
{
    public interface ITableBoundColumnInternal<TModel> : ITableColumnInternal where TModel : class
    {
        bool columnIsPrimaryKey { get; set; }
        string columnProperty { get; set; }
        string columnPropertyExp { get; set; }
        string orderByDirection { get; set; }
        string searchable { get; set; }
    }
}
