using System;
using System.Collections.Generic;
using System.Text;
using AspCoreDataTable.Core.General;
using AspCoreDataTable.Core.General.Enums;
using AspCoreDataTable.Core.Modal.Abstract;

namespace AspCoreDataTable.Core.Modal.Concrete
{
    public class ActionForm : IActionForm
    {
        public ActionForm(string id, string SubmitFormId, string Title, string SmallTitle, bool isModal, EnumModalSize modalSize)
        {
            this.Id = id;
            this.SubmitFormId = SubmitFormId;
            this.Title = Title;
            this.SmallTitle = SmallTitle;
            this.isModal = isModal;
            this.modalSize = modalSize;
        }

        public string Id { get; set; }

        public bool isModal { get; set; }

        public string SmallTitle { get; set; }

        public string SubmitFormId { get; set; }

        public string Title { get; set; }

        public EnumModalSize modalSize { get; set; }

        public ActionForm()
        {
            this.Id = string.Empty;
            this.isModal = false;
            this.SmallTitle = string.Empty;
            this.SubmitFormId = string.Empty;
            this.Title = string.Empty;
            this.modalSize = EnumModalSize.Default;
        }
    }
}
