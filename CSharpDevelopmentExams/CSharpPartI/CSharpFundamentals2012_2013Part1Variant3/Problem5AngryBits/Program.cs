using System;
using System.Linq;

namespace Problem5AngryBits
{
    class Program
    {
        static long scores = 0;
        static byte localScore = 0;
        static void Main()
        {
            uint[] numberList = new uint[8];
            for (int i = 0; i < 8; i++)
            {
                numberList[i] = uint.Parse(Console.ReadLine());
            }

            for (int col = 8; col <= 15; col++)
            {
                for (int row = 0; row <= 7; row++)
                {
                    GetBird(numberList, row, col);
                }
            }

            if (numberList.Any(n => n <= 255 && n > 0))
                Console.WriteLine(scores + " No");
            else
                Console.WriteLine(scores + " Yes");
        }

        private static void GetBird(uint[] numberList, int row, int col)
        {
            while (numberList[row].GetBitByPosition(col) == 1)
            {
                if (col - 1 >= 0)
                {
                    int right = col - 1;
                    //move up
                    if (row - 1 >= 0)
                    {
                        for (int up = row - 1; up >= 0 && numberList[row].GetBitByPosition(col) == 1; up--, right--)
                        {
                            MoveRight(numberList, row, col, up, right);
                        }
                    }
                    //move down
                    for (int down = 1; down < 8 && numberList[row].GetBitByPosition(col) == 1; down++, right--)
                    {
                        MoveRight(numberList, row, col, down, right);
                    }
                }
                numberList[row] = numberList[row].SetBitByPosition(col, true);
            }
        }

        private static void MoveRight(uint[] numberList, int indexRow, int indexCol, int direction, int right)
        {
            if (right <= 7 && numberList[direction].GetBitByPosition(right) == 1)
            {
                localScore = 1;
                numberList[direction] = numberList[direction].SetBitByPosition(right, true);
                HitPig(numberList, direction - 1, right + 1);  //up left
                HitPig(numberList, direction - 1, right);      //up
                HitPig(numberList, direction - 1, right - 1);  //up right
                HitPig(numberList, direction, right + 1);      //left
                HitPig(numberList, direction, right - 1);      //right
                HitPig(numberList, direction + 1, right + 1);  //down left
                HitPig(numberList, direction + 1, right);      //down
                HitPig(numberList, direction + 1, right - 1);  //doen right
                numberList[indexRow] = numberList[indexRow].SetBitByPosition(indexCol, true);
                scores += (indexCol - right) * localScore;
            }
        }

        private static void HitPig(uint[] numberList, int direction, int right)
        {
            if (direction >= 0 && direction < numberList.Length && right >= 0 && right <= 7 && numberList[direction].GetBitByPosition(right) == 1)
            {
                localScore++;
                numberList[direction] = numberList[direction].SetBitByPosition(right, true);
            }
        }
    }

    public static class Extensions
    {
        public static uint GetBitByPosition(this uint source, int position)
        {
            uint mask = (uint)1 << position;
            uint nAndMask = source & mask;
            return nAndMask >> position;
        }

        public static uint SetBitByPosition(this uint source, int position, bool isZero)
        {
            uint mask;
            if (isZero)
            {
                mask = ~((uint)1 << position);
                return source & mask;
            }
            else
            {
                mask = (uint)1 << position;
                return source | mask;
            }
        }
    }
}