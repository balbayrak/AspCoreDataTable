using AspCoreDataTable.Core.DataTable.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspCoreDataTable.Core.DataTable
{
    public static class MvcHtmlTableExtensions
    {
        public static ITableBuilder<TModel> DataTableHelper<TModel>(this IHtmlHelper helper) where TModel : class
        {
            return new TableBuilder<TModel>();
        }
    }
}