namespace AspCoreDataTable.Core.Button.Abstract
{
    public interface IGrupActionButtonInternal : IActionButtonInternal
    {
        void AddAction(IActionButtonInternal button);
    }
}
