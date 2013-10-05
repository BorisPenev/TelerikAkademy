//http://hardprogrammer.blogspot.com/2006/11/permutaciones-con-repeticin.html

using System;

namespace PermutationsWithRepetitions
{
    class Program
    {
        private static void Main(string[] args)
        {
            //int[] numbers = { 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
            string[] numbers = {"(2 3)", "(2 2)", "(3 2)"};
            PermutationsWithRepetition(numbers);
        }

        private static void PermutationsWithRepetition(string[] numbersSet)
        {
            Array.Sort(numbersSet);
            Permute(numbersSet, 0, numbersSet.Length);
        }

        private static void Permute(string[] numbersSet, int start, int end)
        {
            PrintNumbers(numbersSet);

            string swapValue = string.Empty;

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
                    for (int k = i; k < end - 1;)
                    {
                        numbersSet[k] = numbersSet[++k];
                    }
                    numbersSet[end - 1] = swapValue;
                }
            }
        }

        private static void PrintNumbers(string[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write("{0} ", numbers[i]);
            }
            Console.WriteLine();
        }
    }
}