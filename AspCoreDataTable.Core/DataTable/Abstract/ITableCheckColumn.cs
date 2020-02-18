namespace AspCoreDataTable.Core.DataTable.Abstract
{
    public interface ITableCheckColumn : ITableColumn<ITableCheckColumn>
    {
        ITableCheckColumn CheckAllEnabled();
    }
}
