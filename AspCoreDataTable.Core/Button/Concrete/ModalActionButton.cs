using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using AspCoreDataTable.Core.Button.Abstract;
using AspCoreDataTable.Core.General;
using AspCoreDataTable.Core.General.Enums;
using AspCoreDataTable.Core.Modal;
using AspCoreDataTable.Core.Modal.Concrete;

namespace AspCoreDataTable.Core.Button.Concrete
{
    public class ModalActionButton : ActionButton<IModalActionButton>, IModalActionButton, IModalActionButtonInternal
    {
        protected override IModalActionButton _instance => this;

        public ModalUI modalui { get; set; }
        public bool backDropStatic { get; set; }

        public ModalActionButton(string id) : base(id)
        {
            backDropStatic = false;
        }

        public ModalActionButton(string id, string text, string iClass, string cssClass,bool blockui, string blockTarget, string actionUrl, EnumHttpMethod httpMethod, EnumModalSize modalSize, bool backDropStatic = false)
            : base(id, text, iClass, cssClass, blockui,blockTarget, actionUrl, httpMethod)
        {
            this.modalui = new ModalUI();
            this.modalui.id = this.id + HelperConstant.DataTable.LINK_MODAL_ID;
            this.modalui.modalSize = modalSize;
            this.backDropStatic = backDropStatic;
        }
      
        public IModalActionButton Modal(string modalid, EnumModalSize modalSize)
        {
            this.id = modalid;
            this.modalui = this.modalui ?? new ModalUI();
            this.modalui.modalSize = modalSize;
            return _instance;
        }

        public IModalActionButton Modal(EnumModalSize modalSize)
        {
            this.modalui = this.modalui ?? new ModalUI();
            this.modalui.modalSize = modalSize;
            return _instance;
        }

        public string ModalDialog()
        {
            return MvcModalExtensions.ModalHelper(this.modalui.id, this.modalui.modalSize, this.backDropStatic);
        }

        public override TagBuilder BuildActionButton()
        {
            TagBuilder tag = base.CreateTagBuilder("a");

            if (!this.cssClass.Contains(HelperConstant.CssClassName.BLOCK_UI_MODAL_CLASS))
            {
                if (cssClass.Contains(HelperConstant.CssClassName.BLOCK_UI_CLASS))
                {
                    cssClass = cssClass.Replace(HelperConstant.CssClassName.BLOCK_UI_CLASS, "");
                }
                cssClass += " " + HelperConstant.CssClassName.BLOCK_UI_MODAL_CLASS;

                tag.Attributes.Remove("class");
                tag.AddCssClass(cssClass);
            }

            if (this.modalui != null)
            {
                string targetId = this.id + HelperConstant.DataTable.LINK_MODAL_ID;
                string datatarget = "#" + targetId;
                string datatargetBody = "#" + targetId + HelperConstant.DataTable.BODY_ID;

                tag.Attributes.Add(HelperConstant.General.DATA_TARGET, datatarget);
                tag.Attributes.Add(HelperConstant.General.DATA_TARGET_BODY, datatargetBody);
                tag.Attributes.Add(HelperConstant.General.DATA_TOGGLE, HelperConstant.General.DATA_TOGGLE_MODAL);
            }

            return tag;
        }

        public override IHtmlContent ToHtml()
        {
            return base.ToHtml();
        }

        public IModalActionButton BackDropStatic()
        {
            backDropStatic = true;
            return _instance;
        }
    }
}
