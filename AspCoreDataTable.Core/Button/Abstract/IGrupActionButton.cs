using AspCoreDataTable.Core.Button.Concrete;
using System;

namespace AspCoreDataTable.Core.Button.Abstract
{
    public interface IGrupActionButton : IActionButton<IGrupActionButton>
    {
        IGrupActionButton SubActions(Action<SubActionBuilder> actionBuilder);
    }
}
