using System;
using System.Linq;
using System.Text;

namespace Problem1TripleRotationOfDigits
{
    class Program
    {
        static void Main()
        {
            StringBuilder sb = new StringBuilder(Console.ReadLine());
            char temp = new char();
            for (int i = 0; i < 3; i++)
            {
                temp = sb[sb.Length - 1];
                sb.Remove(sb.Length - 1, 1);
                if(temp != '0')
                    sb.Insert(0, temp);
            }
            Console.WriteLine(sb.ToString());
        }
    }
}
