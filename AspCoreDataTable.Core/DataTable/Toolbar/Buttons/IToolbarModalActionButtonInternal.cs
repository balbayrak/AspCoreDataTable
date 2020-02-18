using AspCoreDataTable.Core.Button.Abstract;
using AspCoreDataTable.Core.General.Enums;

namespace AspCoreDataTable.Core.DataTable.Toolbar.Buttons
{
    public interface IToolbarModalActionButtonInternal : IActionButtonInternal
    {
        EnumFormSide formSide { get; set; }
    }
}
