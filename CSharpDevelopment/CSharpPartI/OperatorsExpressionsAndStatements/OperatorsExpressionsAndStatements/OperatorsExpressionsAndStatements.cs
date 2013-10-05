using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace OperatorsExpressionsAndStatements
{
    class OperatorsExpressionsAndStatements
    {
        static void Main()
        {
            IsOddEven();
            Console.WriteLine(Environment.NewLine);
            DividedWithoutRemainder();
            Console.WriteLine(Environment.NewLine); 
            CalculateRectangle();
            Console.WriteLine(Environment.NewLine); 
            IsThirdDigitIsSeven();
            Console.WriteLine(Environment.NewLine); 
            Console.WriteLine(8.GetBitByPosition(3));
            Console.WriteLine(Environment.NewLine); 
            Console.WriteLine(new Point(5, 7).IsInCircle(0, 0, 5));
            Console.WriteLine(Environment.NewLine); 
            Console.Write(37.IsPrime());
            Console.WriteLine(Environment.NewLine); 
            CalculatesTrapezoid();
            Console.WriteLine(Environment.NewLine); 
            Console.WriteLine(IsPointInTheCircleOutRectangle(1, 3, 1, -1, 6, 2));
            Console.WriteLine(Environment.NewLine); 
            Console.WriteLine(5.GetBitByPosition(1) == 0 ? false : true);
            Console.WriteLine(Environment.NewLine); 
            Console.WriteLine(5.GetBitByPosition(2));
            Console.WriteLine(Environment.NewLine);
            HoldValueAtPosition(5, 3, 1);
            HoldValueAtPosition(5, 2, 0);
            Console.WriteLine(Environment.NewLine);
            ExchangeBits();
            Console.WriteLine(Environment.NewLine);
            ExchangeBitsExtended(181563, 3, 2, 23);
            Console.ReadKey();
        }

        private static void ExchangeBits()
        {
            uint num = 181563;
            Console.WriteLine(num + " : " + num.ToBitString(26));
            uint temp = 0;
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("index: " + i);
                Console.WriteLine(temp.ToBitString(26));
                temp = temp.SetBitByPosition(2 + i, num.GetBitByPosition(2 + i).ToString().ToBool());
                temp = temp.SetBitByPosition(23 + i, num.GetBitByPosition(23 + i).ToString().ToBool());
                Console.WriteLine(temp + " : " + temp.ToBitString(26));
            }

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("index: " + i);
                Console.WriteLine(num.ToBitString(26));
                num = num.SetBitByPosition(2 + i, temp.GetBitByPosition(23 + i).ToString().ToBool());
                num = num.SetBitByPosition(23 + i, temp.GetBitByPosition(2 + i).ToString().ToBool());
                Console.WriteLine(num + " : " + num.ToBitString(26));
            }
        }

        private static void ExchangeBitsExtended(uint num, int p, int k, int q)
        {
            //uint num = 181563;
            Console.WriteLine(num + " : " + num.ToBitString(32));
            uint temp = 0;
            for (int i = 0; i < k; i++)
            {
                Console.WriteLine(temp.ToBitString(32));
                temp = temp.SetBitByPosition(p + i, num.GetBitByPosition(p + i).ToString().ToBool());
                temp = temp.SetBitByPosition(q + i, num.GetBitByPosition(q + i).ToString().ToBool());
                Console.WriteLine(temp + " : " + temp.ToBitString(32));
            }

            for (int i = 0; i < k; i++)
            {
                Console.WriteLine(num.ToBitString(32));
                num = num.SetBitByPosition(p + i, temp.GetBitByPosition(q + i).ToString().ToBool());
                num = num.SetBitByPosition(q + i, temp.GetBitByPosition(p + i).ToString().ToBool());
                Console.WriteLine(num + " : " + num.ToBitString(32));
            }
        }

        private static void HoldValueAtPosition(int number, int position, int value)
        {
            //int mask = 1 << position;
            Console.WriteLine("Before: {0}", number.ToBitString(8));
            //Console.WriteLine(number.FindBitByPosition(position) == value ? number |= mask : number ^= mask);
            Console.WriteLine(number = number.SetBitByPosition(position, value.ToString().ToBool()));
            Console.WriteLine("After: {0}", number.ToBitString(8));
        }

        private static bool IsPointInTheCircleOutRectangle(int pointX, int pointY, int recX, int recY, int recWidth, int recHeight)
        {
            Point p = new Point(pointX, pointY);
            Rectangle r = new Rectangle(recX, recY, recWidth, recHeight);
            if (p.IsInCircle(1, 1, 3) && !p.IsInRectangle(r))
                return true;
            return false;
        }

        private static void CalculatesTrapezoid()
        {
            int a = 5;
            int b = 5;
            int h = 2;
            Console.Write((a + b) / 2 * h);
        }

        private static bool IsPointInTheCircle(int x, int y)
        {
            double result = Math.Sqrt(x) + Math.Sqrt(y);
            if (result <= 5)
                return true;
            return false;
        }

        private static void IsThirdDigitIsSeven()
        {
            int i = 1734;
            if (i.ToString().Length > 3)
                Console.Write(i.ToString().Substring(i.ToString().Length - 3, 1) == "7");
            else
                Console.WriteLine(false);
        }

        private static void CalculateRectangle()
        {
            int width = 5;
            int height = 5;
            Console.Write("rectangle is: " + width * height);
        }

        private static void DividedWithoutRemainder()
        {
            int i = 70;
            Console.WriteLine(i % 7 == 0 && i % 5 == 0);
        }

        private static void IsOddEven()
        {
            int i = 4;
            if (i.IsEven())
                Console.Write("even");
            else
                Console.Write("odd");
        }
    }
}