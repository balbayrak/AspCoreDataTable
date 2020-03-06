using AspCoreDataTable.Core.DataTable.Columns;
using AspCoreDataTable.Core.DataTable.Toolbar;
using AspCoreDataTable.Core.General.Enums;
using System;
using System.Drawing;
using System.Linq.Expressions;

namespace AspCoreDataTable.Core.DataTable.Abstract
{
    public interface ITableBuilder<TModel> : IFluentInterface where TModel : class
    {
        ITableBuilder<TModel> Columns(Action<ColumnBuilder<TModel>> columnBuilder);
        ITableBuilder<TModel> ToolBarActions(Action<ToolBarBuilder<TModel>> toolBarBuilder, TableExportSetting exportSetting);
        ITableBuilder<TModel> Portlet(string title, Color color, string iClass);
        ITableBuilder<TModel> PagingType(EnumPagingType pagingType);
        ITableBuilder<TModel> CssClass(string cssClass);
        ITableBuilder<TModel> Searching(bool searchable);
        ITableBuilder<TModel> StateSave(bool stateSave);
        ITableBuilder<TModel> RowsCssCondition<TProperty>(Expression<Func<TModel, TProperty>> expression, object value, string css);

    }
}
