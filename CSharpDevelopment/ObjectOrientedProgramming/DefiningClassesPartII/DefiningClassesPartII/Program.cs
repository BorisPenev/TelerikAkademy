using System;
using System.Linq;

namespace DefiningClassesPartII
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericList<int> test = new GenericList<int>(99);
            for (int i = 0; i < 99; i++)
            {
                test.Add(i);
            }
            Console.WriteLine(test + Environment.NewLine);

            Matrix<double> m1 = new Matrix<double>(2, 2);
            m1[0, 0] = 2;
            m1[0, 1] = 2;
            m1[1, 0] = 2;
            m1[1, 1] = 2;
            Matrix<double> m2 = new Matrix<double>(2, 2);
            m2[0, 0] = 3;
            m2[0, 1] = 3;
            m2[1, 0] = 3;
            m2[1, 1] = 3;

            Matrix<double> m3 = m1 * m2;

            Console.WriteLine(m3.ToString());
        }
    }
}
