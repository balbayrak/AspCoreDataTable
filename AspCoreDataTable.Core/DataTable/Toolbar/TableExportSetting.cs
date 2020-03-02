using AspCoreDataTable.Core.General.Enums;

namespace AspCoreDataTable.Core.DataTable.Toolbar
{
    public class TableExportSetting
    {
        public string title { get; set; }
        public string cssClass { get; set; }
        public bool isExportPdf { get; set; }
        public bool isExportCSV { get; set; }
        public bool isExportExcel { get; set; }
        public bool isPrintable { get; set; }
        public EnumFormSide formSide { get; set; }

        public TableExportSetting()
        {
            this.isExportCSV = false;
            this.isExportExcel = false;
            this.isExportPdf = false;
            this.isPrintable = false;
            formSide = EnumFormSide.LetfSide;
            this.title = string.Empty;
            this.cssClass = string.Empty;
        }

        public TableExportSetting(string _title, string _cssClass, bool _isExportCSV, bool _isExportExcel, bool _isExportPdf, bool _isPrintable, EnumFormSide _side)
        {
            this.isExportCSV = _isExportCSV;
            this.isExportExcel = _isExportExcel;
            this.isExportPdf = _isExportPdf;
            this.isPrintable = _isPrintable;
            formSide = _side;
            this.title = _title;
            this.cssClass = _cssClass;
        }
    }
}
