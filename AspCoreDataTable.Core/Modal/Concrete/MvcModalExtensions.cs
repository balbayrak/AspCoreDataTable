using AspCoreDataTable.Core.Extensions;
using AspCoreDataTable.Core.General;
using AspCoreDataTable.Core.General.Enums;
using AspCoreDataTable.Core.Modal.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspCoreDataTable.Core.Modal.Concrete
{
    public static class MvcModalExtensions
    {
        public static string ModalHelper(string modalformId, EnumModalSize modalSize, bool backDropStatic)
        {
            var divMain = new TagBuilder("div");
            divMain.GenerateId(modalformId, "");
            divMain.AddCssClass(getModalWithSize(modalSize) + " custommodal");
            divMain.Attributes.Add("aria-hidden", "true");
            divMain.Attributes.Add("role", "basic");

            if (backDropStatic)
                divMain.Attributes.Add("data-backdrop", "static");

            var divDialog = new TagBuilder("div");
            divDialog.AddCssClass(getModalDialogWithSize(modalSize));

            var divContent = new TagBuilder("div");

            divContent.AddCssClass("modal-content");
            divContent.GenerateId(modalformId + HelperConstant.DataTable.BODY_ID, "");

            divDialog.InnerHtml.Append(divContent.ConvertHtmlString());
            divMain.InnerHtml.Append(divDialog.ConvertHtmlString());
            return divMain.ConvertHtmlString();
        }

        public static string ModalHelper(string modalformId, string submitformId, string titleValue, string smallTitleValue, EnumModalSize modalSize, bool backDropStatic)
        {
            var divMain = new TagBuilder("div");
            divMain.GenerateId(modalformId, "");
            divMain.AddCssClass(getModalWithSize(modalSize));
            divMain.Attributes.Add("aria-hidden", "true");
            divMain.Attributes.Add("role", "basic");

            if (backDropStatic)
                divMain.Attributes.Add("data-backdrop", "static");

            var divDialog = new TagBuilder("div");
            divDialog.AddCssClass(getModalDialogWithSize(modalSize));

            var divContent = new TagBuilder("div");

            divContent.AddCssClass("modal-content");

            TagBuilder divHeader = null;
            if (!string.IsNullOrEmpty(titleValue))
            {
                divHeader = new TagBuilder("div");
                divHeader.AddCssClass("modal-header");
                var headerLargeTitle = new TagBuilder("h3");
                headerLargeTitle.AddCssClass("page-title");
                var smallTitle = new TagBuilder("small");
                smallTitle.InnerHtml.Append(smallTitleValue);
                headerLargeTitle.InnerHtml.Append(titleValue + " " + smallTitle.ToString());
                divHeader.InnerHtml.Append(headerLargeTitle.ToString());
            }

            var divBody = new TagBuilder("div");
            divBody.GenerateId(modalformId + HelperConstant.DataTable.BODY_ID, "");
            divBody.AddCssClass("modal-body");

            TagBuilder divFooter = null;

            if (!string.IsNullOrEmpty(submitformId))
            {
                divFooter = new TagBuilder("div");
                divFooter.AddCssClass("modal-footer");

                var buttonSubmit = new TagBuilder("button");
                buttonSubmit.AddCssClass("btn green");
                buttonSubmit.Attributes.Add("type", "submit");
                buttonSubmit.InnerHtml.Append("Save");
                buttonSubmit.Attributes.Add("form", submitformId);

                var buttonCancel = new TagBuilder("button");
                buttonCancel.AddCssClass("btn red");
                buttonCancel.Attributes.Add("data-dismiss", "modal");
                buttonCancel.InnerHtml.Append("Cancel");

                divFooter.InnerHtml.Append(buttonSubmit.ToString() + buttonCancel.ToString());
            }

            divContent.InnerHtml.Append((divHeader != null ? divHeader.ToString() : string.Empty) + divBody.ToString() + (divFooter != null ? divFooter.ToString() : string.Empty));
            divDialog.InnerHtml.Append(divContent.ToString());

            divMain.InnerHtml.Append(divDialog.ToString());

            return (divMain.ToString());
        }

        public static string ModalHelper(IActionForm form)
        {
            var divMain = new TagBuilder("div");
            divMain.GenerateId(form.Id, "");
            divMain.AddCssClass(getModalWithSize(form.modalSize));
            divMain.Attributes.Add("aria-hidden", "true");
            divMain.Attributes.Add("role", "basic");
            divMain.Attributes.Add("data-backdrop", "static");

            var divDialog = new TagBuilder("div");
            divDialog.AddCssClass(getModalDialogWithSize(form.modalSize));

            var divContent = new TagBuilder("div");

            divContent.AddCssClass("modal-content");

            TagBuilder divHeader = null;
            if (!string.IsNullOrEmpty(form.Title))
            {
                divHeader = new TagBuilder("div");
                divHeader.AddCssClass("modal-header");
                var headerLargeTitle = new TagBuilder("h3");
                headerLargeTitle.AddCssClass("page-title");
                var smallTitle = new TagBuilder("small");
                smallTitle.InnerHtml.Append(form.SmallTitle);
                headerLargeTitle.InnerHtml.Append(form.Title + " " + smallTitle.ToString());
                divHeader.InnerHtml.Append(headerLargeTitle.ToString());
            }

            var divBody = new TagBuilder("div");
            divBody.GenerateId(form.Id + HelperConstant.DataTable.BODY_ID, "");
            divBody.AddCssClass("modal-body");

            TagBuilder divFooter = null;

            if (!string.IsNullOrEmpty(form.SubmitFormId))
            {
                divFooter = new TagBuilder("div");
                divFooter.AddCssClass("modal-footer");

                var buttonSubmit = new TagBuilder("button");
                buttonSubmit.AddCssClass("btn green");
                buttonSubmit.Attributes.Add("type", "submit");
                buttonSubmit.InnerHtml.Append("Save");
                buttonSubmit.Attributes.Add("form", form.SubmitFormId);

                var buttonCancel = new TagBuilder("button");
                buttonCancel.AddCssClass("btn red");
                buttonCancel.Attributes.Add("data-dismiss", "modal");
                buttonCancel.InnerHtml.Append("Cancel");

                divFooter.InnerHtml.Append(buttonSubmit.ToString() + buttonCancel.ToString());
            }

            divContent.InnerHtml.Append((divHeader != null ? divHeader.ToString() : string.Empty) + divBody.ToString() + (divFooter != null ? divFooter.ToString() : string.Empty));
            divDialog.InnerHtml.Append(divContent.ToString());

            divMain.InnerHtml.Append(divDialog.ToString());

            return (divMain.ToString());
        }

        private static string getModalWithSize(EnumModalSize modalSize)
        {
            if (modalSize.Equals(EnumModalSize.Default))
            {
                return "modal fade";
            }
            else if (modalSize.Equals(EnumModalSize.Draggable))
            {
                return "modal fade draggable-modal ui-draggable";
            }
            else if (modalSize.Equals(EnumModalSize.FullWidth))
            {
                return "modal fade";
            }
            else if (modalSize.Equals(EnumModalSize.Large))
            {
                return "modal fade bs-modal-lg";
            }
            else if (modalSize.Equals(EnumModalSize.Long))
            {
                return "modal fade modal-scroll";
            }
            else if (modalSize.Equals(EnumModalSize.Small))
            {
                return "modal fade bs-modal-sm";
            }

            return "modal fade";
        }

        private static string getModalDialogWithSize(EnumModalSize modalSize)
        {
            if (modalSize.Equals(EnumModalSize.Default))
            {
                return "modal-dialog";
            }
            else if (modalSize.Equals(EnumModalSize.Draggable))
            {
                return "modal-dialog";
            }
            else if (modalSize.Equals(EnumModalSize.FullWidth))
            {
                return "modal-dialog modal-full";
            }
            else if (modalSize.Equals(EnumModalSize.Large))
            {
                return "modal-dialog modal-lg";
            }
            else if (modalSize.Equals(EnumModalSize.Long))
            {
                return "modal-dialog";
            }
            else if (modalSize.Equals(EnumModalSize.Small))
            {
                return "modal-dialog modal-sm";
            }

            return "modal-dialog";
        }
    }
}