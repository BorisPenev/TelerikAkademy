using System;

namespace School
{
    public class Student
    {
        private readonly string name = string.Empty;
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        private int id = 0;
        public int Id
        {
            get
            {
                return this.id;
            }
            internal set
            {
                if (value < UniqueId.MinId || value > UniqueId.MaxId)
                {
                    throw new ArgumentOutOfRangeException(string.Format("Id Need to be between {0} and {1}", UniqueId.MinId, UniqueId.MaxId));
                }
                this.id = value;
            }
        }

        public Student(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name cannot be null or empty");
            }

            this.name = name;
            this.id = UniqueId.NewId();
        }
    }
}
