using System;
using System.Linq;

namespace School
{
    abstract class People : CommentService
    {
        private string name = string.Empty;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
