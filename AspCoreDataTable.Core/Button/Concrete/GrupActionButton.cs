using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using AspCoreDataTable.Core.Button.Abstract;
using AspCoreDataTable.Core.General;

namespace AspCoreDataTable.Core.Button.Concrete
{
    public class GrupActionButton : ActionButton<IGrupActionButton>, IGrupActionButton, IGrupActionButtonInternal
    {
        public GrupActionButton(string id) : base(id)
        {

        }

        public List<IActionButtonInternal> subActions { get; set; }

        public IGrupActionButton SubActions(Action<SubActionBuilder> actionBuilder)
        {
            if (actionBuilder != null)
            {
                SubActionBuilder builder = new SubActionBuilder(this);
                actionBuilder(builder);
            }
            return _instance;
        }

        protected override IGrupActionButton _instance => this;

        public void AddAction(IActionButtonInternal button)
        {
            subActions = subActions ?? new List<IActionButtonInternal>();
            subActions.Add(button);
        }

        public override TagBuilder BuildActionButton()
        {
            TagBuilder divBtnGroup = new TagBuilder("div");

            if (this.subActions != null && this.subActions.Count > 0)
            {
               
                divBtnGroup.AddCssClass("btn-group");

                var mainBtn = base.CreateTagBuilder("button");
                
                var dropdownBtn = new TagBuilder("button");
                dropdownBtn.Attributes.Add("type", "button");
                dropdownBtn.Attributes.Add("data-toggle", "dropdown");
                var css = this.cssClass;

                if (this.cssClass.Contains(HelperConstant.CssClassName.BLOCK_UI_CLASS))
                {
                    css = cssClass.Replace(HelperConstant.CssClassName.BLOCK_UI_CLASS, "");
                }

                if (this.cssClass.Contains(HelperConstant.CssClassName.SUBMIT_LINK_CLASS))
                {
                    css = cssClass.Replace(HelperConstant.CssClassName.SUBMIT_LINK_CLASS, "");
                }

                if (this.cssClass.Contains(HelperConstant.CssClassName.BLOCK_UI_MODAL_CLASS))
                {
                    css = cssClass.Replace(HelperConstant.CssClassName.BLOCK_UI_MODAL_CLASS, "");
                }

                if (this.cssClass.Contains(HelperConstant.CssClassName.DOWNLOAD_LINK_CLASS))
                {
                    css = cssClass.Replace(HelperConstant.CssClassName.DOWNLOAD_LINK_CLASS, "");
                }

                css += " " + "dropdown-toggle";
                dropdownBtn.AddCssClass(css);

                var iTag = new TagBuilder("i");
                iTag.AddCssClass("fa fa-angle-down");
                dropdownBtn.InnerHtml.Append(iTag.ToString());

                var ul = new TagBuilder("ul");
                ul.AddCssClass("dropdown-menu");
                ul.Attributes.Add("role", "menu");

                foreach (var item in this.subActions)
                {
                    var li = new TagBuilder("li");
                    li.InnerHtml.Append(item.CreateLink());
                    ul.InnerHtml.Append(li.ToString());
                }

                divBtnGroup.InnerHtml.Append(mainBtn.ToString() + dropdownBtn.ToString() + ul.ToString());

            }
            return divBtnGroup;
        }
    }
}
