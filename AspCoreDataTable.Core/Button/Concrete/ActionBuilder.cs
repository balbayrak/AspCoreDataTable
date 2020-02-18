using AspCoreDataTable.Core.Button.Abstract;
using AspCoreDataTable.Core.DataTable.Abstract;

namespace AspCoreDataTable.Core.Button.Concrete
{
    public class ActionBuilder
    {
        private ITableActionColumnInternal actionColumn { get; set; }

        public ActionBuilder(ITableActionColumnInternal actioncolumn)
        {
            this.actionColumn = actioncolumn;
        }

        public IModalActionButton ModalButton(string id)
        {
            ModalActionButton act = new ModalActionButton(id);
            actionColumn.AddAction(act);
            return act;
        }

        public IDefaultActionButton ActionButton(string id)
        {
            DefaultActionButton act = new DefaultActionButton(id);
            actionColumn.AddAction(act);
            return act;
        }

        public IConfirmActionButton ConfirmButton(string id)
        {
            ConfirmActionButton act = new ConfirmActionButton(id);
            actionColumn.AddAction(act);
            return act;
        }

        public IDefaultActionButton DownloadButton(string id)
        {
            DownloadActionButton act = new DownloadActionButton(id);
            actionColumn.AddAction(act);
            return act;
        }
    }
}
