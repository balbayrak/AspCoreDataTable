using AspCoreDataTable.Core.Button.Concrete;
using Microsoft.AspNetCore.Html;

namespace AspCoreDataTable.Core.Button.Abstract
{
    public interface IActionButton<T> where T : IActionButton<T>
    {
        T ActionInfo(ActionInfo action);
        T Text(string text);
        T CssClass(string cssClass);
        T IClass(string iclass);
        T BlockUI(string blocktarget = null);
    }

}
