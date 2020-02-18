using AspCoreDataTable.Core.ConfirmBuilder;

namespace AspCoreDataTable.Core.Button.Abstract
{
    public interface IConfirmActionButton : IActionButton<IConfirmActionButton>
    {
        IConfirmActionButton ConfirmOption(ConfirmOption confimoption);
    }
}
