using AspCoreDataTable.Core.Modal.Concrete;

namespace AspCoreDataTable.Core.Button.Abstract
{
    public interface IModalActionButtonInternal : IActionButtonInternal
    {
        ModalUI modalui { get; set; }
        string ModalDialog();
        bool backDropStatic { get; set; }
    }
}
