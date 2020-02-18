using System.Web;

namespace AspCoreDataTable.Core.ConfirmBuilder
{
    public abstract class BaseConfirmManager
    {
        public abstract ConfirmType baseConfirmType { get; }
        public string GetConfirmString(ConfirmOption confirmOption)
        {
            string callbackFunc = confirmOption.confirmCallBackFuncName;

            string actionUrl = string.Empty;
            if (confirmOption.confirmAction != null && !string.IsNullOrEmpty(confirmOption.confirmAction.actionUrl))
            {
                actionUrl = HttpUtility.HtmlEncode(confirmOption.confirmAction.actionUrl);
            }
            return "Confirm.showConfirm('" + confirmOption.confirmTitle + "','" + confirmOption.confirmMessage + "','" + actionUrl + "','" + confirmOption.confirmAction.methodType.ToString().Trim() + "','" + callbackFunc + "','" + baseConfirmType.GetHashCode() + "');";
        }
    }
}
