using System;
using System.Linq;

namespace DocumentSystem
{
    class ExcelDocument: OfficeDocument
    {
        public string NumberOfRows
        {
            get { return this.GetProperty("numberofrows"); }
            set { this.SetProperty("numberofrows", value); }
        }

        public string NumberOfColumns
        {
            get { return this.GetProperty("numberofcolumns"); }
            set { this.SetProperty("numberofcolumns", value); }
        }

        public ExcelDocument(string[] attributes) : base(attributes)
        {
        }
    }
}
