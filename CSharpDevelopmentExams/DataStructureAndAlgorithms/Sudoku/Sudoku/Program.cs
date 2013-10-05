using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        static int[,] sudoku = new int[9,9];
        static void Main()
        {
#if DEBUG
            Console.SetIn(new System.IO.StreamReader("../../input.txt"));
#endif

            for (int row = 0; row < 9; row++)
            {
                string line = Console.ReadLine();
                for (int col = 0; col < 9; col++)
                {
                    if(line[col] == '-')
                    {
                        sudoku[row, col] = 0;
                    }
                    else
                    {
                        sudoku[row, col] = line[col] - '0';
                    }
                }
            }

            Solver(0,0);
        }

        static void Solver(int row, int col)
        {
            if (row == 9 && col == 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        sb.Append(sudoku[i, j]);
                    }
                    sb.AppendLine();
                }
                Console.WriteLine(sb.ToString());
                return;
            }

            if (sudoku[row, col] == 0)
            {
                for (int i = 1; i <= 9; i++)
                {
                    if (CheckRow(row, i) || CheckCol(col, i) || CheckSquare(row, col, i))
                    {
                        continue;
                    }

                    sudoku[row, col] = i;
                    Solver(NextRow(row, col), NextCol(col));
                    sudoku[row, col] = 0;
                }
            }
            else
            {
                Solver(NextRow(row, col), NextCol(col));
            }
        }

        static int NextRow(int row, int col)
        {
            col++;
            if (col > 8)
            {
                return row + 1;
            }
            return row;
        }

        static int NextCol(int col)
        {
            col++;
            if (col > 8)
            {
                return 0;
            }
            return col;
        }

        static bool CheckRow(int row, int number)
        {
            for (int col = 0; col < 9; col++)
            {
                if (sudoku[row, col] == number)
                {
                    return true;
                }
            }

            return false;
        }

        static bool CheckCol(int col, int number)
        {
            for (int row = 0; row < 9; row++)
            {
                if (sudoku[row, col] == number)
                {
                    return true;
                }
            }

            return false;
        }

        static bool CheckSquare(int row, int col, int number)
        {
            int startRow = (row / 3) * 3;
            int startCol = (col / 3) * 3;

            for (int i = startRow; i < startRow + 3; i++)
            {
                for (int j = startCol; j < startCol + 3; j++)
                {
                    if (sudoku[i, j] == number)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
