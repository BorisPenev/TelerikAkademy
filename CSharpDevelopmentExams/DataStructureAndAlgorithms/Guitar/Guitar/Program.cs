using System;
using System.Linq;

namespace Guitar
{
    class Program
    {
        static void Main()
        {
#if DEBUG
            Console.SetIn(new System.IO.StreamReader("../../input.txt"));
#endif
            int n = int.Parse(Console.ReadLine());
            int[] volumes = Console.ReadLine().Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int startVolume = int.Parse(Console.ReadLine());
            int maxVolume = int.Parse(Console.ReadLine());

            int[,] dpMatrix = new int[n + 1, maxVolume + 1];
            dpMatrix[0, startVolume] = 1;

            for (int rowSongs = 1; rowSongs <= n; rowSongs++)
            {
                for (int colVolume = 0; colVolume <= maxVolume; colVolume++)
                {
                    if (dpMatrix[rowSongs - 1, colVolume] == 1)
                    {
                        int possibleVolume = colVolume - volumes[rowSongs - 1];
                        if (possibleVolume >= 0)
                        {
                            dpMatrix[rowSongs, possibleVolume] = 1;
                        }

                        possibleVolume = colVolume + volumes[rowSongs - 1];
                        if (possibleVolume <= maxVolume)
                        {
                            dpMatrix[rowSongs, possibleVolume] = 1;
                        }
                    }
                }
            }

            for (int col = maxVolume; col >= 0; col--)
            {
                if (dpMatrix[n, col] == 1)
                {
                    Console.WriteLine(col);
                    return;
                }
            }

            Console.WriteLine(-1);
        }
    }
}
