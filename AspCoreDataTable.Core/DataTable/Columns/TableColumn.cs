using AspCoreDataTable.Core.DataTable.Abstract;
using AspCoreDataTable.Core.Extensions;
using AspCoreDataTable.Core.General;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspCoreDataTable.Core.DataTable.Columns
{
    public abstract class TableColumn<TInterface> : ITableColumnInternal
        where TInterface : ITableColumn<TInterface>
    {
        protected abstract TInterface _instance { get; }

        public bool columnIsActionColumn { get; set; }

        public string columnTitle { get; set; }

        public bool visible { get; set; }

        public int width { get; set; }

        public string tableid { get; set; }

        public string HtmlColumn()
        {
            TagBuilder column = new TagBuilder("th");

            if (this.width > 0)
            {
                column.Attributes.Add(HelperConstant.General.DATA_WIDTH, this.width.ToString() + "%");
            }
            column = SubHtmlColumn(column);

            return column.ConvertHtmlString();
        }

        public virtual TagBuilder SubHtmlColumn(TagBuilder column)
        {
            return column;
        }

        public TInterface Title(string title)
        {
            this.columnTitle = title;
            return _instance;
        }

        public TInterface Visible(bool visible)
        {
            this.visible = visible;
            return _instance;
        }

        public TInterface Width(int width)
        {
            this.width = width;
            return _instance;
        }

        public TableColumn()
        {
            this.columnIsActionColumn = false;
            this.width = 0;
            this.visible = true;
        }
    }
}