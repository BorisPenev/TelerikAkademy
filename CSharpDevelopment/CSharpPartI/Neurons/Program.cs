using System;

namespace Neurons
{
    class Program
    {
        static void Main()
        {
#if DEBUG
            Console.SetIn(new System.IO.StreamReader("../../input.txt"));
#endif
            long line = long.Parse(Console.ReadLine());
            while (line > -1)
            {
                var number = Convert.ToString(line, 2).PadLeft(32, '0').ToCharArray();
                int startIndex = int.MinValue;
                int endIndex = int.MinValue;
                int tempIdex = int.MinValue;

                for (int i = 0; i < number.Length; i++)
                {
                    if (number[i] == '1')
                    {
                        number[i] = '0';
                        tempIdex = i;
                        if (startIndex > int.MinValue && endIndex == int.MinValue)
                        {
                            endIndex = i - 1;
                        }
                    }
                    else if (tempIdex > int.MinValue)
                    {
                        if (startIndex == int.MinValue)
                        {
                            startIndex = i;
                        }
                    }
                }

                if (endIndex > startIndex)
                {
                    for (int i = startIndex; i <= endIndex; i++)
                    {
                        number[i] = '1';
                    }
                }

                Console.WriteLine(Convert.ToInt32(string.Join("", number), 2));
                //Console.WriteLine(string.Join("", number));

                line = long.Parse(Console.ReadLine());
            }
        }
    }
}