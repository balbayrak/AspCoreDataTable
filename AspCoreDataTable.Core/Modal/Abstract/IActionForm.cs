using System;
using System.Collections.Generic;
using System.Text;
using AspCoreDataTable.Core.General;
using AspCoreDataTable.Core.General.Enums;

namespace AspCoreDataTable.Core.Modal.Abstract
{
    public interface IActionForm
    {
        string Id { get; set; }
        string SubmitFormId { get; set; }

        string SmallTitle { get; set; }

        string Title { get; set; }

        bool isModal { get; set; }

        EnumModalSize modalSize { get; set; }
    }
}
