using AspCoreDataTable.Core.Button.Concrete;
using AspCoreDataTable.Core.DataTable.Abstract;
using Microsoft.AspNetCore.Html;

namespace AspCoreDataTable.Core.Button.Abstract
{
    public interface IActionButton<T> : IFluentInterface where T : IActionButton<T>
    {
        T ActionInfo(ActionInfo action);
        T Text(string text);
        T CssClass(string cssClass);
        T IClass(string iclass);
        T BlockUI(string blocktarget = null);
    }

}
