// Ако някой смята, че няколко задачи в един файл било нечетимо да погледне този напълно реален функционален файл:
// http://nopcommerce.codeplex.com/SourceControl/changeset/view/2cac87e25a44#src/Presentation/Nop.Web/Controllers/ShoppingCartController.cs
// ...от една много хубава shippingcart система и пак да си помисли. 
// Утре когато станете програмисти с такива неща ще се сблъсквате.
// Понеже там няма редове да ви спестя copy&paste: само 2431 са :)

using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentsSort
{
    class Program
    {
        static void Main()
        {
            List<Student> students = new List<Student>();
            students.Add(new Student() { FirstName = "pesho", LastName = "pshev", Age = 18 });
            students.Add(new Student() { FirstName = "ana", LastName = "aneva", Age = 22 });
            students.Add(new Student() { FirstName = "borislav", LastName = "enchev", Age = 44 });
            students.Add(new Student() { FirstName = "svetla", LastName = "ancheva", Age = 13 });

            Console.WriteLine("Write a method that from a given array of students finds all students whose first name is before its last name alphabetically. Use LINQ query operators.");
            var result1 = (from student in students
                         where student.FirstName.CompareTo(student.LastName) == -1
                          select student).OrderBy(s => s.FirstName).ToList(); 
            //OrderBy e nujen za da sortirame polucheniq rezultat po azbuchen red.
            //ToList() e nujen za da varne rezultata v list za da moje da se polzva po dolu ForEach
            result1.ForEach(r =>
                {
                    Console.WriteLine("{0} {1}", r.FirstName, r.LastName);
                });
            Console.WriteLine(Environment.NewLine + "********************************************************************************");

            Console.WriteLine("Write a LINQ query that finds the first name and last name of all students with age between 18 and 24.");
            var result2 = (from student in students
                          where student.Age >= 18 && student.Age <= 24
                          select new
                          {
                              FirstName = student.FirstName,
                              LastName = student.LastName,
                          }).ToList();
            result2.ForEach(r =>
            {
                Console.WriteLine("{0} {1}", r.FirstName, r.LastName);
            });
            Console.WriteLine(Environment.NewLine + "********************************************************************************");

            Console.WriteLine("Using the extension methods OrderBy() and ThenBy() with lambda expressions sort the students by first name and last name in descending order.");

            students.OrderByDescending(s => s.FirstName).ThenByDescending(s => s.LastName).ToList().ForEach(r =>
            {
                Console.WriteLine("{0} {1}", r.FirstName, r.LastName);
            });
            Console.WriteLine(Environment.NewLine + "********************************************************************************");

            Console.WriteLine("Rewrite the same with LINQ.");
            var result3 = (from student in students
                           orderby student.FirstName descending
                           orderby student.LastName descending
                           select student).ToList();
            result3.ForEach(r =>
            {
                Console.WriteLine("{0} {1}", r.FirstName, r.LastName);
            });
        }
    }
}