using System;
using System.Collections.Generic;
using System.Linq;

namespace UsingClassesAndObjects
{
    class Program
    {
        static void Main()
        {
            //1. Write a program that reads a year from the console and checks whether it is a leap. Use DateTime.
            //IsLeapYear();

            //2. Write a program that generates and prints to the console 10 random values in the range [100, 200].
            //PrintRandomValues();

            //3. Write a program that prints to the console which day of the week is today. Use System.DateTime.
            //Console.WriteLine(DateTime.Now.DayOfWeek);

            //4. Write methods that calculate the surface of a triangle by given:
            //Side and an altitude to it; Three sides; Two sides and an angle between them. Use System.Math.

            //5. Write a method that calculates the number of workdays between today and given date, passed as parameter. Consider that workdays are all days from Monday to Friday except a fixed list of public holidays specified preliminary as array.
            //CalculateWorkingDays();

            //6. You are given a sequence of positive integer values written into a string, separated by spaces. Write a function that reads these values from given string and calculates their sum. Example:
            //string = "43 68 9 23 318"  result = 461
            //SumStringOfInt();

            //7*

        }

        private static void SumStringOfInt()
        {
            string[] str = Console.ReadLine().Split(' ');
            int sum = 0;
            for (int i = 0; i < str.Length; i++)
            {
                sum += int.Parse(str[i]);
            }
            Console.WriteLine(sum);
        }

        private static void CalculateWorkingDays()
        {
            Console.Write("Write a date in format yyyy/mm/dd: ");
            DateTime dt = DateTime.Parse(Console.ReadLine()).Date;//"2013/2/4"
            int yearsToDate = dt.Year - DateTime.Now.Year + 1;
            List<DateTime> holidays = new List<DateTime>()
            {   
                new DateTime(DateTime.Now.Year, 12, 24),
                new DateTime(DateTime.Now.Year, 12, 25),
                new DateTime(DateTime.Now.Year, 12, 30),
                new DateTime(DateTime.Now.Year, 12, 31),
                new DateTime(DateTime.Now.Year, 01, 01)
            };
            List<DateTime> allHolidays = new List<DateTime>();
            for (int i = 0; i < yearsToDate; i++)
            {
                allHolidays.AddRange(holidays.Select(s => s.AddYears(i)));
            }
            var futureDate = DateTime.Now.Date;
            var daterange = Enumerable.Range(1, GetDaysBetweenDates(DateTime.Now.Date, dt) + 1);
            var dateSet = daterange.Select(d => futureDate.AddDays(d));
            var dateSetElim = dateSet.Except(holidays).Except(dateSet.Where(s => s.DayOfWeek == DayOfWeek.Sunday).Except(dateSet.Where(s => s.DayOfWeek == DayOfWeek.Saturday)));
            Console.WriteLine(dateSetElim.Count());
        }

        private static int GetDaysBetweenDates(DateTime firstDate, DateTime secondDate)
        {
            return secondDate.Subtract(firstDate).Days;
        }

        private static void PrintRandomValues()
        {
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(rnd.Next(100, 200) + 1);
            }
        }

        private static void IsLeapYear()
        {
            Console.Write("Write a year: ");
            Console.WriteLine(DateTime.IsLeapYear(int.Parse(Console.ReadLine())));
        }
    }
}
