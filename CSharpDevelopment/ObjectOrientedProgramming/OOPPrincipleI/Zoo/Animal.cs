using System;
using System.Linq;
using Zoo.Interfaces;

namespace Zoo
{
    public abstract class Animal : ISound
    {
        private int age = 0;
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        private string name = string.Empty;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private SexType sex = SexType.Female;
        public virtual SexType Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        public virtual string Sound()
        {
            return string.Empty;
        }

        public Animal(int age, string name, SexType sex)
        {
            this.Age = age;
            this.Name = name;
            this.Sex = sex;
        }
    }
}
