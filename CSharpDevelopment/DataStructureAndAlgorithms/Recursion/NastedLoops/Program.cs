using System;

namespace NastedLoops
{
    class Program
    {
        static void Main()
        {
            int n = 3;
            int[] arr = new int[n];
            GenValue(0, arr);
        }

        static void GenValue(int index, int[] arr)
        {
            if (index >= arr.Length)
            {
                Console.WriteLine(string.Join(" ", arr));
            }
            else
            {
                for (int i = 1; i <= arr.Length; i++)
                {
                    arr[index] = i;
                    GenValue(index + 1, arr);
                }
            }
        }
    }
}
