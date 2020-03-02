using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreDataTable.Test.Models
{
    public class AjaxResult
    {
        public AjaxResult()
        {
            this.Result = 0;
            this.ResultText = null;
        }
        public AjaxResultEnum Result { get; internal set; }
        public string ResultText { get; internal set; }
    }

    public enum AjaxResultEnum
    {
        Success = 1,
        Warning = 2,
        Error = 0
    }

}
