using System;
using System.Linq;

namespace DocumentSystem
{
    abstract class OfficeDocument: EncryptableDocument
    {
        public string Version
        {
            get { return this.GetProperty("version"); }
            set { this.SetProperty("version", value); }
        }

        public OfficeDocument(string[] attributes) : base(attributes)
        {
        }
    }
}
