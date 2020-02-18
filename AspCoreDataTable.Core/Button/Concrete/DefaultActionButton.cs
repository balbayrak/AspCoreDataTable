using Microsoft.AspNetCore.Mvc.Rendering;
using AspCoreDataTable.Core.Button.Abstract;
using AspCoreDataTable.Core.General.Enums;

namespace AspCoreDataTable.Core.Button.Concrete
{
    public class DefaultActionButton : ActionButton<IDefaultActionButton>, IDefaultActionButton
    {
        public DefaultActionButton(string id) : base(id)
        {
        }

        public DefaultActionButton(string id, string text, string iClass, string cssClass, bool blockui, string blockTarget, string actionUrl, EnumHttpMethod httpMethod = EnumHttpMethod.GET) 
            : base(id,text,iClass,cssClass,blockui,blockTarget,actionUrl,httpMethod)
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
            TagBuilder tag = base.CreateTagBuilder("a");
            return tag;
        }
    }
}
