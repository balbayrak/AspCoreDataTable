using AspCoreDataTable.Core.Button.Abstract;
using AspCoreDataTable.Core.DataTable.Abstract;
using AspCoreDataTable.Core.DataTable.Toolbar.Buttons;

namespace AspCoreDataTable.Core.DataTable.Toolbar
{
    public class ToolBarBuilder<TModel> where TModel : class
    {
        private TableBuilder<TModel> TableBuilder { get; set; }

        public ToolBarBuilder(TableBuilder<TModel> tableBuilder, TableExportSetting exportSetting)
        {
            TableBuilder = tableBuilder;
        }

        public ITableActionButton<IModalActionButton> ModalActionButton(string id)
        {
            ToolBarModalActionButton act = new ToolBarModalActionButton(id);
            TableBuilder.AddToolBarAction(act);
            return act;
        }

        public ITableActionButton<IDefaultActionButton> ActionButton(string id)
        {
            ToolBarDefaultActionButton act = new ToolBarDefaultActionButton(id);
            TableBuilder.AddToolBarAction(act);
            return act;
        }
    }
}