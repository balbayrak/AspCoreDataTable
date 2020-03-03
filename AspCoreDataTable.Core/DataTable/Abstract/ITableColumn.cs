namespace AspCoreDataTable.Core.DataTable.Abstract
{
    public interface ITableColumn<T> where T : ITableColumn<T>
    {
        T Title(string title);
        T Width(int width);
        T Visible(bool visible);
    }

}
