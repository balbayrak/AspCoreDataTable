using AspCoreDataTable.General;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspCoreDataTable.Core.DataTable.Storage
{
    public class DatatableParser<TEntity> : IDisposable
    where TEntity : class
    {
        #region Fields

        private DatatableStorageObject<TEntity> _storageObject;

        private List<TEntity> _entities;

        #endregion

        #region Constructors and Destructors

        public DatatableParser(List<TEntity> entities, DatatableStorageObject<TEntity> storageObject)
        {
            _entities = entities;
            _storageObject = storageObject;
        }

        public void Dispose()
        {
            _storageObject = null;
            _entities = null;
        }

        #endregion

        #region Public Methods and Operators


        public JQueryDataTablesResponse Parse(JQueryDataTablesModel param, int totalRecord, int displayRecord)
        {
            int totalRecords = totalRecord;
            int displayRecords = displayRecord;

            var provider = new DatatableEntityProvider<TEntity>(_entities);

            var reply = new JQueryDataTablesResponse(provider.Provide(_storageObject).ToArray(), totalRecords, totalRecords, Convert.ToInt32(param.sEcho));

            return reply;
        }

        #endregion
    }
}