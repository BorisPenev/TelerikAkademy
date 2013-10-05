using System;
using System.Linq;

namespace DocumentSystem
{
    abstract class BinaryDocument: Document
    {
        public byte Bize 
        {
            get { return Convert.ToByte(this.GetProperty("size")); }
            set { this.SetProperty("size", value.ToString()); }
        }

        public BinaryDocument(string[] attributes) : base(attributes)
        {
        }
    }
}
