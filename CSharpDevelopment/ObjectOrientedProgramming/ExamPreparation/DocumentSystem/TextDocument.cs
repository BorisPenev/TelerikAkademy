using System;
using System.Linq;

namespace DocumentSystem
{
    class TextDocument: Document, IEditable
    {
        public string Charset
        {
            get { return this.GetProperty("charset"); }
            set { this.SetProperty("charset", value); }
        }

        public TextDocument(string[] attributes) : base(attributes)
        {
        }

        public void ChangeContent(string newContent)
        {
            this.SetProperty("content", newContent);
        }
    }
}
