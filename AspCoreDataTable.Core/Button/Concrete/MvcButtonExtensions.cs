using Microsoft.AspNetCore.Mvc.Rendering;
using AspCoreDataTable.Core.Button.Abstract;

namespace AspCoreDataTable.Core.Button.Concrete
{
    public static class MvcButtonExtensions
    {
        public static IActionButton<IModalActionButton> ModalButton(this IHtmlHelper helper, string id)
        {
            return new ModalHtmlActionButton(id);
        }

        public static IActionButton<IDefaultActionButton> ActionButton(this IHtmlHelper helper, string id)
        {
            return new DefaultHtmlActionButton(id);
        }

        public static IActionButton<IDefaultActionButton> DownloadButton(this IHtmlHelper helper, string id)
        {
            return new DownloadHtmlActionButton(id);
        }

        public static IActionButton<ISubmitActionButton> SubmitButton(this IHtmlHelper helper, string id)
        {
            return new SubmitFormHtmlActionButton(id);
        }
        public static IActionButton<IConfirmActionButton> ConfirmButton(this IHtmlHelper helper, string id)
        {
            return new ConfirmHtmlActionButton(id);
        }
    }
}
