using System;
using System.Reflection;

namespace AspCoreDataTable.Core.DataTable.Columns
{
    public class DatatableBoundColumn<TModel>
    {
        public bool columnIsPrimaryKey { get; set; }
        public string Evaluation(TModel model)
        {
            string columnStr = this.column_Property_Exp.Substring(this.column_Property_Exp.IndexOf('.')).TrimStart('.');
            object result = GetPropertyValue(model, columnStr);
            return result == null ? string.Empty : result.ToString();
        }
        public string columnProperty { get; set; }
        public string column_Property_Exp { get; set; }
        public string orderByDirection { get; set; }

        public string searchable { get; set; }

        private object GetPropertyValue(Object fromObject, string propertyName)
        {
            Type objectType = fromObject.GetType();
            PropertyInfo propInfo = objectType.GetProperty(propertyName);
            if (propInfo == null && propertyName.Contains('.'))
            {
                string firstProp = propertyName.Substring(0, propertyName.IndexOf('.'));
                propInfo = objectType.GetProperty(firstProp);
                if (propInfo == null)//property name is invalid
                {
                    throw new ArgumentException(String.Format("Property {0} is not a valid property of {1}.", firstProp, fromObject.GetType().ToString()));
                }
                return GetPropertyValue(propInfo.GetValue(fromObject, null), propertyName.Substring(propertyName.IndexOf('.') + 1));
            }
            else
            {
                return propInfo.GetValue(fromObject, null);
            }
        }
    }
}
