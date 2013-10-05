using System;
using System.Collections.Generic;
using System.Linq;

namespace School
{
    class Teacher : People
    {
        private List<Discipline> discipline = new List<Discipline>();
        public List<Discipline> Discipline
        {
            get { return discipline; }
            set { discipline = value; }
        }
    }
}
