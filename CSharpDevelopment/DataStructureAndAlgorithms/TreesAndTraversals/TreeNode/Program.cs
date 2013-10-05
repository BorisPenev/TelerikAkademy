using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeNode
{
    class Program
    {
        static void Main()
        {
            var nodes = new Dictionary<int, Node<int>>();

            ParseData(nodes);

            //1. find root node
            var rootNode = FindRootNode(nodes);
            Console.WriteLine("Root Node is: " + rootNode.Value);

            //2. Find All Leafs
            var allLeafs = FindAllLeafs(nodes);
            if (allLeafs.Count == 0)
            {
                Console.WriteLine("No Leafs find");
            }
            else
            {
                Console.WriteLine("All Leafs is: " + string.Join(",", allLeafs.Select(l => l.Value)));
            }

            //3. Find all middle nodes
            var middleNodes = FindAllMiddleNodes(nodes);
            if (middleNodes.Count == 0)
            {
                Console.WriteLine("No middle nodes find");
            }
            else
            {
                Console.WriteLine("Middle Nodes is: " + string.Join(",", middleNodes.Select(l => l.Value)));
            }

            //4. Find longest path in the tree
            int sum = 0;
            int maxPath = 0;
            DFS(rootNode, node =>
                {
                    if (node.Childs.Count > 0)
                    {
                        sum++;
                        if (sum > maxPath)
                        {
                            maxPath = sum;
                        }
                    }
                    else
                    {
                        sum = 0;
                    }
                    return true;
                });
            Console.WriteLine("Max path from root is: " + maxPath);

            //4.* all paths in the tree with given sum S of their nodes
            int s = 8;
            HashSet<string> treepath = new HashSet<string>();
            foreach (var node in nodes.Values)
            {
                if (node.Childs.Count > 0)
                {
                    var stack = new Stack<int>();

                    var safeNode = node;
                    sum = 0;
                    DFS(node, n =>
                        {
                            if (n.Parent == safeNode)
                            {
                                sum = safeNode.Value;
                                if (stack.Count > 1)
                                {
                                    stack.Pop();
                                }
                            }

                            stack.Push(n.Value);
                            sum += n.Value;

                            if (sum == s)
                            {
                                treepath.Add(string.Join(" -> ", stack.Reverse()));
                            }

                            if (sum > s)
                            {
                                stack.Pop();
                                sum -= n.Value;
                                return false;
                            }

                            return true;
                        });
                }
            }

            //4* print
            Console.WriteLine("Paths with sum of {0} is: {1}", s, string.Join(" and ", treepath));

            //5* all subtrees with given sum S of their nodes
            s = 10;
            HashSet<string> subtrees = new HashSet<string>();
            foreach (var node in nodes.Values)
            {
                if (node.Childs.Count > 0)
                {
                    var queue = new Queue<int>();
                    
                    sum = 0;
                    BFS(node, n =>
                    {
                        queue.Enqueue(n.Value);
                        sum += n.Value;

                        if (sum == s)
                        {
                            subtrees.Add(string.Join(" -> ", queue));
                            return false;
                        }

                        if (sum > s)
                        {
                            return false;
                        }

                        return true;
                    });
                }
            }

            //5* print
            Console.WriteLine("Subtrees with sum of {0} is: {1}", s, string.Join(" and ", subtrees));
        }

        private static void BFS(Node<int> node, Func<Node<int>, bool> action)
        {
            Queue<Node<int>> queue = new Queue<Node<int>>();
            queue.Enqueue(node);

            while (queue.Count > 0)
            {
                var n = queue.Dequeue();

                if (action != null)
                {
                    if (!action(n))
                    {
                        queue.Clear();
                        continue;
                    }
                }

                foreach (var child in n.Childs)
                {
                    queue.Enqueue(child);
                }
            }
        }

        private static void DFS(Node<int> node, Func<Node<int>, bool> action)
        {
            Stack<Node<int>> stack = new Stack<Node<int>>();
            stack.Push(node);
            
            while (stack.Count > 0)
            {
                var n = stack.Pop();

                if (action != null)
                {
                    if (!action(n))
                    {
                        continue;
                    }
                }

                foreach (var child in n.Childs)
                {
                    stack.Push(child);
                }
            }
        }

        private static List<Node<int>> FindAllMiddleNodes(Dictionary<int, Node<int>> nodes)
        {
            var result = new List<Node<int>>();
            foreach (var node in nodes)
            {
                if (node.Value.Parent != null && node.Value.Childs.Count > 0)
                {
                    result.Add((node.Value));
                }
            }
            return result;
        }

        private static List<Node<int>> FindAllLeafs(Dictionary<int, Node<int>> nodes)
        {
            var result = new List<Node<int>>();
            foreach (var node in nodes)
            {
                if (node.Value.Childs.Count == 0)
                {
                    result.Add((node.Value));
                }
            }
            return result;
        }

        private static Node<int> FindRootNode(Dictionary<int, Node<int>> nodes)
        {
            foreach (var node in nodes)
            {
                if (node.Value.Parent == null)
                {
                    return node.Value;
                }
            }
            throw new ArgumentException("No Root Node Find", "nodes");
        }

        private static void ParseData(Dictionary<int, Node<int>> nodes)
        {
            var n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n - 1; i++)
            {
                string line = Console.ReadLine();
                string[] values = line.Split(' ');

                int parentValue = int.Parse(values[0]);
                var nodeParent = new Node<int>();
                if (nodes.ContainsKey(parentValue))
                {
                    nodeParent = nodes[parentValue];
                }
                else
                {
                    nodeParent.Value = parentValue;
                    nodes.Add(parentValue, nodeParent);
                }

                int childValue = int.Parse(values[1]);
                var nodeChild = new Node<int>();
                if (nodes.ContainsKey(childValue))
                {
                    nodeChild = nodes[childValue];
                }
                else
                {
                    nodeChild.Value = childValue;
                    nodes.Add(childValue, nodeChild);
                }
                nodeChild.Parent = nodeParent;
                nodeParent.Childs.Add(nodeChild);
            }
        }
    }
}
