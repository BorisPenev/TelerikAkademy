using System;

namespace Labyrinth3D
{
    internal class Program
    {
        private static int minCount = 30;
        private static string[,,] lab;

        private static void Main()
        {
#if DEBUG
            Console.SetIn(new System.IO.StreamReader("../../input.txt"));
#endif
            string[] startPosition = Console.ReadLine().Split(' ');
            string[] labyrinthParams = Console.ReadLine().Split(' ');

            int level = int.Parse(labyrinthParams[0]);
            int rows = int.Parse(labyrinthParams[1]);
            int cols = int.Parse(labyrinthParams[2]);

            lab = new string[level, rows, cols];

            for (int lvl = 0; lvl < level; lvl++)
            {
                for (int row = 0; row < rows; row++)
                {
                    char[] line = Console.ReadLine().ToCharArray();
                    for (int col = 0; col < cols; col++)
                    {
                        lab[lvl, row, col] = line[col].ToString();
                    }
                }
            }

            FindExit(int.Parse(startPosition[0]), int.Parse(startPosition[1]), int.Parse(startPosition[2]), 0);

            Console.WriteLine(minCount);
        }

        private static void FindExit(int level, int row, int col, int count)
        {
            if (count > minCount)
            {
                return;
            }
            //if (level == 1 && row == 0 && col == 3)
            //{
                
            //}

            // Check if we have found the exit
            if (level < 0 || level >= lab.GetLength(0))
            {
                if (count < minCount)
                {
                    minCount = count;
                }
                //Console.WriteLine("NEW LVL");
                //Print();
                return;
            }

            if (col < 0 || row < 0 || col >= lab.GetLength(2) || row >= lab.GetLength(1))
            {
                // We are out of the labyrinth -> can't find a path
                return;
            }

            if (lab[level, row, col] != "." && lab[level, row, col] != "U" && lab[level, row, col] != "D")
            {
                //if (lab[level, row, col].StartsWith("U") && lab[level, row, col].Length > 1)
                //{
                //    return;
                //}

                //if (lab[level, row, col].StartsWith("D") && lab[level, row, col].Length > 1)
                //{
                //    return;
                //}

                // The current cell is not free -> can't find a path
                return;
            }

            // Temporary mark the current cell as visited
            switch (lab[level, row, col])
            {
                case ".":
                    lab[level, row, col] = count.ToString();
                    count++;
                    break;
                case "U":
                    lab[level, row, col] = "U" + count.ToString();
                    count++;
                    break;
                case "D":
                    lab[level, row, col] = "D" + count.ToString();
                    count++;
                    break;
            }

            FindExit(level, row + 1, col, count); // down
            FindExit(level, row - 1, col, count); // up
            if (lab[level, row, col].StartsWith("D"))
            {
                FindExit(level - 1, row, col, count);
            }
            //up
            if (lab[level, row, col].StartsWith("U"))
            {
                FindExit(level + 1, row, col, count);
            }
            FindExit(level, row, col + 1, count); // right
            FindExit(level, row, col - 1, count); // left
           

            // Invoke recursion to explore all possible directions
            //down
            //if (lab[level, row, col].StartsWith("D"))
            //{
            //    FindExit(level - 1, row, col, count);
            //}
            ////up
            //if (lab[level, row, col].StartsWith("U"))
            //{
            //    FindExit(level + 1, row, col, count);
            //}
            //FindExit(level, row, col + 1, count); // right
            //FindExit(level, row + 1, col, count); // down
            //FindExit(level, row, col - 1, count); // left
            //FindExit(level, row - 1, col, count); // up
            

            // Mark back the current cell as free
            if (lab[level, row, col].StartsWith("D"))
            {
                lab[level, row, col] = "D";
            }
            else if (lab[level, row, col].StartsWith("U"))
            {
                lab[level, row, col] = "U";
            }
            else
            {
                lab[level, row, col] = ".";
            }
        }

        private static void Print()
        {
            Console.WriteLine();

            for (int lvl = 0; lvl < lab.GetLength(0); lvl++)
            {
                for (int row = 0; row < lab.GetLength(1); row++)
                {
                    for (int col = 0; col < lab.GetLength(2); col++)
                    {
                        Console.Write("{0, 2} ", lab[lvl, row, col]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
