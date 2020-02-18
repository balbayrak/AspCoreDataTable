using AspCoreDataTable.Core.General.Enums;

namespace AspCoreDataTable.General
{
    public class SortingColumn
    {
        public string propertyName { get; set; }

        public EnumSortingDirection sortDirection { get; set; }

        public SortingColumn()
        {

        }

        public SortingColumn(string propertyName, string sortingDirection)
        {
            this.propertyName = propertyName;
            if (sortingDirection.Equals("asc", System.StringComparison.InvariantCultureIgnoreCase))
            {
                this.sortDirection = EnumSortingDirection.Ascending;
            }
            else
            {
                this.sortDirection = EnumSortingDirection.Descending;
            }
        }
    }
}
