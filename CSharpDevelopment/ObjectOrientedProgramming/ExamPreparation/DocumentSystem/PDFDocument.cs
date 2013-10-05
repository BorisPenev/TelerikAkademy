using System;
using System.Linq;

namespace DocumentSystem
{
    class PDFDocument: EncryptableDocument
    {
        public string NumberOfPages
        {
            get { return this.GetProperty("numberofpages "); }
            set { this.SetProperty("numberofpages", value); }
        }

        public PDFDocument(string[] attributes) : base(attributes)
        {
        }
    }
}
