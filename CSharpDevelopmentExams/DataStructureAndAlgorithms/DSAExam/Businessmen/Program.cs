using System;

namespace Businessmen
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            Console.SetIn(new System.IO.StreamReader("../../input.txt"));
#endif
            int n = int.Parse(Console.ReadLine()) / 2;
            //for (int i = 2; i <= 70; i++)
            //{
            //    var v = i/2;
            //    var top = (2*v).GetFactoriel();
            //    var bottom = ((v + 1).GetFactoriel()*v.GetFactoriel());
            //    Console.WriteLine(top / bottom);
            //}
            Console.WriteLine((2 * n).GetFactoriel() / ((n + 1).GetFactoriel() * n.GetFactoriel()));
        }
    }
}
