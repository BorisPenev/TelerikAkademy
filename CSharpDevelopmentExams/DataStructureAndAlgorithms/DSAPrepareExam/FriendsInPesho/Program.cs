using System;
using System.Collections.Generic;

namespace FriendsInPesho
{
    class Program
    {
        static void Main()
        {
            string[] input = Console.ReadLine().Split(' ');
            //int nodes = int.Parse(input[0]);
            int streets = int.Parse(input[1]);
            //int hospitals = int.Parse(input[2]);

            string[] hospitalIds = Console.ReadLine().Split(' ');

            Dictionary<int, Node> allNodes = new Dictionary<int, Node>();

            for (int i = 0; i < streets; i++)
            {
                input = Console.ReadLine().Split(' ');
                int firstNodeId = int.Parse(input[0]);
                int secondNodeId = int.Parse(input[1]);
                int distance = int.Parse(input[2]);

                allNodes.AddSafe(firstNodeId, new Node(firstNodeId));
                allNodes.AddSafe(secondNodeId, new Node(secondNodeId));

                var firstNode = allNodes[firstNodeId];
                var secondNode = allNodes[secondNodeId];

                firstNode.Connections.Add(new Connection(secondNode, distance));
                secondNode.Connections.Add(new Connection(firstNode, distance));
            }

            int result = int.MaxValue;

            foreach (string id in hospitalIds)
            {
                var hospitalNode = allNodes[int.Parse(id)];
                hospitalNode.IsHospital = true;
            }

            foreach (string id in hospitalIds)
            {
                var startNode = allNodes[int.Parse(id)];
                Solve(allNodes, startNode);

                int tempSum = 0;
                foreach (var node in allNodes)
                {
                    if (!node.Value.IsHospital)
                    {
                        tempSum += node.Value.Distance;
                    }
                }

                if (tempSum < result)
                {
                    result = tempSum;
                }
            }

            Console.WriteLine(result);
        }

        static void Solve(Dictionary<int, Node> allNodes, Node startNode)
        {

            foreach (var node in allNodes.Values)
            {
                node.Distance = int.MaxValue;
            }

            startNode.Distance = 0;
            
            PriorityQueue<Node> nodes = new PriorityQueue<Node>();
            nodes.Enqueue(startNode);

            while (nodes.Count > 0)
            {
                var node = nodes.Dequeue();

                if (node.Distance == int.MaxValue)
                {
                    break;
                }

                for (int i = 0; i < node.Connections.Count; i++)
                {
                    var newDistance = node.Distance + node.Connections[i].Distance;
                    if (newDistance < node.Connections[i].ToNode.Distance)
                    {
                        node.Connections[i].ToNode.Distance = newDistance;
                        nodes.Enqueue(node.Connections[i].ToNode);
                    }
                }
            }
        }
    }
}