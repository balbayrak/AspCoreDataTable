using Microsoft.AspNetCore.Html;
using System;
using AspCoreDataTable.Core.Button.Abstract;
using AspCoreDataTable.Core.General.Enums;

namespace AspCoreDataTable.Core.Button.Concrete
{
    public class DefaultHtmlActionButton : DefaultActionButton
    {
        public DefaultHtmlActionButton(string id) : base(id)
        {

        }
        public DefaultHtmlActionButton(string id, string text, string iClass, string cssClass, bool blockui, string blockTarget, string actionUrl, EnumHttpMethod httpMethod = EnumHttpMethod.GET)
           : base(id, text, iClass, cssClass, blockui, blockTarget, actionUrl, httpMethod)
        {
        }
        public override IHtmlContent ToHtml()
        {
            return new HtmlString(CreateLink());
        }
    }
}
