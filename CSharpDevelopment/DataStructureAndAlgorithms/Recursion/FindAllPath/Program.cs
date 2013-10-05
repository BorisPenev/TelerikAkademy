using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindAllPath
{
    class Program
    {
        static string[,] labyrinth = 
        {
            {" ", " ", " ", "*", " ", " ", " "},
            {"*", "*", " ", "*", " ", "*", " "},
            {" ", " ", " ", " ", " ", " ", " "},
            {" ", "*", "*", "*", "*", "*", " "},
            {" ", " ", " ", " ", " ", " ", " "},
        };

        static void Main()
        {
            labyrinth[4, 6] = "e";
            CheckPath(0, 0, 0, new Stack<int>());
        }

        private static void CheckPath(int row, int col, int count, Stack<int> stack)
        {
            if (row < 0 || col < 0 ||
                row >= labyrinth.GetLength(0) || col >= labyrinth.GetLength(1))
            {
                return;
            }

            if (labyrinth[row, col] == "e")
            {
                Console.WriteLine("Exit found at [{0},{1}]", row, col);
                Print(new Stack<int>(stack));
            }

            if (labyrinth[row, col] != " ")
            {
                return;
            }

            stack.Push(count);
            labyrinth[row, col] = count.ToString();
            count++;
            CheckPath(row, col - 1, count, stack);
            CheckPath(row - 1, col, count, stack);
            CheckPath(row, col + 1, count, stack);
            CheckPath(row + 1, col, count, stack);

            labyrinth[row, col] = " ";
            if (stack.Count > 0)
            {
                stack.Pop();
            }
        }

        private static void Print(Stack<int> stack)
        {
            while (stack.Count > 0)
            {
                if (stack.Count == 1)
                {
                    Console.Write(stack.Pop()); 
                }
                else
                {
                    Console.Write(stack.Pop() + ",");
                }
                
            }
            Console.WriteLine();
            for (int i = 0; i < labyrinth.GetLength(0); i++)
            {
                for (int j = 0; j < labyrinth.GetLength(1); j++)
                {
                    Console.Write("{0, 2} ", labyrinth[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
