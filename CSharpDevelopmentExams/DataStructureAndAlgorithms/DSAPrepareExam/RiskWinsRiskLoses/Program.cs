using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace RiskWinsRiskLoses
{
    internal class Program
    {
        private static string startNumber = "88056";
        private static string endtNumber = "86508";
        static bool isSolved = false;

        private static HashSet<string> forbiddenNumbers = new HashSet<string>()
            {
                "88057",
                "88047",
                "85508",
                "87508",
                "86408",
            };
        private static HashSet<string> vizited = new HashSet<string>();
 
        private static void Main()
        {
            startNumber = Console.ReadLine();
            endtNumber = Console.ReadLine();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                forbiddenNumbers.Add(Console.ReadLine());
            }
 
            var startNode = new Node();
            startNode.Value = startNumber.ToArray();
            startNode.Steps = 0;
 
            var queue = new Queue<Node>();
            queue.Enqueue(startNode);
 
            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();

                if (currentNode.Id == endtNumber)
                {
                    Console.WriteLine(currentNode.Steps);
                    isSolved = true;
                    return;
                }
 
                //n+1
                for (int i = 0; i < 5; i++)
                {
                    var newNode = new Node();
                    newNode.Steps = currentNode.Steps + 1;
                    newNode.Value = new char[5];
                    Array.Copy(currentNode.Value, newNode.Value, 5);
                    if (newNode.Value[i] == 9)
                    {
                        newNode.Value[i] = '0';
                    }
                    else
                    {
                        newNode.Value[i]++;
                    }

                    if (!vizited.Contains(newNode.Id) && !forbiddenNumbers.Contains(newNode.Id))
                    {
                        queue.Enqueue(newNode);
                        vizited.Add(newNode.Id);
                    }
                }
 
                //n-1
                for (int i = 0; i < 5; i++)
                {
                    var newNode = new Node();
                    newNode.Steps = currentNode.Steps + 1;
                    newNode.Value = new char[5];
                    Array.Copy(currentNode.Value, newNode.Value, 5);
                    if (newNode.Value[i] == 48)
                    {
                        newNode.Value[i] = '9';
                    }
                    else
                    {
                        newNode.Value[i]--;
                    }
                    if (!vizited.Contains(newNode.Id) && !forbiddenNumbers.Contains(newNode.Id))
                    {
                        queue.Enqueue(newNode);
                        vizited.Add(newNode.Id);
                    }
                }
            }
            if (!isSolved)
                Console.WriteLine(-1);
        }
    }
 
    class Node
    {
        private string id = string.Empty;
        public string Id
        {
            get { return string.Join("", this.Value); }
        }
 
        public char[] Value { get; set; }
 
        public int Steps { get; set; }
    }
}