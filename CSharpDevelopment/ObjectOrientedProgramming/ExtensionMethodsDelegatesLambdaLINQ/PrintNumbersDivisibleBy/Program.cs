using System;
using System.Linq;

namespace PrintNumbersDivisibleBy
{
    class Program
    {
        static void Main()
        {
            //Write a program that prints from given array of integers all numbers that are divisible by 7 and 3. Use the built-in extension methods and lambda expressions. 

            int[] numbers = new int[100];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = i;
            }

            numbers.Where(x => x % 7 == 0 && x % 3 == 0).ToList().ForEach(r =>
                {
                    Console.WriteLine(r);
                });

            Console.WriteLine(Environment.NewLine + "********************************************************************************");

            //Rewrite the same with LINQ.
            var result = (from n in numbers
                          where n % 7 == 0 && n % 3 == 0
                          select n).ToList();
            result.ForEach(r =>
            {
                Console.WriteLine(r);
            });
        }
    }
}
