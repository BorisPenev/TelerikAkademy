using System;
using System.Collections.Generic;
using System.Linq;

namespace School
{
    class Class : CommentService
    {
        public readonly string ClassID;

        private List<Teacher> teachers = new List<Teacher>();
        public List<Teacher> Teachers
        {
            get { return teachers; }
            set { teachers = value; }
        }

        public Class(string identifier)
        {
            this.ClassID = identifier;
        }
    }
}
