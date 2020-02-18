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


        public IConfirmService confirmService;
        public ConfirmOption()
        {
            this.confirmService = DependencyResolver.Current.GetService<IConfirmService>();
        }

        public ConfirmOption(string message, string title, string confirmCallBackFuncName = null) : this()
        {
            this.confirmMessage = message;
            this.confirmTitle = title;
            this.confirmCallBackFuncName = confirmCallBackFuncName;
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