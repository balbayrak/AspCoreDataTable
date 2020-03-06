﻿using AspCoreDataTable.Core.DataTable.Columns;
using AspCoreDataTable.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace AspCoreDataTable.Core.DataTable.Storage
{
    public class DatatableEntityProvider<TEntity> where TEntity : class
    {
        #region Fields

        private readonly List<TEntity> _entities;

        #endregion

        #region Constructors and Destructors

        public DatatableEntityProvider(List<TEntity> entities)
        {
            _entities = entities;
        }

        #endregion

        #region Public Methods and Operators

        public IEnumerable<IDictionary<string, dynamic>> Provide(DatatableStorageObject<TEntity> datatableStorageObject)
        {
            List<DatatableBoundColumn<TEntity>> datatableProperties = datatableStorageObject.DatatableProperties ?? datatableStorageObject.DatatableProperties;

            for (int i = 0; i < _entities.Count; i++)
            {
                var dictionary = new Dictionary<string, dynamic>();
                string primarykey = string.Empty;

                if (datatableStorageObject.rowCssCondition != null)
                {
                    var result = ExpressionBuilder.Evaluation(_entities[i], datatableStorageObject.rowCssCondition.property);
                    bool isEqueal = false;

                    if(result is string)
                    {
                        isEqueal = ((string)result).Equals((string)datatableStorageObject.rowCssCondition.value, System.StringComparison.InvariantCultureIgnoreCase);
                    }
                    else
                    {
                        isEqueal = result.Equals(datatableStorageObject.rowCssCondition.value);
                    }

                    if(isEqueal)
                    {
                        dictionary[i.ToString()] = datatableStorageObject.rowCss;
                    }
                }


                foreach (var property in datatableProperties)
                {
                    var result = ExpressionBuilder.Evaluation(_entities[i], property.column_Property_Exp);
                    if (property.columnIsPrimaryKey)
                    {
                        primarykey = result == null ? string.Empty : result.ToString();
                    }
                    else
                    {
                        dictionary[property.columnProperty] = result == null ? string.Empty : result.ToString();
                    }
                }

                dictionary["test"] = "test";

                if (datatableStorageObject.DatatableActions != null && datatableStorageObject.DatatableActions.Count > 0)
                {
                    foreach (var item in datatableStorageObject.DatatableActions)
                    {
                        string tmp_ActionColumn = item.ActionColumn;
                        if (item.conditions != null)
                        {
                            bool conditionTrue = false;
                            string[] arr = item.ActionColumn.Split("</a>");

                            foreach (var condition in item.conditions)
                            {
                                var conditionResult = ExpressionBuilder.Evaluation(_entities[i], condition.Value.property);

                                if (conditionResult.Equals(condition.Value.value))
                                {
                                    conditionTrue = true;
                                    arr[condition.Key] = string.Empty;
                                }
                            }
                            if (conditionTrue)
                            {
                                var actionlist = arr.Where(t => !string.IsNullOrEmpty(t)).ToList();
                                if (actionlist.Count == 1)
                                {
                                    actionlist[0].Concat("</a>");
                                    tmp_ActionColumn = actionlist[0];
                                }
                                else
                                {
                                    tmp_ActionColumn = string.Join("</a>", actionlist);
                                }
                            }
                        }

                        string value = string.Format(tmp_ActionColumn, primarykey);
                        value = value.Replace("[[", "{");
                        value = value.Replace("]]", "}");
                        dictionary[item.ActionColumnHeader] = value;
                        tmp_ActionColumn = null;
                    }
                }
                yield return dictionary;
            }
        }

        #endregion
    }
}