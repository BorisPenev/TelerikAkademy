using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringBuilderExtension
{
    class Program
    {
        static void Main(string[] args)
        {
            //Implement an extension method Substring(int index, int length) for the class StringBuilder that returns new StringBuilder and has the same functionality as Substring in the class String.

            StringBuilder testing = new StringBuilder();
            testing.AppendLine("Implement an extension method Substring(int index, int length) for the class StringBuilder that returns new StringBuilder and has the same functionality as Substring in the class String.");
            Console.WriteLine(testing.Substring(3, 5).ToString());
        }
    }
}
