using System;

namespace EightQueensPuzzle
{
    class Program
    {
        private static int count = 0;

        static void Main()
        {
            int[,] matrix = new int[8, 8];
            Queens(matrix, 8, 0);
            Console.WriteLine(count);
        }

        private static void Queens(int[,] matrix, int queens, int col)
        {
            for (int row = 0; row < 8; row++)
            {
                if (matrix[row, col] == 0)
                {
                    //place queen
                    queens--;
                    matrix[row, col] = 9;

                    SetBeatenField(matrix, row, col, 1);
                    if (queens == 0)
                    {
                        //Print(matrix);
                        count++;
                    }
                    if (col + 1 < 8)
                    {
                        Queens(matrix, queens, col + 1);
                    }

                    //remove queen
                    queens++;
                    matrix[row, col] = 1;
                    SetBeatenField(matrix, row, col, -1);
                }
            }
        }

        private static void Print(int[,] matrix)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Console.Write(matrix[row, col] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static void SetBeatenField(int[,] matrix, int row, int col, int sum)
        {
            SetBeatenColumns(matrix, row, sum);
            SetBeatenRows(matrix, col, sum);
            SetBeatenTopLeftBottomRight(matrix, row, col, sum);
            SetBeatenTopRightBottomLeft(matrix, row, col, sum);
        }

        private static void SetBeatenTopRightBottomLeft(int[,] matrix, int row, int col, int sum)
        {
            int currentRow = row;
            int currentCol = col;
            while (currentRow  - 1 >= 0 && currentCol + 1 < 8)
            {
                matrix[currentRow  - 1, currentCol + 1] += sum;
                currentRow--;
                currentCol++;
            }

            currentRow = row;
            currentCol = col;
            while (currentRow + 1 < 8 && currentCol - 1 >= 0 )
            {
                matrix[currentRow + 1, currentCol - 1] += sum;
                currentRow++;
                currentCol--;
            }
        }

        private static void SetBeatenTopLeftBottomRight(int[,] matrix, int row, int col, int sum)
        {
            int startCol;
            int startRow = 0;
            startCol = col - row;

            for (int i = 0; i < 8; i++)
            {
                if (startRow + i < 8 && startCol + i < 8 && startCol + i >= 0 &&
                    matrix[startRow + i, startCol + i] >= 0 && matrix[startRow + i, startCol + i] < 9)
                {
                    matrix[startRow + i, startCol + i] += sum;
                    if (matrix[startRow + i, startCol + i] < 0)
                    {
                        matrix[startRow + i, startCol + i] = 0;
                    }
                }
            }
        }

        private static void SetBeatenRows(int[,] matrix, int col, int sum)
        {
            for (int i = 0; i < 8; i++)
            {
                if (matrix[i, col] >= 0 && matrix[i, col] < 9)
                {
                    matrix[i, col] += sum;
                    if (matrix[i, col] < 0)
                    {
                        matrix[i, col] = 0;
                    }
                }
            }
        }

        private static void SetBeatenColumns(int[,] matrix, int row, int sum)
        {
            for (int i = 0; i < 8; i++)
            {
                if (matrix[row, i] >= 0 && matrix[row, i] < 9)
                {
                    matrix[row, i] += sum;
                    if (matrix[row, i] < 0)
                    {
                        matrix[row, i] = 0;
                    }
                }
            }
        }
    }
}
