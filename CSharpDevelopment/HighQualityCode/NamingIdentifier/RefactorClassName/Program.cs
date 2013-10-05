using System;

namespace RefactorClassName
{
    class Program
    {
        const int max_count = 6;
        class Print
        {
            public void Start(bool source)
            {
                string sourceToString = source.ToString();
                Console.WriteLine(sourceToString);
            }
        }    

        static void Main()
        {
            Print print = new Print();
            print.Start(true); 
        }
    }
}
