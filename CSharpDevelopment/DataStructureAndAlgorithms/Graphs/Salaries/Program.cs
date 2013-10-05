using System;
using System.Collections.Generic;

namespace Salaries
{
    class Program
    {
        static HashSet<int> vizited = new HashSet<int>();

        static void Main()
        {
#if DEBUG
            Console.SetIn(new System.IO.StreamReader("../../input.txt"));
#endif
            Dictionary<int, Employer> employers = new Dictionary<int, Employer>();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                employers.AddSafe(i, new Employer(i));
                var employer = employers[i];

                string input = Console.ReadLine();
                for (int j = 0; j < n; j++)
                {
                    if (input[j] == 'Y')
                    {
                        employers.AddSafe(j, new Employer(j));
                        var worker = employers[j];

                        employer.Employers.Add(worker);
                    }
                }
            }

            long sum = 0;

            for (int i = 0; i < n; i++)
            {
                Solve(employers[i]);
            }

            foreach (var employer in employers.Values)
            {
                sum += employer.Salary;
            }

            Console.WriteLine(sum);
        }

        private static void Solve(Employer employer)
        {
            if (vizited.Contains(employer.Id))
            {
                return;
            }

            vizited.Add(employer.Id);

            if (employer.Employers.Count == 0)
            {
                employer.Salary = 1;
                return;
            }

            foreach (var e in employer.Employers)
            {
                Solve(e);
                employer.Salary += e.Salary;
            }
        }
    }
}
