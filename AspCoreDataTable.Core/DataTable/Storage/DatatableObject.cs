using System.Collections.Generic;

namespace AspCoreDataTable.Core.DataTable.Storage
{
    public class DatatableObject<TEntity>
            where TEntity : class
    {
        public string controllerName { get; set; }

        public Dictionary<string,string> dataTablesDictionary { get; set; }

    }
}
