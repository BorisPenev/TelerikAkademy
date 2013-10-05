using System;

namespace FindLargeArea
{
    class Program
    {
        static string[,] labyrinth = 
        {
            {"*", " ", "*", "*", " ", " ", " "},
            {" ", " ", " ", "*", " ", "*", " "},
            {" ", " ", " ", "*", " ", "*", " "},
            {"*", " ", "*", "*", "*", "*", "*"},
            {" ", " ", " ", "*", " ", " ", " "},
        };

        private static int maxSum = 0;
        private static int count = 0;

        static void Main()
        {
            
            for (int row = 0; row < labyrinth.GetLength(0); row++)
            {
                for (int col = 0; col < labyrinth.GetLength(1); col++)
                {
                    if (labyrinth[row, col] == " ")
                    {
                        DFS(row, col);
                        if (count > maxSum)
                        {
                            maxSum = count;
                        }
                        count = 0;
                    }
                }
            }
            Console.WriteLine(maxSum);
        }

        static void DFS(int row, int col)
        {
            if (row < 0 || col < 0 ||
                row >= labyrinth.GetLength(0) || col >= labyrinth.GetLength(1) ||
                labyrinth[row, col] != " ")
            {
                return;
            }

            labyrinth[row, col] = count.ToString();

            count++;
            DFS(row, col - 1);
            DFS(row - 1, col);
            DFS(row, col + 1);
            DFS(row + 1, col);
        }
    }
}
