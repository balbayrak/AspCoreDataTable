using AspCoreDataTable.Core.DataTable.Storage;
using AspCoreDataTable.Core.General;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AspCoreDataTable.Core.Extensions
{
    public static class DatatableStorageExt
    {
        public static string Serialize<TEntity>(this DatatableStorageObject<TEntity> storageObject) where TEntity : class
        {
            string seralized = JsonConvert.SerializeObject(storageObject);
            seralized = seralized.Replace("\"", HelperConstant.General.SPLIT_CHAR1.ToString());
            seralized = seralized.EncrpytDecryptDataTableData(true).CompressString();
            return seralized;
        }

        public static DatatableStorageObject<TEntity> DeSerialize<TEntity>(this string serializedStorageObject) where TEntity : class
        {

            serializedStorageObject = serializedStorageObject.UnCompressString();
            serializedStorageObject = serializedStorageObject.Replace(HelperConstant.General.SPLIT_CHAR1.ToString(), "\"");
            serializedStorageObject = serializedStorageObject.EncrpytDecryptDataTableData(false);
            DatatableStorageObject<TEntity> storageObj = JsonConvert.DeserializeObject<DatatableStorageObject<TEntity>>(serializedStorageObject);
            return storageObj;
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
            encryptionDict.Add("Confirm.showConfirm", "éd3");
            encryptionDict.Add("searchable", "éd4");
            encryptionDict.Add("ActionColumn", "éd5");
            encryptionDict.Add("mt-checkbox mt-checkbox-single mt-checkbox-outline", "éd6");

            encryptionDict.Add("_Link_Modal_Body", "éd7");
            encryptionDict.Add("checkbox", "éd8");
            encryptionDict.Add("Chk_Actions", "éd9");

            encryptionDict.Add("checkboxRowid", "ée1");
            encryptionDict.Add("checkboxes", "ée2");
            encryptionDict.Add("_Link_Modal", "ée3");
            encryptionDict.Add("<input class=", "ée4");
            encryptionDict.Add("Actions_", "ée5");
            encryptionDict.Add("btn-blockui-modal", "ée6");

          
            foreach (var key in encryptionDict.Keys)
            {
                if (isEncrpytion)
                    compressedString = compressedString.Replace(key, encryptionDict[key]);
                else
                    compressedString = compressedString.Replace(encryptionDict[key], key);
            }

            return compressedString;
        }

    }
}
