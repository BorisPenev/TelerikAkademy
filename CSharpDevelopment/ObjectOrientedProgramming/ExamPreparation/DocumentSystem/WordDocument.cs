using System;
using System.Linq;

namespace DocumentSystem
{
    class WordDocument: OfficeDocument, IEditable
    {
        public string NumberOfCharacters
        {
            get { return this.GetProperty("numberofcharacters"); }
            set { this.SetProperty("numberofcharacters", value); }
        }

        public WordDocument(string[] attributes) : base(attributes)
        {
        }

        public void ChangeContent(string newContent)
        {
            this.SetProperty("content", newContent);
        }
    }
}
