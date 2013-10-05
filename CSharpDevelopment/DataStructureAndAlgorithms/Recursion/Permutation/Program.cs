//http://hardprogrammer.blogspot.com/2006/11/permutaciones-con-repeticin.html
using System;

namespace Permutation
{
    class Program
    {
        private static void Main()
        {
            int n = 4;
            int[] numbers = new int[n];
            for (int i = 0; i < n; i++)
            {
                numbers[i] = i + 1;
            }
            Permutations(numbers);
        }

        private static void Permutations(int[] numbersSet)
        {
            Array.Sort(numbersSet);
            Permute(numbersSet, 0, numbersSet.Length);
        }

        private static void Permute(int[] numbersSet, int start, int end)
        {
            PrintNumbers(numbersSet);

            int swapValue = 0;

            if (start < end)
            {
                for (int i = end - 2; i >= start; i--)
                {
                    for (int j = i + 1; j < end; j++)
                    {
                        if (numbersSet[i] != numbersSet[j])
                        {
                            swapValue = numbersSet[i];
                            numbersSet[i] = numbersSet[j];
                            numbersSet[j] = swapValue;

                            Permute(numbersSet, i + 1, end);
                        }
                    }

                    swapValue = numbersSet[i];
                    for (int k = i; k < end - 1; )
                    {
                        numbersSet[k] = numbersSet[++k];
                    }
                    numbersSet[end - 1] = swapValue;
                }
            }
        }

        private static void PrintNumbers(int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write("{0} ", numbers[i]);
            }
            Console.WriteLine();
        }
    }
}
