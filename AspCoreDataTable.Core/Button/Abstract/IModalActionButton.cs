using AspCoreDataTable.Core.General.Enums;

namespace AspCoreDataTable.Core.Button.Abstract
{
    public interface IModalActionButton : IActionButton<IModalActionButton>
    {
        IModalActionButton Modal(string modalid, EnumModalSize modalSize);
        IModalActionButton Modal(EnumModalSize modalSize);
        IModalActionButton BackDropStatic();
    }
}
