using System;

namespace ColorfulRowsOfBeads
{
    class Program
    {
        private static int count = 0;
        private static void Main()
        {
            char[] arr = "RYYRYBY".ToCharArray();

            arr = Console.ReadLine().ToCharArray();

            PermutationsWithRepetition(arr);
            Console.WriteLine(count);
        }

        private static void PermutationsWithRepetition(char[] numbersSet)
        {
            Array.Sort(numbersSet);
            Permute(numbersSet, 0, numbersSet.Length);
        }

        private static void Permute(char[] numbersSet, int start, int end)
        {
            count++;

            char swapValue = new char();

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
    }
}
