using System;
using System.Linq;

namespace DocumentSystem
{
    abstract class MultimediaDocument: BinaryDocument
    {
        public int Length
        {
            get { return Convert.ToInt16(this.GetProperty("length")); }
            set { this.SetProperty("length", value.ToString()); }
        }

        public MultimediaDocument(string[] attributes) : base(attributes)
        {
        }
    }
}
