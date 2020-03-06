using AspCoreDataTable.Core.Button.Abstract;
using AspCoreDataTable.Core.DataTable.Abstract;
using AspCoreDataTable.Core.DataTable.Columns;
using AspCoreDataTable.Core.DataTable.Columns.Buttons;

namespace AspCoreDataTable.Core.Button.Concrete
{
    public class ActionBuilder<TModel>
        where TModel : class
    {
        private ITableActionColumnInternal actionColumn { get; set; }

        public ActionBuilder(ITableActionColumnInternal actioncolumn)
        {
            this.actionColumn = actioncolumn;
        }

        public ITableActionButton<IModalActionButton, TModel> ModalButton()
        {
            TableModalActionButton<TModel> act = new TableModalActionButton<TModel>();
            actionColumn.AddAction(act);
            return act;
        }

        public ITableActionButton<IDefaultActionButton, TModel> ActionButton()
        {
            TableDefaultActionButton<TModel> act = new TableDefaultActionButton<TModel>();
            actionColumn.AddAction(act);
            return act;
        }

        public ITableActionButton<IConfirmActionButton, TModel> ConfirmButton()
        {
            TableConfirmActionButton<TModel> act = new TableConfirmActionButton<TModel>();
            actionColumn.AddAction(act);
            return act;
        }

        public ITableActionButton<IDefaultActionButton, TModel> DownloadButton()
        {
            TableDownloadActionButton<TModel> act = new TableDownloadActionButton<TModel>();
            actionColumn.AddAction(act);
            return act;
        }
    }
}
