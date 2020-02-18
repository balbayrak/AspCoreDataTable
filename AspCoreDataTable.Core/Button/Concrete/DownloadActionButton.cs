using Microsoft.AspNetCore.Mvc.Rendering;
using AspCoreDataTable.Core.Button.Abstract;
using AspCoreDataTable.Core.General;
using AspCoreDataTable.Core.General.Enums;

namespace AspCoreDataTable.Core.Button.Concrete
{
    public class DownloadActionButton : ActionButton<IDefaultActionButton>, IDefaultActionButton
    {
        public DownloadActionButton(string id) : base(id)
        {

        }

        public DownloadActionButton(string id, string text, string iClass, string cssClass, bool blockui, string blockTarget, string actionUrl, EnumHttpMethod httpMethod = EnumHttpMethod.GET)
           : base(id, text, iClass, cssClass, blockui, blockTarget, actionUrl, httpMethod)
        {
        }

        protected override IDefaultActionButton _instance
        {
            get
            {
                return this;
            }
        }

        public override TagBuilder BuildActionButton()
        {
            TagBuilder link = base.CreateTagBuilder("a");

            if (!this.cssClass.Contains(HelperConstant.CssClassName.DOWNLOAD_LINK_CLASS))
            {
                if (cssClass.Contains(HelperConstant.CssClassName.BLOCK_UI_CLASS))
                {
                    cssClass = cssClass.Replace(HelperConstant.CssClassName.BLOCK_UI_CLASS, "");
                }
                cssClass += " " + HelperConstant.CssClassName.DOWNLOAD_LINK_CLASS;
                link.Attributes.Remove("class");
                link.AddCssClass(cssClass);
            }
            return link;
        }

    }
}
