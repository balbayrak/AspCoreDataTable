using AspCoreDataTable.Core.Extensions;
using AspCoreDataTable.Core.General;
using AspCoreDataTable.Core.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspCoreDataTable.Core.DataTable.Storage
{
    public static class DatatableStorageHelper
    {
        #region Public Methods

        public static void AddDatatableProperties<TEntity>(
            this IStorage storage, string datatableId, DatatableStorageObject<TEntity> properties)
            where TEntity : class
        {
            var requestPath = HttpContextWrapper.Current.Request.Host.Value + HttpContextWrapper.Current.Request.Path;
            Uri uri = new Uri(requestPath);
            var controllerName = uri.AbsoluteUri.Split('/')[1];

            DatatableObject<TEntity> cookieObject = storage.GetOrMakeDatatablesDictionary<TEntity>(controllerName, datatableId);
            cookieObject.dataTablesDictionary = cookieObject.dataTablesDictionary ?? new Dictionary<string, string>();
            if (!cookieObject.dataTablesDictionary.ContainsKey(datatableId))
            {
                cookieObject.controllerName = controllerName;
                cookieObject.dataTablesDictionary = cookieObject.dataTablesDictionary ?? new Dictionary<string, string>();
                cookieObject.dataTablesDictionary.Add(datatableId, JsonConvert.SerializeObject(properties, Formatting.None).EncrpytDecryptDataTableData(true));



                string datatableStorageKey = HelperConstant.DataTable.DATA_TABLE_COOKIE_KEY + "_" + datatableId;
                storage.SetObject(datatableStorageKey, cookieObject);

            }
        }

        public static DatatableStorageObject<TEntity> GetDatatableProperties<TEntity>(
            this IStorage storage, string datatableId) where TEntity : class
        {
            string datatableStorageKey = HelperConstant.DataTable.DATA_TABLE_COOKIE_KEY + "_" + datatableId;
            DatatableObject<TEntity> cookieObject = storage.GetObject<DatatableObject<TEntity>>(datatableStorageKey);

            if (cookieObject != null && cookieObject.dataTablesDictionary != null)
            {
                if (cookieObject.dataTablesDictionary.ContainsKey(datatableId))
                {
                    return JsonConvert.DeserializeObject<DatatableStorageObject<TEntity>>(cookieObject.dataTablesDictionary[datatableId].ToString().EncrpytDecryptDataTableData(false));
                }
            }
            throw new ArgumentException("No datatable properties found in storage!", datatableStorageKey.ToString());
        }

        public static string GetSearchableColumnString<TEntity>(this DatatableStorageObject<TEntity> storageObject)
   where TEntity : class
        {
            var list = storageObject.DatatableProperties.Where(t => t.searchable != null);
            List<string> result = new List<string>();
            foreach (var item in list)
            {
                var propertyName = item.column_Property_Exp.Substring(item.column_Property_Exp.IndexOf('.') + 1);
                result.Add(string.Join(HelperConstant.General.SPLIT_CHAR, item.searchable, propertyName));
            }

            if (result.Count > 0)
            {
                if (result.Count > 1)
                {
                    return string.Join(HelperConstant.General.SPLIT_CHAR1, result);
                }
                else
                    return result[0];
            }

            return string.Empty;
        }

        #endregion

        #region Private Methods

        private static DatatableObject<TEntity> GetOrMakeDatatablesDictionary<TEntity>(
            this IStorage storage, string controllerName,string datatableId) where TEntity : class
        {
            string datatableStorageKey = HelperConstant.DataTable.DATA_TABLE_COOKIE_KEY + "_" + datatableId;

            DatatableObject<TEntity> cookieObject = storage.GetObject<DatatableObject<TEntity>>(datatableStorageKey);

            if (cookieObject != null && !cookieObject.controllerName.Equals(controllerName))
            {
                storage.Remove(datatableStorageKey);
                return new DatatableObject<TEntity>();
            }

            return cookieObject ?? new DatatableObject<TEntity>();
        }

        private static string EncrpytDecryptDataTableData(this string compressedString, bool isEncrpytion)
        {
            Dictionary<string, string> encryptionDict = new Dictionary<string, string>();

            encryptionDict.Add("<a class=", "é1");
            encryptionDict.Add("data-blockui=", "é2");
            encryptionDict.Add("data-target=", "é3");
            encryptionDict.Add("data-target-body=", "é4");
            encryptionDict.Add("data-target-url=", "é5");
            encryptionDict.Add("data-toggle=", "é6");
            encryptionDict.Add("<i class=", "é7");
            encryptionDict.Add("showCancelButton:", "é8");
            encryptionDict.Add("confirmButtonColor:", "é9");

            encryptionDict.Add("cancelButtonColor:", "éa0");
            encryptionDict.Add("confirmButtonText:", "éa1");
            encryptionDict.Add("closeOnConfirm:", "éa2");
            encryptionDict.Add("BlockFunc.showSpinnerBlock();", "éa3");
            encryptionDict.Add("BlockFunc.closeSpinnerBlock();", "éa4");
            encryptionDict.Add("url:decodeURIComponent(", "éa5");
            encryptionDict.Add("dataTablesDictionary", "éa6");
            encryptionDict.Add("columnIsPrimaryKey", "éa7");
            encryptionDict.Add("column_Property_Exp", "éa9");


            encryptionDict.Add("orderByDirection", "éb1");
            encryptionDict.Add("location.reload();", "éb2");
            encryptionDict.Add("if(isconfirm)", "éb3");
            encryptionDict.Add("$.ajax(", "éb4");
            encryptionDict.Add("function(isconfirm)", "éb5");
            encryptionDict.Add("data-evet-httpmethod=", "éb6");

            encryptionDict.Add("DatatableProperties", "éb7");
            encryptionDict.Add("DatatableActions", "éb8");

            encryptionDict.Add("event.preventDefault();", "éc1");
            encryptionDict.Add("cancelButtonText:", "éc2");
            encryptionDict.Add("success:function()", "éc3");
            encryptionDict.Add("error:function()", "éc4");
            encryptionDict.Add("return false;", "éc5");
            encryptionDict.Add("ActionColumnHeader", "éc6");

            encryptionDict.Add("columnProperty", "éc7");
            encryptionDict.Add("onclick=", "éc8");

            encryptionDict.Add("title:", "éc9");
            encryptionDict.Add("text:", "éc9");
            encryptionDict.Add("type:", "éd1");
            encryptionDict.Add("warning", "éd2");


            foreach (var key in encryptionDict.Keys)
            {
                if (isEncrpytion)
                    compressedString = compressedString.Replace(key, encryptionDict[key]);
                else
                    compressedString = compressedString.Replace(encryptionDict[key], key);
            }


            return compressedString;
        }

        #endregion
    }
}