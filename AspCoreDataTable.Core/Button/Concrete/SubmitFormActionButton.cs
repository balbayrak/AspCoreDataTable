using Microsoft.AspNetCore.Mvc.Rendering;
using AspCoreDataTable.Core.Button.Abstract;
using AspCoreDataTable.Core.General;

namespace AspCoreDataTable.Core.Button.Concrete
{
    public class SubmitFormActionButton : ActionButton<ISubmitActionButton>, ISubmitActionButton, ISubmitActionButtonInternal
    {
        public string submitformid { get; set; }

        public bool closeParentModal { get; set; }

        protected override ISubmitActionButton _instance
        {
            get
            {
                return this;
            }
        }

        public SubmitFormActionButton(string id) : base(id)
        {

        }

        public ISubmitActionButton SubmitForm(string formid)
        {
            this.submitformid = formid;
            return this;
        }

        public ISubmitActionButton CloseParentModal(bool closeParentModal)
        {
            this.closeParentModal = closeParentModal;
            return this;
        }

        public override TagBuilder BuildActionButton()
        {
            TagBuilder link = base.CreateTagBuilder("a");

            if (!this.cssClass.Contains(HelperConstant.CssClassName.SUBMIT_LINK_CLASS))
            {
                if (cssClass.Contains(HelperConstant.CssClassName.BLOCK_UI_CLASS))
                {
                    cssClass = cssClass.Replace(HelperConstant.CssClassName.BLOCK_UI_CLASS, "");
                }
                cssClass += " " + HelperConstant.CssClassName.SUBMIT_LINK_CLASS;
                link.Attributes.Remove("class");
                link.AddCssClass(cssClass);
            }


            if (!string.IsNullOrEmpty(this.submitformid))
            {
                this.submitformid = this.submitformid.StartsWith("#") ? this.submitformid : "#" + this.submitformid;

                link.Attributes.Add(HelperConstant.Button.DATA_SUBMIT_FORM_ID, submitformid);
                if (this.closeParentModal)
                    link.Attributes.Add(HelperConstant.Button.DATA_CLOSEPARENT_MODAL, true.ToString());
                else
                    link.Attributes.Add(HelperConstant.Button.DATA_CLOSEPARENT_MODAL, false.ToString());
            }

            return link;
        }
    }
}
