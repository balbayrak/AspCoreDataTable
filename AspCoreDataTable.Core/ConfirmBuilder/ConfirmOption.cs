using AspCoreDataTable.Core.Button.Concrete;
using AspCoreDependency.Core.Concrete;

namespace AspCoreDataTable.Core.ConfirmBuilder
{
    public class ConfirmOption
    {
        public string confirmMessage { get; set; }
        public string confirmTitle { get; set; }
        public string confirmOKButtonText { get; set; }
        public string confirmCancelButtonText { get; set; }
        public ActionInfo confirmAction { get; set; }
        public string confirmCallBackFuncName { get; set; }


        private IConfirmService confirmService;

        public ConfirmOption(string message, string title, ConfirmType confirmType, string confirmCallBackFuncName = null)
        {
            this.confirmMessage = message;
            this.confirmTitle = title;
            this.confirmCallBackFuncName = confirmCallBackFuncName;

            if (confirmType == ConfirmType.Alertify)
            {
                confirmService = new AlertifyConfirmManager();
            }
            else if (confirmType == ConfirmType.BootBox)
            {
                confirmService = new BootBoxConfirmManager();
            }
            else if (confirmType == ConfirmType.Default)
            {
                confirmService = new DefaultConfirmManager();
            }
            else
            {
                confirmService = new SweetConfirmManager();
            }
        }

        public string ConfirmString
        {
            get
            {
                return this.confirmService.GetConfirmString(this);
            }
        }
    }
}