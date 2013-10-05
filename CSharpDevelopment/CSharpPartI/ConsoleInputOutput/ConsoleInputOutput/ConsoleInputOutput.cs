using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ConsoleInputOutput
{
    class ConsoleInputOutput
    {
        static void Main()
        {

            int daysOfExam = 3;
            int timeOfExam = 6;
            int placeInRoom = 100;// залата е от около 200 човека но да не са един до друг да си клюкарят по екраните.
            int totalStudents = 12 / timeOfExam * placeInRoom * daysOfExam;
            //var stidents.Take(totalStudents).Where(s => s.Homeworks.Max() && s.Test.Max() && s.Bonuses.Max()).OrderBy(o => o.TotalPoints).ToList().SendMail();

            //Sum3Integers();
            //CalculatePerimeterAndAreaOfCircle();
            //GetSetCompanyManager();
            //Console.WriteLine(CountP(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine())));
            //Console.WriteLine(Math.Max(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine())));
            //SolveQuadratic(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));
            //CalculateSumOfNumbers();
            //PrintNumbersInterval();
            //Fibonacci(100);
            //CalculateSum();
            Console.ReadKey();
        }

        private static void CalculateSum()
        {
            float sum = 1f;
            for (int i = 2; (1f / i) >= 0.0001f; i++)
            {
                if (i % 2 == 0)
                {
                    sum += (1f / i);
                }
                else
                {
                    sum -= (1f / i);
                }
            }
            Console.WriteLine("{0:0.000}", sum);
        }

        public static void Fibonacci(int n)
        {
            BigInteger a = 0;
            BigInteger b = 1;
            for (int i = 0; i < n; i++)
            {
                BigInteger temp = a;
                a = b;
                b = temp + b;
                Console.WriteLine(a);
            }
        }

        private static void PrintNumbersInterval()
        {
            int n = int.Parse(Console.ReadLine());
            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine(i);
            }
        }

        private static void CalculateSumOfNumbers()
        {
            int n = int.Parse(Console.ReadLine());
            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum += int.Parse(Console.ReadLine());
            }
            Console.WriteLine(sum);
        }

        public static void SolveQuadratic(double a, double b, double c)
        {
            //Quadratic Formula: x = (-b +- sqrt(b^2 - 4ac)) / 2a
            double insideSquareRoot = (b * b) - 4 * a * c;
            if (insideSquareRoot < 0)
            {
                Console.WriteLine("No Solution");
            }
            else
            {
                double sqrt = Math.Sqrt(insideSquareRoot);
                double x1 = (-b + sqrt) / (2 * a);
                double x2 = (-b - sqrt) / (2 * a);
                if(x1 == x2)
                    Console.WriteLine("Only one solution: " + x1);
                else
                    Console.WriteLine("2 Solutions: " + x1 + " and " + x2);
            }
        }

        private static int CountP(int n, int k)
        {
            int count = 0;
            for (int i = n; i <= k; i++)
            {
                if (i % 5 == 0)
                    count++;
            }
            return count;
        }

        private static void GetSetCompanyManager()
        {
            Company c = new Company();
            Console.Write("Write a Company Name: ");
            c.Name = Console.ReadLine();
            Console.Write("Write a Company Address: ");
            c.Address = Console.ReadLine();
            Console.Write("Write a Company Phone Number: ");
            c.PhoneNumber = Console.ReadLine();
            Console.Write("Write a Company Fax Number: ");
            c.FaxNumber = Console.ReadLine();
            Console.Write("Write a Company Web Site: ");
            c.WebSite = Console.ReadLine();
            Console.Write("Write a Company Manager First Name: ");
            c.CompanyManager.FirstName = Console.ReadLine();
            Console.Write("Write a Company Manager Last Name: ");
            c.CompanyManager.LastName = Console.ReadLine();
            Console.Write("Write a Company Manager Age: ");
            c.CompanyManager.Age = byte.Parse(Console.ReadLine());
            Console.Write("Write a Company Manager PhoneNumber: ");
            c.CompanyManager.PhoneNumber = Console.ReadLine();

            Console.WriteLine("Company Name: " + c.Name);
            Console.WriteLine("Company Address: " + c.Address);
            Console.WriteLine("Company Phone Number: " + c.PhoneNumber);
            Console.WriteLine("Company Fax Number: " + c.FaxNumber);
            Console.WriteLine("Company Web Site: " + c.WebSite);
            Console.WriteLine("Company Manager First Name: " + c.CompanyManager.FirstName);
            Console.WriteLine("Company Manager Last Name: " + c.CompanyManager.LastName);
            Console.WriteLine("Company Manager Age: " + c.CompanyManager.Age);
            Console.WriteLine("Company Manager PhoneNumber: " + c.CompanyManager.PhoneNumber);
        }

        private static void CalculatePerimeterAndAreaOfCircle()
        {
            int r = int.Parse(Console.ReadLine());
            Console.WriteLine("Perimeter of the circle is: " + 2 * Math.PI * r);
            Console.WriteLine("Area of the circle is: " + Math.PI * r * r);
        }

        private static void Sum3Integers()
        {
            int i = int.Parse(Console.ReadLine());
            int n = int.Parse(Console.ReadLine());
            int p = int.Parse(Console.ReadLine());
            Console.WriteLine(i + n + p);
        }
    }
}
