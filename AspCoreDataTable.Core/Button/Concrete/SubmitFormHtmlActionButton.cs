using Microsoft.AspNetCore.Html;
using System;
using AspCoreDataTable.Core.Button.Abstract;

namespace AspCoreDataTable.Core.Button.Concrete
{
    public class SubmitFormHtmlActionButton : SubmitFormActionButton
    {
        public SubmitFormHtmlActionButton(string id) : base(id)
        {

        }
        public override IHtmlContent ToHtml()
        {
            return new HtmlString(CreateLink());
        }
    }
}
