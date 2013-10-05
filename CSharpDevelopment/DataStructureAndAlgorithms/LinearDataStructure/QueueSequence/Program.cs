using System;
using System.Collections.Generic;

namespace QueueSequence
{
    class Program
    {
        static int end = 50;
        static List<int> result = new List<int>(); 

        static void Main()
        {
            int N = 5;
            
            var queue = new Queue<int>();
            queue.Enqueue(N);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                Calculate(OperationOne(current), queue);

                if (queue.Count > 0)
                {
                    Calculate(OperationTwo(current), queue);
                }

                if (queue.Count > 0)
                {
                    Calculate(OperationThree(current), queue);
                }
            }
        }

        private static void Calculate(int newNumber, Queue<int> queue)
        {
            result.Add(newNumber);
            end--;
            if (end <= 0)
            {
                Console.WriteLine(string.Join(Environment.NewLine, result));
                queue.Clear();
            }
            else
            {
                queue.Enqueue(newNumber);
            }
        }

        static int OperationOne(int number)
        {
            return number += 1;
        }

        static int OperationTwo(int number)
        {
            return 2*number + 1;
        }

        static int OperationThree(int number)
        {
            return number += 2;
        }
    }
}
