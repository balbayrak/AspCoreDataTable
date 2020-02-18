using AspCoreDataTable.Core.Block;
using AspCoreDataTable.Core.Button.Concrete;

namespace AspCoreDataTable.Core.Button.Abstract
{
    public interface IActionButtonInternal
    {
        string id { get; set; }
        ActionInfo action { get; set; }
        string text { get; set; }
        string cssClass { get; set; }
        string iclass { get; set; }
        BlockInfo block { get; set; }
        string CreateLink();

    }
}
