using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreDataTable.Core.General.Enums
{
    public enum EnumPagingType
    {
        bootstrap_number=0,
        //Page number buttons only 
        numbers = 1,
        //'Previous' and 'Next' buttons only
        simple = 2,
        // 'Previous' and 'Next' buttons, plus page numbers
        simple_numbers = 3,
        // 'First', 'Previous', 'Next' and 'Last' buttons
        full = 4,
        //'First', 'Previous', 'Next' and 'Last' buttons, plus page numbers
        full_numbers = 5,
        // 'First' and 'Last' buttons, plus page numbers
        first_last_numbers = 6


    }
}
