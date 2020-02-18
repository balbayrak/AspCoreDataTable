using AspCoreDataTable.Core.DataTable.Columns;
using AspCoreDataTable.Core.DataTable.Toolbar;
using System;
using System.Drawing;

namespace AspCoreDataTable.Core.DataTable.Abstract
{
    public interface ITableBuilder<TModel> where TModel : class
    {
        TableBuilder<TModel> Columns(Action<ColumnBuilder<TModel>> columnBuilder);
        TableBuilder<TModel> ToolBarActions(Action<ToolBarBuilder<TModel>> toolBarBuilder, TableExportSetting exportSetting);
        TableBuilder<TModel> Portlet(string title, Color color, string iClass);
    }
}
