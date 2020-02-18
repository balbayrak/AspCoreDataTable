using AspCoreDataTable.Core.ConfirmBuilder;
using AspCoreDataTable.Core.Extensions;
using AspCoreDataTable.Core.General.Enums;
using AspCoreDataTable.Core.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreDataTable.Core.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataTable(this IServiceCollection services, Action<DataTableOption> option)
        {
            DataTableOption dataTableOption = new DataTableOption();
            option(dataTableOption);

            if (dataTableOption != null)
            {
                if (dataTableOption.confirmType == ConfirmType.Alertify)
                {
                    services.AddSingleton<IConfirmService, AlertifyConfirmManager>();
                }
                else if (dataTableOption.confirmType == ConfirmType.BootBox)
                {
                    services.AddSingleton<IConfirmService, BootBoxConfirmManager>();
                }
                else if (dataTableOption.confirmType == ConfirmType.Default)
                {
                    services.AddSingleton<IConfirmService, DefaultConfirmManager>();
                }
                else if (dataTableOption.confirmType == ConfirmType.Sweet)
                {
                    services.AddSingleton<IConfirmService, SweetConfirmManager>();
                }

                if (dataTableOption.storageType == EnumStorage.Cookie)
                {
                    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

                    services.AddSingleton<IStorage, CookieStorage>();
                }

                HttpContextWrapper.Configure(services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>());

            }
            return services;
        }
    }
}
