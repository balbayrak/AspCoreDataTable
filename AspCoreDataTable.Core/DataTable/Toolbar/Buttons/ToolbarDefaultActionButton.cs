using AspCoreDataTable.Core.Button.Abstract;
using AspCoreDataTable.Core.Button.Concrete;
using AspCoreDataTable.Core.DataTable.Abstract;
using AspCoreDataTable.Core.General.Enums;

namespace AspCoreDataTable.Core.DataTable.Toolbar.Buttons
{
    public class ToolBarDefaultActionButton : DefaultActionButton, ITableActionButton<IDefaultActionButton>, IToolbarModalActionButtonInternal
    {
        public EnumFormSide formSide { get; set; }

        public ToolBarDefaultActionButton(string id) : base(id)
        {

        }

        public IDefaultActionButton FormSide(EnumFormSide formSide)
        {
            this.formSide = formSide;
            return _instance;
        }
    }
}
