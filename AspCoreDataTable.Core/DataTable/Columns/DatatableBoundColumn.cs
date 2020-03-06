using AspCoreDataTable.Core.DataTable.Columns.Buttons;
using System;
using System.Reflection;

namespace AspCoreDataTable.Core.DataTable.Columns
{
    public class DatatableBoundColumn<TModel>
    {
        public bool columnIsPrimaryKey { get; set; }
        public string columnProperty { get; set; }
        public string column_Property_Exp { get; set; }
        public string orderByDirection { get; set; }
        public string searchable { get; set; }

    }
}
