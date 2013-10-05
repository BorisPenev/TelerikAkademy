using System;
using System.Linq;

namespace CommonTypes
{
    class Program
    {
        static void Main()
        {

            //test 4: Create a class Person with two fields – name and age. Age can be left unspecified (may contain null value. Override ToString() to display the information of a person and if age is not specified – to say so. Write a program to test this functionality.
            Person p = new Person("pesho", null);
            Console.WriteLine(p);
            p = new Person("pesho", 22);
            Console.WriteLine(p);

            //test 5: Define a class BitArray64 to hold 64 bit values inside an ulong value. Implement IEnumerable<int> and Equals(…), GetHashCode(), [], == and !=.
            BitArray64 ba = new BitArray64(100);
            for (int i = 0; i < 100; i++)
            {
                ba[i] = (ulong)i;
            }
            foreach (var b in ba)
            {
                Console.WriteLine(b);
            }
        }
    }
}
