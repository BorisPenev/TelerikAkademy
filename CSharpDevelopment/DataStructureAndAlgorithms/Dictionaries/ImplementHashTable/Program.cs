using System;

namespace ImplementHashTable
{
    class Program
    {
        static void Main()
        {
            HashTable<string, int> ht = new HashTable<string, int>();

            ht.Add("Hari", 10);
            ht.Add("Potar", 20);

            ht["Star"] = 12;
            ht["Wars"] = 12;

            Console.WriteLine(ht["Hari"]);
            ht["Hari"] = 22;
            Console.WriteLine(string.Join(Environment.NewLine, ht));
        }
    }
}
