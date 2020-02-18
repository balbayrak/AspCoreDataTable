using AspCoreDataTable.Core.DataTable.Columns;
using System.Collections.Generic;

namespace AspCoreDataTable.Core.DataTable.Storage
{
    public class DatatableEntityProvider<TEntity>
    where TEntity : class
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

        public IEnumerable<IDictionary<string, dynamic>> Provide(DatatableStorageObject<TEntity> datatableSessionObject)
        {
            List<DatatableBoundColumn<TEntity>> datatableProperties = datatableSessionObject.DatatableProperties ?? datatableSessionObject.DatatableProperties;

            foreach (TEntity entity in _entities)
            {
                var dictionary = new Dictionary<string, dynamic>();
                string primarykey = string.Empty;

                foreach (var property in datatableProperties)
                {
                    DatatableBoundColumn<TEntity> prop = (DatatableBoundColumn<TEntity>)property;
                    if (prop.columnIsPrimaryKey)
                    {
                        primarykey = prop.Evaluation(entity);
                    }
                    else
                        dictionary[prop.columnProperty] = prop.Evaluation(entity);
                }
                if (datatableSessionObject.DatatableActions != null && datatableSessionObject.DatatableActions.Count > 0)
                {
                    foreach (var item in datatableSessionObject.DatatableActions)
                    {
                        string value = string.Format(item.ActionColumn, primarykey);
                        value = value.Replace("[[", "{");
                        value = value.Replace("]]", "}");
                        dictionary[item.ActionColumnHeader] = value;
                    }
                }

           

                yield return dictionary;
            }
        }

        #endregion
    }
}