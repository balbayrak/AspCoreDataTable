using System;
using System.Collections.Generic;
using System.Text;
using AspCoreDataTable.Core.DataTable.ModelBinder;
using Microsoft.Extensions.DependencyInjection;

namespace AspCoreDataTable.Core.Extensions
{
   public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataTableProvider(this IServiceCollection services)
        {
            services.AddMvcCore(t => t.ModelBinderProviders.Insert(0, new JQueryDataTablesModelProvider()));
            return services;
        }
    }
}
