using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using AspCoreDataTable.Core.Button.Abstract;
using AspCoreDataTable.Core.ConfirmBuilder;
using AspCoreDataTable.Core.General;
using AspCoreDataTable.Core.General.Enums;

namespace AspCoreDataTable.Core.Button.Concrete
{
    public class ConfirmActionButton : ActionButton<IConfirmActionButton>, IConfirmActionButton
    {
        public ConfirmActionButton(string id) : base(id)
        {

        }

        public ConfirmActionButton(string id, string text, string iClass, string cssClass, bool blockui, string blockTarget, string actionUrl, EnumHttpMethod httpMethod, string confirmTitle, string confirmMessage, string confirmCallbackFunc = null)
           : base(id, text, iClass, cssClass, blockui, blockTarget, actionUrl, httpMethod)
        {
            if (!string.IsNullOrEmpty(confirmMessage) || !string.IsNullOrEmpty(confirmTitle))
            {
                this.confirmOption = new ConfirmOption(confirmTitle, confirmMessage, ConfirmType.Default, confirmCallbackFunc);

                if (this.action != null)
                {
                    this.confirmOption.confirmAction = new ActionInfo();
                    if (!string.IsNullOrEmpty(this.action.actionUrl)) this.confirmOption.confirmAction.actionUrl = this.action.actionUrl + "/" + "{0}";
                }

                this.confirmOption.confirmAction.methodType = action.methodType;
            }
        }

        public ConfirmOption confirmOption { get; set; }

        protected override IConfirmActionButton _instance => this;

        public override TagBuilder BuildActionButton()
        {
            TagBuilder tag = base.CreateTagBuilder("a");

            if (this.confirmOption != null)
            {
                if (tag.Attributes.ContainsKey(HelperConstant.General.DATA_BLOCKUI))
                {
                    tag.Attributes.Remove(HelperConstant.General.DATA_BLOCKUI);
                }

                if (tag.Attributes.ContainsKey(HelperConstant.CssClassName.CSS_CLASS))
                {
                    if (tag.Attributes[HelperConstant.CssClassName.CSS_CLASS].Contains(HelperConstant.CssClassName.BLOCK_UI_CLASS))
                    {
                        tag.Attributes[HelperConstant.CssClassName.CSS_CLASS] = tag.Attributes[HelperConstant.CssClassName.CSS_CLASS].Replace(HelperConstant.CssClassName.BLOCK_UI_CLASS, "");
                    }
                }

                tag.Attributes.Add(HelperConstant.General.DATA_BLOCKUI, false.ToString());

                if (!string.IsNullOrEmpty(confirmOption.ConfirmString))
                {
                    tag.Attributes.Add(HelperConstant.General.LINK_ONCLICK, confirmOption.ConfirmString);
                }
            }

            return tag;
        }

        public virtual IConfirmActionButton ConfirmOption(ConfirmOption confirmoption)
        {
            this.confirmOption = confirmoption;
            if (this.action != null)
            {
                this.confirmOption.confirmAction = new ActionInfo();
                if (!string.IsNullOrEmpty(this.action.actionUrl)) this.confirmOption.confirmAction.actionUrl = this.action.actionUrl + "/" + "{0}";
            }

            this.confirmOption.confirmAction.methodType = action.methodType;
            return _instance;
        }
    }
}
