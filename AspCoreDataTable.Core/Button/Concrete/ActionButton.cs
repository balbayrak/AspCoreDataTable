using AspCoreDataTable.Core.Block;
using AspCoreDataTable.Core.Button.Abstract;
using AspCoreDataTable.Core.DataTable.Columns.Buttons;
using AspCoreDataTable.Core.Extensions;
using AspCoreDataTable.Core.General;
using AspCoreDataTable.Core.General.Enums;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.IO;
using System.Text.Encodings.Web;

namespace AspCoreDataTable.Core.Button.Concrete
{
    public abstract class ActionButton<T> : IHtmlContent, IActionButtonInternal
        where T : IActionButton<T>
    {
        protected abstract T _instance { get; }
        public ActionInfo action { get; set; }
        public BlockInfo block { get; set; }
        public string cssClass { get; set; }
        public string iclass { get; set; }
        public string id { get; set; }
        public string text { get; set; }
        public Condition condition { get; set; }

        public T ActionInfo(ActionInfo action)
        {
            this.action = action;
            return _instance;
        }
        public T BlockUI(string blocktarget)
        {
            this.block = this.block ?? new BlockInfo();
            this.block.isEnabled = true;
            this.block.blockTarget = blocktarget;
            return _instance;
        }
        public T CssClass(string cssClass)
        {
            this.cssClass = cssClass;
            return _instance;
        }
        public T IClass(string iclass)
        {
            this.iclass = iclass;
            return _instance;
        }
        public T Text(string text)
        {
            this.text = text;
            return _instance;
        }
        public ActionButton(string id)
        {
            this.id = id;
            this.block = new BlockInfo { blockTarget = null, isEnabled = false };
            this.condition = null;
        }
        public ActionButton(string id, string text, string iClass, string cssClass, bool blockui, string blockTarget, string actionUrl, EnumHttpMethod httpMethod = EnumHttpMethod.GET) : this(id)
        {
            this.text = text;
            this.iclass = iClass;
            this.cssClass = cssClass;
            this.condition = null;

            if (blockui)
            {
                this.block.blockTarget = blockTarget;
                this.block.isEnabled = true;
            }

            if (!string.IsNullOrEmpty(actionUrl))
            {
                this.action = new ActionInfo();
                this.action.actionUrl = actionUrl;
                this.action.methodType = httpMethod;
            }
        }
        protected TagBuilder CreateTagBuilder(string tagName)
        {
            var link = new TagBuilder(tagName);
            link.GenerateId(this.id, "");

            if (!string.IsNullOrEmpty(cssClass))
            {
                link.Attributes.Add(HelperConstant.CssClassName.CSS_CLASS, cssClass);
            }


            if (this.block.isEnabled)
            {
                if (!cssClass.Contains(HelperConstant.CssClassName.BLOCK_UI_CLASS))
                {
                    cssClass += " " + HelperConstant.CssClassName.BLOCK_UI_CLASS;

                    if (link.Attributes.ContainsKey(HelperConstant.CssClassName.CSS_CLASS))
                        link.Attributes.Remove(HelperConstant.CssClassName.CSS_CLASS);

                    link.Attributes.Add(HelperConstant.CssClassName.CSS_CLASS, cssClass);
                }
                if (this.block.blockTarget != null)
                    link.Attributes.Add(HelperConstant.General.DATA_BLOCKUI_TARGET, (this.block.blockTarget.StartsWith("#") ? this.block.blockTarget : "#" + this.block.blockTarget));
                link.Attributes.Add(HelperConstant.General.DATA_BLOCKUI, true.ToString());
            }
            else
            {
                link.Attributes.Add(HelperConstant.General.DATA_BLOCKUI, false.ToString());
            }

            if (!string.IsNullOrEmpty(this.action.actionUrl))
            {
                link.Attributes.Add(HelperConstant.General.DATA_TARGET_URL, this.action.actionUrl);
                link.Attributes.Add(HelperConstant.General.DATA_EVENT_HTTPMETHOD, this.action.methodType.ToString());
            }

            var iClassTag = new TagBuilder("i");
            iClassTag.AddCssClass(this.iclass);
            if (string.IsNullOrEmpty(this.iclass))
            {
                link.InnerHtml.Append(this.text);
            }
            else
            {
                link.InnerHtml.Append(iClassTag.ConvertHtmlString() + " " + this.text);
            }

            return link;
        }
        public abstract TagBuilder BuildActionButton();
        public string CreateLink()
        {
            TagBuilder link = BuildActionButton();

            var linkStr= link.ConvertHtmlString();

            return linkStr;

        }
        public virtual IHtmlContent ToHtml()
        {
            return new HtmlString(string.Empty);
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write(this.ToHtml());

        }
    }
}