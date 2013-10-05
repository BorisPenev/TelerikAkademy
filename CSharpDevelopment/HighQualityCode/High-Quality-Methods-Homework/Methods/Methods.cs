using System;
using Methods.Utils;

namespace Methods
{
    class Methods
    {
        static void Main()
        {
            Console.WriteLine(Calculations.TriangleArea(3, 4, 5));
            
            Console.WriteLine(Converters.NumberToDigit(5));
            
            Console.WriteLine(Mathematical.Max(5, -1, 3, 2, 14, 2, 3));
            
            Console.WriteLine(Formatters.Number(1.3, "f"));
            Console.WriteLine(Formatters.Number(0.75, "%"));
            Console.WriteLine(Formatters.Number(2.30, "r"));

            double x1 = 3;
            double y1 = -1;
            double x2 = 3;
            double y2 = 2.5;
            Console.WriteLine(Calculations.Distance(x1, y1, x2, y2));
            Console.WriteLine("Horizontal? " + (x1 == x2));
            Console.WriteLine("Vertical? " + (y1 == y2));

            DateTime birthDate;
            var peter = new Student() { FirstName = "Peter", LastName = "Ivanov" };
            // "From Sofia, born at 17.03.1992";
            peter.City = "Sofia";
            if (DateTime.TryParse("17.03.1992", out birthDate))
            {
                peter.BirthDate = birthDate;
            }
            else
            {
                throw new FormatException("Invalid Birth Data entered: 17.03.1992");
            }
            
            var stella = new Student() { FirstName = "Stella", LastName = "Markova" };
            //stella.OtherInfo = "From Vidin, gamer, high results, born at 03.11.1993";
            stella.City = "Vidin";
            if (DateTime.TryParse("03.11.1993", out birthDate))
            {
                stella.BirthDate = birthDate;
            }
            else
            {
                throw new FormatException("Invalid Birth Data entered: 03.11.1993");
            }

            Console.WriteLine("{0} older than {1} -> {2}",
                peter.FirstName, stella.FirstName, peter.IsOlder(stella));
        }
    }
}
