using AspCoreDataTable.General;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspCoreDataTable.Core.DataTable.Storage
{
    public class DatatableParser<TEntity>
    where TEntity : class
    {
        #region Fields


        private readonly DatatableStorageObject<TEntity> _sessionObject;

        private List<TEntity> _entities;

        #endregion

        #region Constructors and Destructors

        public DatatableParser(List<TEntity> entities, DatatableStorageObject<TEntity> sessionObject)
        {
            _entities = entities;
            _sessionObject = sessionObject;
        }

        #endregion

        #region Public Methods and Operators


        public JQueryDataTablesResponse Parse(JQueryDataTablesModel param, int totalRecord, int displayRecord)
        {
            int totalRecords = totalRecord;
            int displayRecords = displayRecord;

            var provider = new DatatableEntityProvider<TEntity>(_entities);

            var reply = new JQueryDataTablesResponse(provider.Provide(_sessionObject).ToArray(), totalRecords, totalRecords, Convert.ToInt32(param.sEcho));
      
            return reply;
        }

        #endregion
    }
}