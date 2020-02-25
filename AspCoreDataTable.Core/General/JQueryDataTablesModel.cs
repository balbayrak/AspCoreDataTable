
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AspCoreDataTable.General
{
    /// <summary>
    /// Represents the jQuery DataTables request that is sent for server
    /// side processing.
    /// <para>http://datatables.net/usage/server-side</para>
    /// </summary>
    public class JQueryDataTablesModel
    {
        public string columnInfos { get; set; }
        public string datatableId { get; set; }

        /// <summary>
        /// Gets or sets the information for DataTables to use for rendering.
        /// </summary>
        public int sEcho { get; set; }

        /// <summary>
        /// Gets or sets the display start point.
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Gets or sets the number of records to display.
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// Gets or sets the Global search field.
        /// </summary>
        public string sSearch { get; set; }

        /// <summary>
        /// Gets or sets if the Global search is regex or not.
        /// </summary>
        public bool bRegex { get; set; }

        /// <summary>
        /// Gets or sets the number of columns being display (useful for getting individual column search info).
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// Gets or sets indicator for if a column is flagged as sortable or not on the client-side.
        /// </summary>
        public ReadOnlyCollection<bool> bSortable_ { get; set; }

        /// <summary>
        /// Gets or sets indicator for if a column is flagged as searchable or not on the client-side.
        /// </summary>
        public ReadOnlyCollection<bool> bSearchable_ { get; set; }

        /// <summary>
        /// Gets or sets individual column filter.
        /// </summary>
        public ReadOnlyCollection<string> sSearch_ { get; set; }

        /// <summary>
        /// Gets or sets if individual column filter is regex or not.
        /// </summary>
        public ReadOnlyCollection<bool> bRegex_ { get; set; }

        /// <summary>
        /// Gets or sets the number of columns to sort on.
        /// </summary>
        public int? iSortingCols { get; set; }

        /// <summary>
        /// Gets or sets column being sorted on (you will need to decode this number for your database).
        /// </summary>
        public ReadOnlyCollection<int> iSortCol_ { get; set; }

        /// <summary>
        /// Gets or sets the direction to be sorted - "desc" or "asc".
        /// </summary>
        public ReadOnlyCollection<string> sSortDir_ { get; set; }

        /// <summary>
        /// Gets or sets the value specified by mDataProp for each column. 
        /// This can be useful for ensuring that the processing of data is independent 
        /// from the order of the columns.
        /// </summary>
        public ReadOnlyCollection<string> mDataProp_ { get; set; }

        public ReadOnlyCollection<SortingColumn> GetSortedColumns()
        {
            if (!iSortingCols.HasValue)
            {
                // Return an empty collection since it's easier to work with when verifying against
                return new ReadOnlyCollection<SortingColumn>(new List<SortingColumn>());
            }

            var sortedColumns = new List<SortingColumn>();
            for (int i = 0; i < iSortingCols.Value; i++)
            {
                sortedColumns.Add(new SortingColumn(mDataProp_[iSortCol_[i]], sSortDir_[i]));
            }

            return sortedColumns.AsReadOnly();
        }

       
    }
}