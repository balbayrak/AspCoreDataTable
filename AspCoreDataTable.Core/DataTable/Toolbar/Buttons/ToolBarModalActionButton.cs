using AspCoreDataTable.Core.Button.Abstract;
using AspCoreDataTable.Core.Button.Concrete;
using AspCoreDataTable.Core.DataTable.Abstract;
using AspCoreDataTable.Core.General.Enums;

namespace AspCoreDataTable.Core.DataTable.Toolbar.Buttons
{
    public class ToolBarModalActionButton : ModalActionButton, ITableActionButton<IModalActionButton>, IToolbarModalActionButtonInternal
    {
        public EnumFormSide formSide { get; set; }

        public ToolBarModalActionButton(string id) : base(id)
        {

        }

        public IModalActionButton FormSide(EnumFormSide formSide)
        {
            this.formSide = formSide;
            return _instance;
        }
    }
}
