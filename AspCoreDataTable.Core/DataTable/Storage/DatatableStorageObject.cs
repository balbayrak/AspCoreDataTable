﻿using AspCoreDataTable.Core.DataTable.Columns;
using AspCoreDataTable.Core.DataTable.Columns.Buttons;
using AspCoreDataTable.Core.DataTable.Rows;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AspCoreDataTable.Core.DataTable.Storage
{
    public class DatatableStorageObject<TEntity>
    where TEntity : class
    {
        #region Public Properties

        public List<DatatableBoundColumn<TEntity>> DatatableProperties { get; set; }

        public List<DatatableActionColumn> DatatableActions { get; set; }

        public List<RowCondition> RowConditions { get; set; }

        #endregion
    }
}