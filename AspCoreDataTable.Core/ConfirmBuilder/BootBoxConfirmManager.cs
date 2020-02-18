namespace AspCoreDataTable.Core.ConfirmBuilder
{
    public class BootBoxConfirmManager : BaseConfirmManager, IConfirmService
    {
        public override ConfirmType baseConfirmType => ConfirmType.BootBox;
    }
}
