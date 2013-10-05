using System;

namespace KnapsackProblem
{
    class Program
    {
        static void Main()
        {
            const int n = 6;
            int knapsackSize = 10;
            string[] names = new string[n] { "beer", "vodka", "cheese", "nuts", "ham", "whiskey" };
            int[] weights = new int[n] { 3, 8, 4, 1, 2, 8 };
            int[] costs = new int[n] { 2, 12, 5, 4, 3, 13 };
            int[,] matrix = new int[n + 1, knapsackSize + 1];
            int[,] picks = new int[n + 1, knapsackSize + 1];

            Console.WriteLine("Optimal solution: {0}",
                Knapsack(n, knapsackSize, weights, costs, matrix, picks));
            PrintPicks(n, knapsackSize, weights, names, picks);
        }

        private static int Knapsack(int length, int size, int[] weights, int[] cost, int[,] matrix, int[,] picks)
        {
            for (int row = 1; row <= length; row++)
            {
                for (int col = 0; col <= size; col++)
                {
                    if (weights[row - 1] <= col)
                    {
                        var temp = cost[row - 1] + matrix[row - 1, col - weights[row - 1]];
                        matrix[row, col] = Math.Max(matrix[row - 1, col], temp);

                        if (temp > matrix[row - 1, col])
                        {
                            picks[row, col] = 1;
                        }
                        else
                        {
                            picks[row, col] = -1;
                            matrix[row, col] = matrix[row - 1, col];
                        }
                    }
                }
            }
            return matrix[length, size];
        }

        static void PrintPicks(int length, int size, int[] weights, string[] names, int[,] picks)
        {
            while (length > 0)
            {
                if (picks[length, size] == 1)
                {
                    Console.WriteLine(names[length - 1]);
                    length--;
                    size -= weights[length];
                }
                else
                {
                    length--;
                }
            }
            Console.WriteLine("\n");
        }
    }
}
