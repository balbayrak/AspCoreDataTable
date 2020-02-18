namespace AspCoreDataTable.Core.Button.Abstract
{
    public interface ISubmitActionButtonInternal : IActionButtonInternal
    {
        string submitformid { get; set; }

        bool closeParentModal { get; set; }
    }
}
