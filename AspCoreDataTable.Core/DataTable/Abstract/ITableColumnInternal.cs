namespace AspCoreDataTable.Core.DataTable.Abstract
{
    public interface ITableColumnInternal
    {
        string tableid { get; set; }
        string columnTitle { get; set; }
        int width { get; set; }
        bool visible { get; set; }
        string HtmlColumn();
    }
}
