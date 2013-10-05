using System;
using System.Collections.Generic;

namespace MaximalPath
{
    class Program
    {
        static HashSet<int> visitedIds = new HashSet<int>();
        static long maxSum = 0;

        static void Main()
        {
#if DEBUG
            Console.SetIn(new System.IO.StreamReader("../../input.txt"));
#endif

            Dictionary<int, Node> nodes = new Dictionary<int, Node>();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n - 1; i++)
            {
                string[] input = Console.ReadLine().Split(new Char[] { '(', ')', '<', '-' }, StringSplitOptions.RemoveEmptyEntries);

                int parentId = int.Parse(input[0]);
                int childrenId = int.Parse(input[1]);

                Node parentNode = new Node(parentId);
                nodes.AddSafe(parentId, parentNode);
                parentNode = nodes[parentId];

                Node childrenNode = new Node(childrenId);
                nodes.AddSafe(childrenId, childrenNode);
                childrenNode = nodes[childrenId];
                
                parentNode.Childrens.Add(childrenNode);
                childrenNode.Childrens.Add(parentNode);
            }

            foreach (var value in nodes.Values)
            {
                if (value.Childrens.Count == 1)
                {
                    visitedIds.Clear();
                    DFS(value, 0);
                }
            }

            Console.WriteLine(maxSum);
        }

        private static void DFS(Node node, long sum)
        {
            if (!visitedIds.Contains(node.Id))
            {
                visitedIds.Add(node.Id);
                sum += node.Id;
                
                foreach (var children in node.Childrens)
                {
                    DFS(children, sum);
                }

                if (node.Childrens.Count == 1 && sum > maxSum)
                {
                    maxSum = sum;
                }
            }
        }
    }
}
