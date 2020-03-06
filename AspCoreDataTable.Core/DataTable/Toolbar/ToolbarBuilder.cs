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

        public IToolbarActionButton<IModalActionButton> ModalActionButton()
        {
            ToolBarModalActionButton act = new ToolBarModalActionButton(string.Empty);
            TableBuilder.AddToolBarAction(act);
            return act;
        }

        public IToolbarActionButton<IDefaultActionButton> ActionButton()
        {
            ToolBarDefaultActionButton act = new ToolBarDefaultActionButton(string.Empty);
            TableBuilder.AddToolBarAction(act);
            return act;
        }
    }
}