namespace AspCoreDataTable.Core.ConfirmBuilder
{
    public class DefaultConfirmManager : BaseConfirmManager, IConfirmService
    {
        public override ConfirmType baseConfirmType => ConfirmType.Default;
    }
}

