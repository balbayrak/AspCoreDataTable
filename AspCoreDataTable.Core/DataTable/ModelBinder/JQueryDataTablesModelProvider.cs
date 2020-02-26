using System;
using System.Collections.Generic;
using System.Text;
using AspCoreDataTable.General;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspCoreDataTable.Core.DataTable.ModelBinder
{
    public class JQueryDataTablesModelProvider:IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(JQueryDataTablesModel))
                return new JQueryDataTablesModelBinder();
            return null;
        }
    }
}
