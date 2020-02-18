using AspCoreDataTable.Core.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;

namespace AspCoreDataTable.Core.General.Portlet
{
    public class PortletForm
    {
        public string title { get; set; }

        public Color color { get; set; }

        public string iClass { get; set; }

        public bool adjustFormWidth { get; set; }

        public PortletForm() { }

        public IHtmlContent ToHtml(string portletBodyInnerHtml)
        {
            var portletDiv = new TagBuilder("div");
            if (adjustFormWidth)
                portletDiv.Attributes.Add("style", "width:auto;  display:inline-block;");

            portletDiv.AddCssClass("portlet box " + this.color.Name.ToLower());

            var portletCaption = new TagBuilder("div");
            portletCaption.AddCssClass("portlet-title");

            var caption = new TagBuilder("div");
            caption.AddCssClass("caption");

            var iClassTag = new TagBuilder("i");
            iClassTag.AddCssClass(this.iClass);

            caption.InnerHtml.Append(iClassTag.ConvertHtmlString() + this.title);

            portletCaption.InnerHtml.Append(caption.ConvertHtmlString());

            var portletBody = new TagBuilder("div");
            portletBody.AddCssClass("portlet-body");

            portletBody.InnerHtml.Append(portletBodyInnerHtml);

            string portletCaptionStr = portletCaption.ConvertHtmlString();

            string portletBodyStr = portletBody.ConvertHtmlString();

            portletDiv.InnerHtml.Append(portletCaptionStr + portletBodyStr);

            return new HtmlString(portletDiv.ConvertHtmlString());

        }
    }
}
