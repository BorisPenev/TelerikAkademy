using System;
using System.Collections.Generic;

namespace CheckForPossiblePath
{
    class Program
    {
        static string[,] labyrinth = new string[100, 100];

        static void Main()
        {
            for (int row = 0; row < 100; row++)
            {
                for (int col = 0; col < 100; col++)
                {
                    labyrinth[row, col] = " ";
                }
            }
            labyrinth[40, 60] = "e";
            Stack<int> stack = new Stack<int>();
            Console.WriteLine(CheckPath(5, 5, 0, stack));
        }

        private static bool CheckPath(int row, int col, int count, Stack<int> stack)
        {
            if (row < 0 || col < 0 ||
                row >= labyrinth.GetLength(0) || col >= labyrinth.GetLength(1))
            {
                return false;
            }

            if (labyrinth[row, col] == "e")
            {
                return true;
            }

            if (labyrinth[row, col] != " ")
            {
                return false;
            }

            stack.Push(count);
            labyrinth[row, col] = count.ToString();
            count++;
            if (CheckPath(row, col - 1, count, stack))
            {
                return true;
            }
            
            if (CheckPath(row - 1, col, count, stack))
            {
                return true;
            }

            if (CheckPath(row, col + 1, count, stack))
            {
                return true;
            }

            if (CheckPath(row + 1, col, count, stack))
            {
                return true;
            }

            labyrinth[row, col] = " ";
            if (stack.Count > 0)
            {
                stack.Pop();
            }
            return false;
        }
    }
}
