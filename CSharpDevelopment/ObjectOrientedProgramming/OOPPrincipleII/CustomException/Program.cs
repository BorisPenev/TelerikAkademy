using System;
using System.Linq;

namespace CustomException
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Insert Number:");
            PrintConsoleNumber();
            Console.Write("Insert Date:");
            PrintConsoleDateTime();
        }

        private static void PrintConsoleNumber()
        {
            int min = 0;
            int max= 100;
            try
            {
                Console.WriteLine("Insert number: " + GetConsoleNumber(min, max));
            }
            catch (InvalidRangeException<int> ex)
            {
                Console.WriteLine("Your number " + ex.Message + " is not in range of: " + ex.Start + " and " + ex.End);
                Console.WriteLine(ex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static int GetConsoleNumber(int min, int max)
        {
            int number = int.Parse(Console.ReadLine());
            if (number > min && number < max)
                return number;
            throw new InvalidRangeException<int>(number.ToString(), min, max);
        }

        private static void PrintConsoleDateTime()
        {
            var min = new DateTime(1980, 1, 1);
            var max = new DateTime(2013, 12, 31);
            try
            {
                Console.WriteLine("Inserted datetime: " + GetConsoleDateTime(min, max));
            }
            catch (InvalidRangeException<DateTime> ex)
            {
                Console.WriteLine("Your date " + ex.Message + " is not in range of: " + ex.Start + " and " + ex.End);
                Console.WriteLine(ex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static DateTime GetConsoleDateTime(DateTime min, DateTime max)
        {
            DateTime date = DateTime.Parse(Console.ReadLine());
            if (date > min && date < max)
                return date;
            throw new InvalidRangeException<DateTime>(date.ToShortDateString(), min, max);
        }
    }
}
