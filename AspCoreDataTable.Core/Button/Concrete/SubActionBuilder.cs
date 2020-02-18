using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspCoreDataTable.Core.Button.Abstract;

namespace AspCoreDataTable.Core.Button.Concrete
{
    public class SubActionBuilder
    {
        private IGrupActionButtonInternal actionButton { get; set; }

        public SubActionBuilder(IGrupActionButtonInternal actionButton)
        {
            this.actionButton = actionButton;
        }

        public IModalActionButton ModalActionButton(string id)
        {
            ModalActionButton act = new ModalActionButton(id);
            actionButton.AddAction(act);
            return act;
        }

        public IDefaultActionButton ActionButton(string id)
        {
            DefaultActionButton act = new DefaultActionButton(id);
            actionButton.AddAction(act);
            return act;
        }

        public IDefaultActionButton DownloadButton(string id)
        {
            DownloadActionButton act = new DownloadActionButton(id);
            actionButton.AddAction(act);
            return act;
        }

        public ISubmitActionButton SubmitButton(string id)
        {
            SubmitFormActionButton act = new SubmitFormActionButton(id);
            actionButton.AddAction(act);
            return act;
        }
    }
}
