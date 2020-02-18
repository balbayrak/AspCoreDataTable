namespace AspCoreDataTable.Core.ConfirmBuilder
{
    public class AlertifyConfirmManager : BaseConfirmManager, IConfirmService
    {
        public override ConfirmType baseConfirmType => ConfirmType.Alertify;
    }
}
