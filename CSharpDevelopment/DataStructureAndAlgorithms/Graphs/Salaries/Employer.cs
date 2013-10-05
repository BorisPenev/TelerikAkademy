using System.Collections.Generic;

namespace Salaries
{
    class Employer
    {
        public int Id { get; set; }
        public long Salary { get; set; }
        public List<Employer> Employers { get; set; }

        public Employer(int id)
        {
            this.Id = id;
            this.Employers = new List<Employer>();
        }
    }
}
