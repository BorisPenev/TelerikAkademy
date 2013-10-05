using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook
{
    public class Content : IComparable<Content>
    {
        private string name;
        public string Name
        {
            get {  return this.name; }
            set { this.name = value; }
        }

        private SortedSet<string> numbers = new SortedSet<string>();
        public SortedSet<string> Numbers
        {
            get { return numbers; }
            set { numbers = value; }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append('[');
            sb.Append(this.Name);
            bool isMultipleNumbers = true;
            foreach (var phone in this.Numbers)
            {
                if (isMultipleNumbers)
                {
                    sb.Append(": ");
                    isMultipleNumbers = false;
                }
                else
                {
                    sb.Append(", ");
                }

                sb.Append(phone);
            }
            sb.Append(']');
            return sb.ToString();
        }

        public int CompareTo(Content other)
        {
            return this.name.CompareTo(other.name);
        }
    }
}