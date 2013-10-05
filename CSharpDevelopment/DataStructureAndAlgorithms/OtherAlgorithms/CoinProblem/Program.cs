using System;
using System.Collections.Generic;
using System.Linq;

namespace CoinProblem
{
    class Program
    {
        static int[] coins = { 1,2,5 };
        static int target = 33;

        static void Main()
        {
            coins.Reverse();
            
            Stack<int> result = new Stack<int>();

            Solve(result, coins.Length - 1);

            if(result.Sum() != target)
                Console.WriteLine(-1);
        }

        static void Solve(Stack<int> stack, int index)
        {
            if (index == -1)
            {
                return;
            }

            if (stack.Sum() == target)
            {
                Console.WriteLine(string.Join(", ", stack.Reverse()));
                return;
            }
            else
            {
                if (stack.Sum() + coins[index] <= target)
                {
                    stack.Push(coins[index]);
                }
                else
                {
                    index--;
                }
                Solve(stack, index);
            }
        }
    }
}
