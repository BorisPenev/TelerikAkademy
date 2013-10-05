using System;
using System.Linq;

namespace HumanWork
{
    class Worker : Human
    {
        private int weekSalary = 0;
        public int WeekSalary
        {
            get { return weekSalary; }
            set { weekSalary = value; }
        }
        
        private int workHoursPerDay = 0;
        public int WorkHoursPerDay
        {
            get { return workHoursPerDay; }
            set { workHoursPerDay = value; }
        }

        public Worker(string firstName, string lastName, int weekSalary, int workHoursPerDay)
            : base(firstName, lastName)
        {
            this.WeekSalary = weekSalary;
            this.WorkHoursPerDay = workHoursPerDay;
        }

        public float MoneyPerHour()
        {
            return this.WeekSalary / 5 / WorkHoursPerDay;
        }

        public override string ToString()
        {
            return base.ToString() + " " + this.MoneyPerHour();
        }
    }
}
