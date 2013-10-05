using System;
using System.Collections.Generic;
using System.Linq;

namespace HumanWork
{
    class Program
    {
        static void Main()
        {
            List<Student> students = new List<Student>();
            for (int i = 10; i < 20; i++)
            {
                students.Add(new Student("firstName" + i, "lastName" + i, i / 2));
            }

            students.OrderBy(s => s.Grade).ToList().ForEach(s =>
                {
                    Console.WriteLine(s);
                });

            Console.WriteLine(Environment.NewLine + "***************************************" + Environment.NewLine);

            List<Worker> workers = new List<Worker>();
            for (int i = 10; i < 20; i++)
            {
                workers.Add(new Worker("firstName" + i, "lastName" + i, (i *156 / 3), 8));
            }

            workers.OrderByDescending(w => w.MoneyPerHour()).ToList().ForEach(w =>
            {
                Console.WriteLine(w);
            });

            Console.WriteLine(Environment.NewLine + "***************************************" + Environment.NewLine);

            List<Human> humans = new List<Human>();
            humans.AddRange(students);
            humans.AddRange(workers);
            humans.OrderBy(h => h.FirstName).ThenBy(h => h.LastName).ToList().ForEach(h =>
            {
                Console.WriteLine(h);
            });
        }
    }
}
