using Microsoft.AspNetCore.Html;
using System;
using AspCoreDataTable.Core.Button.Abstract;
using AspCoreDataTable.Core.ConfirmBuilder;
using AspCoreDataTable.Core.General.Enums;

namespace AspCoreDataTable.Core.Button.Concrete
{
    public class ConfirmHtmlActionButton : ConfirmActionButton
    {
        public ConfirmHtmlActionButton(string id) : base(id)
        {

        }
        public ConfirmHtmlActionButton(string id, string text, string iClass, string cssClass, bool blockui, string blockTarget, string actionUrl, EnumHttpMethod httpMethod, string confirmTitle, string confirmMessage, string confirmCallbackFunc = null)
          : base(id, text, iClass, cssClass, blockui, blockTarget, actionUrl, httpMethod, confirmTitle, confirmMessage, confirmCallbackFunc)
        {
        }
        public override IHtmlContent ToHtml()
        {
            return new HtmlString(CreateLink());
        }

        public override IConfirmActionButton ConfirmOption(ConfirmOption confirmoption)
        {
            this.confirmOption = confirmoption;
            if (this.action != null)
            {
                this.confirmOption.confirmAction = new ActionInfo();
                if (!string.IsNullOrEmpty(this.action.actionUrl)) this.confirmOption.confirmAction.actionUrl = this.action.actionUrl;
            }

            this.confirmOption.confirmAction.methodType = action.methodType;
            return _instance;
        }
    }
}
